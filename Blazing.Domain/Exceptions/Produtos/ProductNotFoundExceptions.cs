using Blazing.Domain.Entities;

namespace Blazing.Domain.Exceptions.Produtos
{
    #region Error exceptions..
    /// <summary>
    /// Class responsible for creating empty product list exceptions.
    /// </summary>
    /// <param name="produtos"></param>
    public class ProductNotFoundExceptions(IEnumerable<Product?> produtos) 
        : DomainException($"A lista {produtos} está vazia.")
    {
    }
    #endregion
}
