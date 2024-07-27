using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável pela imagem do produtos em geral.
    /// </summary>
    public class Imagem
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string? Url { get; set; }
        [StringLength(200)]
        public string? TextoAlternativo { get; set; }

    }
}
