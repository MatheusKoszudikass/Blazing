using BlazingPizza.Api.Entites;
using BlazingPizza.Models.DTOs;
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Api.Repositories.Interface
{
    /// <summary>
    /// Metodos nescessarios para cria serviço do carrinho de compra.
    /// </summary>
    public interface ICarrinhoCompraRepository
    {
        Task<CarrinhoDeItem?> AddCarrinhoCompra(CarrinhoItemAddDto carrinhoDeItemsDtos);
        Task<CarrinhoDeItem?> UpdateItemQuantity(Guid id, CarrinhoDeItemAtualizarQuantidadeDto carrinhoDeItemAtualizarQuantidadeDto);
        Task<CarrinhoDeItem?> GetItem(Guid id);
        Task<IEnumerable<CarrinhoDeCompra?>> GetItens(Guid usuarioId);
        Task<CarrinhoDeItem?> DeleteItem(Guid id);

    }
}
