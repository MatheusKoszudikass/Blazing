

namespace BlazingPizzaria.Models.DTOs
{
    public class CategoriasDto
    {
        public Guid Id { get; set; }


        public string? Nome { get; set; }

        public ICollection<ProdutoDto>? Produtos { get; set; } = new List<ProdutoDto>();
    }
}
