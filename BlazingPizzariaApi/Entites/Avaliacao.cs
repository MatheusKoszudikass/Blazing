using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável pela avaliação do produto
    /// </summary>
    public class Avaliacao
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public double Media { get; set; }
        public int NumeroDeAvaliacoes { get; set; }

        [ForeignKey("RevisaoId")]
        public Guid RevisaoId { get; set; }

        public Revisao? Revisao { get; set; }
    }
}
