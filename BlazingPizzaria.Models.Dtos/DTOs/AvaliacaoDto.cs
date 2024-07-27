

namespace BlazingPizzaria.Models.DTOs
{
    public class AvaliacaoDto
    {
        public Guid Id { get; set; }
        public double Media { get; set; }
        public int NumeroDeAvaliacoes { get; set; }
        public Guid RevisaoId { get; set; }

        public RevisaoDto? Revisao { get; set; }
    }
}
