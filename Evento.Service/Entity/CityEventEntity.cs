

namespace Eventos.Service.Entity
{
    public class CityEventEntity
    {
        public long IdEvent { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateHourEvent { get; set; }
        public string Local { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public bool Status { get; set; }

    }
}
