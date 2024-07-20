
using System.ComponentModel.DataAnnotations;

namespace BlazingPizzaria.Models.DTOs
{
    public class AtributosDtos
    {
        [Key]
        public int Id { get; set; }
        public string? Cor { get; set; }
        public string? Material { get; set; }
        public string? Modelo { get; set; }
    }
}
