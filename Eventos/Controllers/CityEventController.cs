using APIPessoa.Filter;
using Evento.Service.Interface;
using Eventos.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CityEventController : ControllerBase
    {
        private ICityEventService _cityEventService;
        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("Titulo/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult<List<CityEventDTO>>> ConsultaTituloAsync(string title)
        {
            var evento = await _cityEventService.ConsultEventTituloAsync(title);
            if (!evento.Any())
            {
                return NotFound("Titulo não encontrado");
            }
            return Ok(await _cityEventService.ConsultEventTituloAsync(title));
            
        }
        [HttpGet("Local e Data/{local}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult<List<CityEventDTO>>> ConsultaLocalDataAsync(string local, DateTime data)
        {
            var evento = await _cityEventService.ConsultEventLocalDateAsync(local, data);
            if (!evento.Any())
            {
                return NotFound("Local e data não encontrados");
            }
            return Ok(await _cityEventService.ConsultEventLocalDateAsync(local, data));
        }
        [HttpGet("Preco e Data/{preco}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult<List<CityEventDTO>>> ConsultaPrecoDataAsync(decimal precoMin, decimal precoMax, DateTime data)
        {
            var resposta = await _cityEventService.ConsultEventPriceDateAsync(precoMin, precoMax, data);
            if (!resposta.Any())
            {
                return NotFound();
            }
            return Ok(resposta);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult<CityEventDTO>> InserirAsync(CityEventDTO entity)
        {
            if (!await _cityEventService.AddEventAsync(entity))
            {
                return BadRequest();
            }
            return Ok(entity);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult<CityEventDTO>> EditarEventoAsync(CityEventDTO entity, int id)
        {
            if (!await _cityEventService.EditEventAsync(entity, id))
            {
                return BadRequest();
            }
            return Ok(entity);
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<IActionResult> DeletaAsync(int id)
        {

            if (!await _cityEventService.DeleteOrInativeEvent(id))
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
