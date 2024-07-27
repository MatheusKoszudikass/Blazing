using System.ComponentModel.DataAnnotations;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável  pelo usuário.
    /// </summary>
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        public string? Senha { get; set; }

        public ICollection<CarrinhoDeCompra>? CarrinhosDeCompra { get; set; }
    }
}
