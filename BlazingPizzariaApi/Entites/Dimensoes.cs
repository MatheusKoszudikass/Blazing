using System.ComponentModel.DataAnnotations;

namespace BlazingPizza.Api.Entites
{
    public class Dimensoes
    {
        [Key]
        public int Id { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Profundidade { get; set; }
        public string Unidade { get; set; } = "cm";
    }
}
