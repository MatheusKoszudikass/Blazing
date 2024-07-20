using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    public class Avaliacao
    {
        [Key]
        public int Id { get; set; }
        public double Media { get; set; }
        public int NumeroDeAvaliacoes { get; set; }

        [ForeignKey("RevisaoId")]
        public int RevisaoId { get; set; }

        public Revisao? Revisao { get; set; }
    }
}
