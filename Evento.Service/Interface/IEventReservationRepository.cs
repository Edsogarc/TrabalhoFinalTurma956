using Eventos.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Service.Interface
{
    public interface IEventReservationRepository
    {
        Task<List<EventReservationEntity>> GetReserveAsync(string nome, string tituloEvento);
        Task<bool> DeleteReserveAsync(int idreservation);
        Task<bool> EditQuantityReserveAsync(int id, int quantity);
        Task<bool> PostReserve(EventReservationEntity reserve);
        Task<bool> GetStatusEventAsync(int idevent);
    }
}
