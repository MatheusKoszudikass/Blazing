using BlazingPizza.Api.Entites;
using BlazingPizzaria.Models.DTOs;
using System.Text.Json;

namespace BlazingPizza.Api.Repositories.Security
{
    public class ProdutoEncryptionService
    {

        public string EncryptionProduto(IEnumerable<ProdutoDto> produtoDtos)
        {

            var jasonProdutos = JsonSerializer.Serialize(produtoDtos);
            return Encryption.EncryptString(jasonProdutos);
        }

        public string DescryptionProduto(IEnumerable<ProdutoDto> produtoDtos)
        {
            var jsonProdutos = JsonSerializer.Serialize(produtoDtos);
            return Encryption.DecryptString(jsonProdutos);
        }
    }
}
