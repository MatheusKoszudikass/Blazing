
using System.Text.Json.Serialization;

namespace BlazingPizzaria.Models.DTOs
{
    public class CarrinhoDeItemsDtos
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoDtos? Produto { get; set; }
        public int Quantidade { get; set; }
        public int CarrinhoDeCompraId { get; set; }
        [JsonIgnore]
        public CarrinhoDeCompraDtos? CarrinhoDeCompra { get; set; }
    }
}
