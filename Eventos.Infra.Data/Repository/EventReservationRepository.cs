using Dapper;
using Eventos.Service.DTO;
using Eventos.Service.Entity;
using Eventos.Service.Interface;
using MySqlConnector;

namespace Eventos.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly string _stringConnection;
        public EventReservationRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }
        public async Task<List<EventReservationEntity>> GetReserveAsync(string nome, string tituloEvento)
        {
            string query = "SELECT * FROM eventreservation INNER JOIN cityevent ON " +
                "cityevent.idevent = eventreservation.idevent WHERE personname = " +
                "@nome  AND title LIKE @titulo";
            DynamicParameters param = new();
            param.Add("nome", nome);
            param.Add("titulo", "%"+tituloEvento+"%");
            using MySqlConnection conn = new(_stringConnection);
            try
            {
                return (await conn.QueryAsync<EventReservationEntity>(query, param)).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteReserveAsync(int idreservation)
        {
            string query = "DELETE FROM eventreservation WHERE idreservation = " +
                "@idreservation";
            DynamicParameters param = new(idreservation);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, param);
            return linhasAfetadas > 0;
        }

        public async Task<bool> EditQuantityReserveAsync(int id, int quantity)
        {
            string query = "UPDATE eventreservation SET quantity = " +
                "@quantity WHERE idreservation = @id";
            DynamicParameters param = new(id);
            param.Add("quantidade", quantity);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, param);
            return linhasAfetadas > 0;
        }

        public async Task<bool> PostReserve(EventReservationEntity reserve)
        {
            string query = "INSERT INTO iventreservation (idevent, personname, quantity)" +
                " VALUES (@idevent, @personname, @quantity)";
            DynamicParameters param = new(reserve);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, param);
            return linhasAfetadas > 0;
        }

        public async Task<bool> GetStatusEventAsync(int idevent)
        {
            string query = "SELECT * FROM cityevent WHERE idevent = @idevent";
            DynamicParameters param = new();
            param.Add("idevento", idevent);
            using MySqlConnection conn = new(_stringConnection);
            var valor = await conn.QueryFirstOrDefaultAsync<CityEventDTO>(query, param);
            return valor.Status;
        }
    }
}
