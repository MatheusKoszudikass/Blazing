using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BlazingPizzaria.Models.DTOs;
using System.Text.Json.Serialization;

namespace BlazingPizza.Api.Entites
{
    public class CarrinhoDeItems
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        [ForeignKey("CarrinhoDeCompra")]
        public int CarrinhoDeCompraId { get; set; }
        public CarrinhoDeCompra? CarrinhoDeCompra { get; set; }
    }
}
