using System.ComponentModel.DataAnnotations;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Eintidade responsável pela Dimensoes do produto.
    /// </summary>
    public class Dimensoes
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Profundidade { get; set; }
        public string Unidade { get; set; } = "cm";
    }
}
