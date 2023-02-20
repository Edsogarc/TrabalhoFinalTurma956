using System.ComponentModel.DataAnnotations;

namespace Eventos.Service.DTO
{
    public class CityEventDTO
    {
        [StringLength(100, ErrorMessage = "Tamanho maximo 100 caracteres")]
        [Required(ErrorMessage = "Titulo obrigatório")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Data obrigatória")]
        public DateTime DateHourEvent { get; set; }
        [Required(ErrorMessage = "Local obrigatório")]
        [StringLength(150, ErrorMessage = "Tamanho maximo 150 caracteres")]
        public string Local { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public bool Status { get; set; }
    }
}
