

namespace BlazingPizzaria.Models.DTOs
{
    public class CategoriasDtos
    {
        public int Id { get; set; }


        public string? Nome { get; set; }

        public ICollection<ProdutoDtos>? Produtos { get; set; } = new List<ProdutoDtos>();
    }
}
