using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável por agrupar produtos por categorias.
    /// </summary>
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        public ICollection<Produto?> Produtos { get; set; } = [];
    }
}
