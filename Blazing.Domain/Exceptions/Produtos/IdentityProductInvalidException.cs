using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Domain.Exceptions.Produtos
{
    #region Exceções de erro.
    /// <summary>
    /// Classe responsável pela exceção o identificado do produto.
    /// </summary>
    /// <param name="Id"></param>
    public class IdentityProductInvalidException : DomainException
    {
        public IdentityProductInvalidException(Guid id)
            : base($"Identificador inválido: {id}")
        {
        }

        public IdentityProductInvalidException(IEnumerable<Guid> ids)
            : base($"Identificadores inválidos: {string.Join(", ", ids)}")
        {
        }
    }
    #endregion
}
