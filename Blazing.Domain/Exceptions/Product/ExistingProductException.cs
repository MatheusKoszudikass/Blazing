using Blazing.Domain.Entities;

namespace Blazing.Domain.Exceptions.Produtos
{
    #region Error exceptions.
    /// <summary>
    /// Class responsible for existing product exceptions.
    /// </summary>
    /// <param name="produtoNome"></param>
    public class ExistingProductException(IEnumerable<string?> produtoNome) 
        : DomainException($"O produto {produtoNome} já existe.")
    {

    }

    /// <summary>
    /// Class responsible for the product identifier exception.
    /// </summary>
    /// <param name="id"></param>
    public class IdentityProductInvalidException : DomainException
    {

        public IdentityProductInvalidException(Guid id)
            : base($"Identificador inválido: {id}")
        {
        }

        public IdentityProductInvalidException(IEnumerable<Guid> id)
            : base($"Identificadores inválidos: {string.Join(", ", id)}")
        {
        }
    }

    /// <summary>
    /// Class responsible for invalid product exceptions.
    /// </summary>
    /// <param name="produto">Recebendo o produto que foi o motivo da exceção.</param>
    public class ProductInvalidExceptions(Product produto)
        : DomainException($"Produto {produto} invalido.")
    {
    }

    /// <summary>
    /// Class responsible for creating empty product list exceptions.
    /// </summary>
    /// <param name="produtos"></param>
    public class ProductNotFoundExceptions(IEnumerable<Product> produtos)
        : DomainException($"A lista {produtos} está vazia.")
    {
    }
    #endregion
}
