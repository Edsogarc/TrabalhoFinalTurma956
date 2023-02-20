using AutoMapper;
using Evento.Service.Interface;
using Eventos.Service.DTO;
using Eventos.Service.Entity;
using Eventos.Service.Interface;

namespace Evento.Service.Service
{
    public class CityEventService : ICityEventService
    {
        private ICityEventRepository _cityEventRepository;
        private IMapper _mapper;

        public CityEventService(ICityEventRepository cityEventRepository, IMapper mapper)
        {
            _cityEventRepository = cityEventRepository;
            _mapper = mapper;
        }

        public async Task<List<CityEventDTO>> ConsultEventTituloAsync(string title)
        {
            List<CityEventEntity> eventoE = await _cityEventRepository.GetEventTitleAsync(title);
            if (eventoE == null) return null;
            
            List<CityEventDTO> eventoDto = _mapper.Map<List<CityEventDTO>>(eventoE);
            return eventoDto;
        }
        public async Task<List<CityEventDTO>> ConsultEventLocalDateAsync(string local, DateTime data)
        {
            List<CityEventEntity> eventoE = await _cityEventRepository.GetEventLocalDateAsync(local, data);
            if (eventoE == null) return null;

            List<CityEventDTO> eventoDto = _mapper.Map<List<CityEventDTO>>(eventoE);
            return eventoDto;
        }
        public async Task<List<CityEventDTO>> ConsultEventPriceDateAsync(decimal precoMin, decimal precoMax, DateTime data)
        {
            var entidade = await _cityEventRepository.GetEventPriceDateAsync(precoMin, precoMax, data);
            if (entidade == null)
            {
                return null;
            }
            var eventoDto = _mapper.Map<List<CityEventDTO>>(entidade);
            return eventoDto;
        }
        public async Task<bool> AddEventAsync(CityEventDTO evento)
        {
            CityEventEntity entidade = _mapper.Map<CityEventEntity>(evento);
            return await _cityEventRepository.PostEventAsync(entidade);
        }
        public async Task<bool> EditEventAsync(CityEventDTO evento, int id)
        {
            CityEventEntity entidade = _mapper.Map<CityEventEntity>(evento);
            return await _cityEventRepository.UpdateEventAsync(entidade, id);
        }
        public async Task<bool> DeleteOrInativeEvent(int id)
        {
            bool qtdReserva = await _cityEventRepository.GetReservasEventAsync(id);

            if (qtdReserva != false)
            {
                return await _cityEventRepository.DeleteEventAsync(id);
            }
            return await _cityEventRepository.InativeEventAsync(id);
        }
    }
}
