
namespace Blazing.Domain.Exceptions.Produtos
{
    #region Exceções de erro.
    /// <summary>
    /// Classe responsável pela exceções de produtos existentes.
    /// </summary>
    /// <param name="produtoNome"></param>
    public class ExistingProductException(IEnumerable<string?> produtoNome) 
        : DomainException($"O produto {produtoNome} já existe.")
    {

    }
    #endregion
}
