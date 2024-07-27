using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável pela criação do carrinho de compra vinculado com usuário logado no sistema.
    /// </summary>
    public class CarrinhoDeCompra
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [ForeignKey("Usuario")]
        public Guid UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        public IEnumerable<CarrinhoDeItem?> Items { get; set; } = [];

        public DateTime DataCriacao { get; set; }
    }
}
