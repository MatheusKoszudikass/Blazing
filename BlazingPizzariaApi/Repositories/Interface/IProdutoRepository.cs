using BlazingPizza.Api.Entites;
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Api.Repositories.Interface
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto?>> AddProduto(List<ProdutoDtos> produto);  
        Task<Produto?> GetItem(int id);
        Task<IEnumerable<Produto?>> GetItens();
        Task<IEnumerable<Categoria?>> GetItensPorCategorias(int id);


    }
}
