using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    public class Imagem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Url { get; set; }
        [StringLength(200)]
        public string? TextoAlternativo { get; set; }

    }
}
