
using System.Text.Json.Serialization;

namespace BlazingPizzaria.Models.DTOs
{
    public class UsuariosDto
    {

        public Guid Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? Senha { get; set; }

        [JsonIgnore]
        public ICollection<CarrinhoDeCompraDto>? CarrinhosDeCompra { get; set; }
    }
}
