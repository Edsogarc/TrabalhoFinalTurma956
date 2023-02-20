using System.ComponentModel.DataAnnotations;

namespace Eventos.Service.DTO
{
    public class EventReservationDTO
    {
        [Required(ErrorMessage = "Identificador obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Insira número no intervalo")]
        public int IdEvent { get; set; }
        [Required(ErrorMessage = "Nome obrigatório")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "Quantidade obrigatória")]
        [Range(1, 10, ErrorMessage = "A quantidade deve estar entre 1 e 10")]
        public int Quantity { get; set; }
    }
}
