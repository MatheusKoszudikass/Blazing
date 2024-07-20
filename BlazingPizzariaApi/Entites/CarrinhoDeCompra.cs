using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazingPizza.Api.Entites
{
    public class CarrinhoDeCompra
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        public IEnumerable<CarrinhoDeItems>? Items { get; set; } = new List<CarrinhoDeItems>();

        public DateTime DataCriacao { get; set; }
    }
}
