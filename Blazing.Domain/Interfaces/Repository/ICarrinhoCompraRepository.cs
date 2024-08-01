using Blazing.Domain.Entities;

namespace Blazing.Domain.Interfaces.Repository
{
    /// <summary>
    /// Metodos nescessarios para cria serviço do carrinho de compra.
    /// </summary>
    public interface ICarrinhoCompraRepository
    {
        Task<CartItem?> AddCarrinhoCompra(CartItem cartItem);
        Task<CartItem?> UpdateItemQuantity(Guid id, AddCartItem addCartItem);
        Task<CartItem?> GetItem(Guid id);
        Task<IEnumerable<CartItem?>> GetItens(Guid usuarioId);
        Task<CartItem?> DeleteItem(Guid id);

    }
}
