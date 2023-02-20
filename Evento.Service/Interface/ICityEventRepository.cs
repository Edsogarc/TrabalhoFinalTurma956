using Eventos.Service.Entity;

namespace Eventos.Service.Interface
{
    public interface ICityEventRepository
    {
        Task<List<CityEventEntity>> GetEventTitleAsync(string title);
        Task<List<CityEventEntity>> GetEventLocalDateAsync(string local, DateTime datehourevent);
        Task<List<CityEventEntity>> GetEventPriceDateAsync(decimal preco, decimal precoMax, DateTime data);
        Task<bool> PostEventAsync(CityEventEntity evento);
        Task<bool> UpdateEventAsync(CityEventEntity evento, int id);
        Task<bool> DeleteEventAsync(int id);
        Task<bool> GetReservasEventAsync(int idevent);
        Task<bool> InativeEventAsync(int id);
    }
}
