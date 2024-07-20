
using System.Text.Json.Serialization;

namespace BlazingPizzaria.Models.DTOs
{
    public class CarrinhoDeCompraDtos
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
            
        public UsuariosDtos? Usuario { get; set; }

        public IEnumerable<CarrinhoDeItemsDtos>? Items { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
