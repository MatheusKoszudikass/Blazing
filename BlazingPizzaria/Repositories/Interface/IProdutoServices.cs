using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Repositories.Interface
{
    public interface IProdutoServices
    {
        Task<IEnumerable<ProdutoDtos>> GetItens();

        Task<ProdutoDtos> GetItem(int Id);

        Task<IEnumerable<ProdutoDtos>> GetItensPorCategoria(int CategoriaId);

        //Task<IEnumerable<ProdutoDtos>> GetItensPorCategoria();
    }
}
