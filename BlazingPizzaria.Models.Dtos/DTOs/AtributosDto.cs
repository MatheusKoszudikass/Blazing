
using System.ComponentModel.DataAnnotations;

namespace BlazingPizzaria.Models.DTOs
{
    public class AtributosDto
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string? Cor { get; set; }
        public string? Material { get; set; }
        public string? Modelo { get; set; }
    }
}
