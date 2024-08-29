using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Blazing.Domain.Exceptions
{
    #region Exceptions
    /// <summary>
    /// Represents exceptions that occur within the domain.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Gets the stack trace of the exception.
        /// </summary>
        public StackTrace ExceptionStackTrace { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DomainException(string message) : base(message)
        {
            ExceptionStackTrace = new StackTrace(true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
            ExceptionStackTrace = new StackTrace(true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class with a specified error message and a stack trace.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="stackTrace">The stack trace that describes the sequence of function calls leading to the exception.</param>
        public DomainException(string message, StackTrace stackTrace) : base(message)
        {
            ExceptionStackTrace = stackTrace;
        }

        /// <summary>
        /// Exception that is thrown when a Identity Guid is found to be invalid.
        /// </summary>
        public class IdentityInvalidException : DomainException
        {
            private IdentityInvalidException(string message)
                : base(message) { }

            public static IdentityInvalidException Identity(Guid id)
            {
                return new IdentityInvalidException(
                    $"Identificador inválido: {id}");
            }

            public static IdentityInvalidException Identities(IEnumerable<Guid> id)
            {
                return new IdentityInvalidException(
                    $"Identificadores inválidos: {string.Join(",", id)}");
            }

            public static IdentityInvalidException IdentitiesExist(IEnumerable<Guid> id)
            {
                return new IdentityInvalidException(
                    $"Identificadores já existem: {string.Join(",", id)}");
            }
        }

        /// <summary>
        /// Exception that is thrown when a list of is empty or not found.
        /// </summary>
        public class NotFoundException : DomainException
        {
            private NotFoundException(string message)
                : base(message) { }

            public static NotFoundException FoundException()
            {
                return new NotFoundException(
                    $"A lista está vazia.");
            }
        }
    }
    #endregion
}
