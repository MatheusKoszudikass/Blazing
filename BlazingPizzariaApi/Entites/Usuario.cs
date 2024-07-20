using System.ComponentModel.DataAnnotations;

namespace BlazingPizza.Api.Entites
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

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
