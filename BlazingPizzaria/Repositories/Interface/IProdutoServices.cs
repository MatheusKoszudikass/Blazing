using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Repositories.Interface
{
    public interface IProdutoServices
    {
        Task<IEnumerable<ProdutoDto>> GetItens();

        Task<ProdutoDto> GetItem(int Id);

        Task<IEnumerable<ProdutoDto>> GetItensPorCategoria(int CategoriaId);

        //Task<IEnumerable<ProdutoDtos>> GetItensPorCategoria();
    }
}
