using System.ComponentModel.DataAnnotations;

namespace BlazingPizza.Api.Entites
{
    public class Atributos
    {
        [Key]
        public int Id { get; set; }
        public string? Cor { get; set; }
        public string? Material { get; set; }
        public string? Modelo { get; set; }
    }
}
