using System.Diagnostics;

namespace Blazing.Domain.Exceptions
{
    #region Exceções de erro.
    /// <summary>
    /// Classe responsavél pela exceções de erro do dominio.
    /// </summary>
    public class DomainException : Exception
    {
        public StackTrace ExceptionStackTrace { get; }
        public DomainException(string message) : base(message) 
        {
            ExceptionStackTrace = new (true);
        }

        public DomainException(string message, Exception innerException) : base(message, innerException) 
        {
            ExceptionStackTrace = new (true);
        }

        public DomainException(string message, StackTrace stackTrace) : base(message)
        {
            ExceptionStackTrace = stackTrace;
        }
    }
    #endregion
}
