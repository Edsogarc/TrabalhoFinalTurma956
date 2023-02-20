using Eventos.Service.DTO;

namespace Evento.Service.Interface
{
    public interface ICityEventService
    {
        Task<List<CityEventDTO>> ConsultEventTituloAsync(string title);
        Task<List<CityEventDTO>> ConsultEventLocalDateAsync(string local, DateTime data);
        Task<List<CityEventDTO>> ConsultEventPriceDateAsync(decimal precoMin, decimal precoMax, DateTime data);
        Task<bool> AddEventAsync(CityEventDTO evento);
        Task<bool> EditEventAsync(CityEventDTO evento, int id);
        Task<bool> DeleteOrInativeEvent(int id);
    }
}
