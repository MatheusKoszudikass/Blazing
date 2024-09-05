using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Domain.Exceptions.User
{
    #region Error exceptions.
    /// <summary>
    /// A static class that contains all exceptions related to user.
    /// this class servers as a centralized location for managing and referencing user-related exceptions
    /// </summary>
    public static class UserException
    {
        /// <summary>
        /// Represents an exception that occurs when a user account is locked out.
        /// </summary>
        public class UserLockedOutException(string message) : DomainException(message)
        {
            /// <summary>
            /// Creates a new instance of the <see cref="UserLockedOutException"/> class with a message that indicates the user's username is locked out.
            /// </summary>
            /// <param name="userName">The username of the locked out user.</param>
            /// <returns>A new instance of the <see cref="UserLockedOutException"/> class.</returns>
            public static UserLockedOutException FromLockedOutExceptionUserName(string userName)
            {
                return new UserLockedOutException(
                    $"Tentativa de login falhou. A conta está bloqueada. Nome do usuário: {userName}");
            }

            /// <summary>
            /// Creates a new instance of the <see cref="UserLockedOutException"/> class with a message that indicates the user's email is locked out.
            /// </summary>
            /// <param name="email">The email of the locked out user.</param>
            /// <returns>A new instance of the <see cref="UserLockedOutException"/> class.</returns>
            public static UserLockedOutException FromLockedOutExceptionEmail(string email)
            {
                return new UserLockedOutException(
                    $"Tentativa de login falhou. A conta está bloqueada. E-mail: {email}");
            }
        }

        /// <summary>
        /// Represents an exception that occurs when a user already exists.
        /// </summary>
        public class UserAlreadyExistsException : DomainException
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class.
            /// </summary>
            /// <param name="message">The error message.</param>
            private UserAlreadyExistsException(string message)
                : base(message) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class with the specified user identifiers.
            /// </summary>
            /// <param name="id">The identifiers of the users that already exist.</param>
            /// <returns>A new instance of the <see cref="UserAlreadyExistsException"/> class.</returns>
            public static UserAlreadyExistsException FromExistingId(string id)
            {
                return new UserAlreadyExistsException(
                    $"Identificador já existe: {string.Join(", ", id)}");
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class with the specified users.
            /// </summary>
            /// <param name="users">The users that already exist.</param>
            /// <returns>A new instance of the <see cref="UserAlreadyExistsException"/> class.</returns>
            public static UserAlreadyExistsException FromExistingUsers(IEnumerable<Entities.User> users)
            {
                return new UserAlreadyExistsException(
                    $"Nenhuma alteração foi detectada para os usuários: {string.Join(", ", users.Select(u => u.UserName).ToList())}");
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class with the specified users names.
            /// </summary>
            /// <param name="usersName">The names of the users that already exist.</param>
            /// <returns>A new instance of the <see cref="UserAlreadyExistsException"/> class.</returns>
            public static UserAlreadyExistsException FromNameExistingUser(string usersName)
            {
                return new UserAlreadyExistsException(
                    $"O nome do usuário: {string.Join(", ", usersName)} já existe.");
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class with the specified user email.
            /// </summary>
            /// <param name="userEmail">The email of the user that already exists.</param>
            /// <returns>A new instance of the <see cref="UserAlreadyExistsException"/> class.</returns>
            public static UserAlreadyExistsException FromEmailExistingUser(string? userEmail)
            {
                return new UserAlreadyExistsException(
                    $"Email: {string.Join(", ", userEmail)} já existe.");
            }
        }

        /// <summary>
        /// Represents an exception that is thrown when a user is found to be invalid.
        /// </summary>
        public class UserInvalidException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="UserInvalidException"/> class with a specified error message.
            /// </summary>
            /// <param name="message">The error message that explains the reason for the exception.</param>
            private UserInvalidException(string message) 
                : base(message) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserInvalidException"/> class with the specified user.
            /// </summary>
            /// <param name="users">The user that is invalid.</param>
            /// <returns>A new instance of the <see cref="UserInvalidException"/> class.</returns>
            public static UserInvalidException UserInvalid(Entities.User users)
            {
                return new UserInvalidException(
                    $"Usuário: {users.FirstName} é inválido.");
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserInvalidException"/> class with the specified users.
            /// </summary>
            /// <param name="user">The users that are invalid.</param>
            /// <returns>A new instance of the <see cref="UserInvalidException"/> class.</returns>
            public static UserInvalidException UsersInvalid(IEnumerable<Entities.User> user)
            {
                return new UserInvalidException(
                    $"Usuários: {string.Join(", ", user.Select(u => u.FirstName).ToList())}");
            }
        }

        /// <summary>
        /// Represents an exception that occurs when there is an error adding users to the identity system.
        /// </summary>
        public class IdentityAddUserException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="IdentityAddUserException"/> class.
            /// </summary>
            /// <param name="message">The error message.</param>
            private IdentityAddUserException(string message)
                : base(message) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="IdentityAddUserException"/> class with the specified user.
            /// </summary>
            /// <param name="user">The user that is invalid.</param>
            /// <returns>A new instance of the <see cref="IdentityAddUserException"/> class.</returns>
            public static IdentityAddUserException AddUsers(IEnumerable<Entities.User> user)
            {
                return new IdentityAddUserException(
                    $"Erro ao adicionar os usuários com os email: {string.Join(",", user.Select(u => u.Email).ToList())}");
            }
        }

        /// <summary>
        /// Represents a custom exception when a user is not found.
        /// </summary>
        public class UserNotFoundException : DomainException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="UserNotFoundException"/> class.
            /// </summary>
            /// <param name="message">The error message.</param>
            private UserNotFoundException(string message)
                : base(message) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserNotFoundException"/> class with the specified user.
            /// </summary>
            /// <param name="users">The user that is not found.</param>
            /// <returns>A new instance of the <see cref="UserNotFoundException"/> class.</returns>
            public static UserNotFoundException UserNotFound(IEnumerable<Entities.User> users)
            {
                return new UserNotFoundException(
                    $"Usuários não encontrados: {string.Join(", ", users.Select(u => u.Email).ToList())}");
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserNotFoundException"/> class with the specified user email.
            /// </summary>
            /// <param name="email">The email of the user that is not found.</param>
            /// <returns>A new instance of the <see cref="UserNotFoundException"/> class.</returns>
            public static UserNotFoundException UserNotFoundEmail(string email)
            {
                return new UserNotFoundException(
                    $"Email do usuário não encontrado:{email}");
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserNotFoundException"/> class with the specified user address.
            /// </summary>
            /// <param name="users">The user address that is not found.</param>
            /// <returns>A new instance of the <see cref="UserNotFoundException"/> class.</returns>
            public static UserNotFoundException UserAddressNotFound(Entities.User users)
            {
                if (users.Addresses != null)
                    return new UserNotFoundException(
                        $"Endereço não foi encontrado:{string.Join(", ", users.Addresses.Select(u => u.Id).ToList())}");

                throw new Exception();
            }

            public static UserNotFoundException AddressNotFound(Address address)
            {
                return new UserNotFoundException(
                    $"Endereço não foi encontrado:{string.Join(", ", address.City)}");
            }

        }
    }
    #endregion
}
