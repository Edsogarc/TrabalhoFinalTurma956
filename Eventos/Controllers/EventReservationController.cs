using APIPessoa.Filter;
using Evento.Service.Interface;
using Eventos.Service.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : Controller
    {
        private IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult<List<EventReservationDTO>>> ConsultaReservaAsync(string nome, string tituloEvento)
        {
            var resposta = await _eventReservationService.ConsultReserveAsync(nome, tituloEvento);
            if (!resposta.Any())
            {
                return NotFound();
            }
            return Ok(resposta);
        }
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult> AdicionarReservaAsync(EventReservationDTO reserva)
        {
            if (!await _eventReservationService.InsertReserveAsync(reserva))
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult<CityEventDTO>> EditarQuantidadeReservaAsync(int id, int quantidade)
        {
            if (!await _eventReservationService.UpdateQuantityReserveAsync(id, quantidade))
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        public async Task<ActionResult> DeletarReservaAsync(int id)
        {
            if (!await _eventReservationService.DeleteReserveAsync(id))
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
