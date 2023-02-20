using Eventos.Service.DTO;

namespace Evento.Service.Interface
{
    public interface IEventReservationService
    {
        Task<bool> InsertReserveAsync(EventReservationDTO reserve);
        Task<List<EventReservationDTO>> ConsultReserveAsync(string nome, string tituloEvento);
        Task<bool> DeleteReserveAsync(int id);
        Task<bool> UpdateQuantityReserveAsync(int id, int quantidade);
    }
}
