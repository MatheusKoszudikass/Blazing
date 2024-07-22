using BlazingPizza.Api.Entites;
using BlazingPizzaria.Models.DTOs;
using System.Collections;

namespace BlazingPizza.Api.Repositories.Interface
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<ProdutoDtos?>> AddProduto(List<ProdutoDtos> produtoDtos);
        Task<ProdutoDtos?> EditProduto(int id, ProdutoDtos produtoDtos);
        Task<ProdutoDtos?> GetItem(int id);
        Task<IEnumerable<ProdutoDtos?>> GetItens();
        Task<IEnumerable<ProdutoDtos?>> GetItensProdutoCategoria(int id);
        Task<IEnumerable<ProdutoDtos?>> DeleteProduto(List<int> id);

    }
}
