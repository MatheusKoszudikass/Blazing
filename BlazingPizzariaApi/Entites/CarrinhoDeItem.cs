using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BlazingPizzaria.Models.DTOs;
using System.Text.Json.Serialization;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável pela adição dos items no carrinho de compra.
    /// </summary>
    public class CarrinhoDeItem
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [ForeignKey("Produto")]
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        [ForeignKey("CarrinhoDeCompra")]
        public Guid CarrinhoDeCompraId { get; set; }
        public CarrinhoDeCompra? CarrinhoDeCompra { get; set; }
    }
}
