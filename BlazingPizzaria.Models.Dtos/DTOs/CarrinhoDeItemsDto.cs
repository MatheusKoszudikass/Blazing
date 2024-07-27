
using System.Text.Json.Serialization;

namespace BlazingPizzaria.Models.DTOs
{
    public class CarrinhoDeItemsDto
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public ProdutoDto? Produto { get; set; }
        public int Quantidade { get; set; }
        public Guid CarrinhoDeCompraId { get; set; }
        [JsonIgnore]
        public CarrinhoDeCompraDto? CarrinhoDeCompra { get; set; }
    }
}
