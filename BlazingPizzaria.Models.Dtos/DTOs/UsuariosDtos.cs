
using System.Text.Json.Serialization;

namespace BlazingPizzaria.Models.DTOs
{
    public class UsuariosDtos
    {

        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? Senha { get; set; }

        [JsonIgnore]
        public ICollection<CarrinhoDeCompraDtos>? CarrinhosDeCompra { get; set; }
    }
}
