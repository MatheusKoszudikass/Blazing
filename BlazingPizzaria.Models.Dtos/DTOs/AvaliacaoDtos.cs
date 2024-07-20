

namespace BlazingPizzaria.Models.DTOs
{
    public class AvaliacaoDtos
    {
        public int Id { get; set; }
        public double Media { get; set; }
        public int NumeroDeAvaliacoes { get; set; }
        public int RevisaoId { get; set; }

        public RevisaoDtos? Revisao { get; set; }
    }
}
