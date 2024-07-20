using BlazingPizza.Api.Entites;
using BlazingPizza.Models.DTOs;
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Api.Repositories.Interface
{
    public interface ICarrinhoCompraRepository
    {
        Task<CarrinhoDeItems?> AddItem(CarrinhoItemAddDtos carrinhoDeItemsDtos);

        Task<CarrinhoDeItems?> AddItensQuantidade(int id, CarrinhoDeItemAtualizarQuantidadeDto carrinhoDeItemAtualizarQuantidadeDto);

        Task<CarrinhoDeItems?> DeletItem(int id);

        Task<CarrinhoDeItems?> GetItem(int id);

        Task<IEnumerable<CarrinhoDeCompra?>> GetItens(int usuarioId);
    }
}
