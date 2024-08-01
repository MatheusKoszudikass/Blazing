
using Blazing.Domain.Entities;

namespace Blazing.Domain.Exceptions.Produtos
{
    #region Error exceptions.
    /// <summary>
    /// Class responsible for invalid product exceptions.
    /// </summary>
    /// <param name="message">Recebendo qual foi o motivo da exceção.</param>
    public class ProductInvalidExceptions(Product produto) 
        : DomainException($"Produto {produto} invalido.")
    {
    }
    #endregion
}
