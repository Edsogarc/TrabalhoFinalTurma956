using AutoMapper;
using Evento.Service.Interface;
using Eventos.Service.DTO;
using Eventos.Service.Entity;
using Eventos.Service.Interface;

namespace Evento.Service.Service
{
    public class EventReservationService : IEventReservationService
    {
        private IEventReservationRepository _EventReservationRep;
        private IMapper _mapper;

        public EventReservationService(IEventReservationRepository Eventrep, IMapper mapper)
        {
            _EventReservationRep = Eventrep;
            _mapper = mapper;
        }

        public async Task<bool> InsertReserveAsync(EventReservationDTO reserve)
        {
            bool status = await _EventReservationRep.GetStatusEventAsync(reserve.IdEvent);
            if (status)
            {
                EventReservationEntity entity = _mapper.Map<EventReservationEntity>(reserve);
                return await _EventReservationRep.PostReserve(entity);
            }
            return false;
        }

        public async Task<List<EventReservationDTO>> ConsultReserveAsync(string nome, string tituloEvento)
        {
            List<EventReservationEntity> entity = await _EventReservationRep.GetReserveAsync(nome, tituloEvento);
            if (entity == null)
            {
                return null;
            }
            List<EventReservationDTO> reservaDto = _mapper.Map<List<EventReservationDTO>>(entity);
            return reservaDto;
        }

        public async Task<bool> DeleteReserveAsync(int id)
        {
            return await _EventReservationRep.DeleteReserveAsync(id);
        }

        public async Task<bool> UpdateQuantityReserveAsync(int id, int quantidade)
        {
            return await _EventReservationRep.EditQuantityReserveAsync(id, quantidade);
        }
    }
}
