
using System.Text.Json.Serialization;

namespace BlazingPizzaria.Models.DTOs
{
    public class CarrinhoDeCompraDto
    {
        public Guid Id { get; set; } 

        public Guid UsuarioId { get; set; }
            
        public UsuariosDto? Usuario { get; set; }

        public IEnumerable<CarrinhoDeItemsDto>? Items { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
