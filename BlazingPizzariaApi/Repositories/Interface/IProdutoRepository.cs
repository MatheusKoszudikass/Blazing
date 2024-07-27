using BlazingPizza.Api.Entites;
using BlazingPizzaria.Models.DTOs;
using System.Collections;

namespace BlazingPizza.Api.Repositories.Interface
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<ProdutoDto?>> AddProdutos(List<ProdutoDto> produtoDtos);
        Task<ProdutoDto?> UpdateProduto(Guid id, ProdutoDto produtoDto);
        Task<IEnumerable<ProdutoDto?>> GetProdutosByCategoriaId(Guid categoriaId);
        Task<IEnumerable<ProdutoDto?>> DeleteProdutos(List<Guid> ids);
        Task<ProdutoDto?> GetProdutoById(Guid id);
        Task<IEnumerable<ProdutoDto?>> GetAllProdutos();
    }
}
