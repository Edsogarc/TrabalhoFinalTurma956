using Dapper;
using Eventos.Service.Entity;
using Eventos.Service.Interface;
using MySqlConnector;

namespace Eventos.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly string _stringConnection;
        public CityEventRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE");
        }
        public async Task<List<CityEventEntity>> GetEventTitleAsync(string title)
        {
            string query = "SELECT * FROM cityevent WHERE title LIKE @title";
            DynamicParameters param = new();
            param.Add("title", "%" + title + "%");
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            try
            {
                return (await conn.QueryAsync<CityEventEntity>(query, param)).ToList();
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<CityEventEntity>> GetEventLocalDateAsync(string local, DateTime datehourevent)
        {
            string query = "SELECT * FROM cityevent WHERE local LIKE @local " +
                "AND DATE_FORMAT(datehourevent, '%Y-%m-%d') = @datehourevent";
            DynamicParameters param = new();
            param.Add("local", "%"+local+"%");
            param.Add("datehourevent", datehourevent);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            try
            {
                return (await conn.QueryAsync<CityEventEntity>(query, param)).ToList();
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<CityEventEntity>> GetEventPriceDateAsync(decimal precoMin, decimal precoMax, DateTime data)
        {
            string query = "SELECT * FROM CityEvent where DATE(dateHourEvent) = @data and price between @precoMin and @precoMax";
            DynamicParameters parametros = new();
            parametros.Add("data", data);
            parametros.Add("precoMin", precoMin);
            parametros.Add("precoMax", precoMax);
            using MySqlConnection conn = new(_stringConnection);
            return (await conn.QueryAsync<CityEventEntity>(query, parametros)).ToList();
        }
        public async Task<bool> PostEventAsync(CityEventEntity evento)
        {
            string query = @"INSERT INTO cityevent(title,description, datehourevent, local, address, price,status) 
             VALUES (@title, @description, @datehourevent, @local, @address, @price,true)";
            DynamicParameters param = new(evento);

            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, param);
            return linhasAfetadas > 0;
        }
        public async Task<bool> UpdateEventAsync(CityEventEntity evento, int id)
        {
            string query = "UPDATE cityevent SET title=@title,description=@description, " +
                "datehourevent=@datehourevent, local=@local, address=@address, price=@price where idevent=@id";
            DynamicParameters param = new(evento);
            param.Add("idevent", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, param);
            return linhasAfetadas > 0;
        }
        public async Task<bool> DeleteEventAsync(int id)
        {
            string query = "DELETE FROM CityEvent WHERE idevent = @id";
            DynamicParameters param = new();
            param.Add("id", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, param);
            return linhasAfetadas > 0;
        }
        public async Task<bool> GetReservasEventAsync(int idevent)
        {
            string query = "SELECT * FROM EventReservation WHERE idevent = @idevent";
            DynamicParameters param = new();
            param.Add("idEvento", idevent);
            using MySqlConnection conn = new(_stringConnection);
            return conn.QueryFirstOrDefaultAsync(query, param) == null;
        }
        public async Task<bool> InativeEventAsync(int id)
        {
            string query = "UPDATE CityEvent SET status = false WHERE Idevent = @id";
            DynamicParameters param = new();
            param.Add("idevent", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, param);
            return linhasAfetadas > 0;
        }
    }
}
