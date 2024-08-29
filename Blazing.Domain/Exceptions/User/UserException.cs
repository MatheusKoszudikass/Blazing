using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Domain.Exceptions.User
{
    #region Error exceptions.
    /// <summary>
    /// A static class that contains all exceptions related to user.
    /// this class servers as a centralized location for managing and referencing user-reladted exceptions
    /// </summary>
    public static class UserException
    {

        /// <summary>
        /// Exception that is thrown when a user already exists in the system.
        /// </summary>
        public class UserAlreadyExistsException : DomainException
        {

            private UserAlreadyExistsException(string message)
                : base(message) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class with the specified product names.
            /// </summary>
            /// <param name="usersName">The names of the products that already exist.</param>
            public static UserAlreadyExistsException FromExistingUsers(IEnumerable<Entities.User> users)
            {
                return new UserAlreadyExistsException(
                    $"Nenhuma alteração foi dectada para os usuários: {string.Join(",", users.Select(u => u.UserName).ToList())}");
            }

            public static UserAlreadyExistsException FromNameExistingUsers(IEnumerable<Entities.User> usersName)
            {
                return new UserAlreadyExistsException(
                    $"O usuário: {string.Join(",", usersName)} já existe");
            }

            public static UserAlreadyExistsException FromEmailExistingUsers(IEnumerable<Entities.User> userEmail)
            {
                return new UserAlreadyExistsException(
                    $"Email: {string.Join(",", userEmail)}");
            }
        }

        /// <summary>
        /// Exception that is thrown when a user is found to be invalid.
        /// </summary>
        public class UserInvalidException : DomainException
        {
            private UserInvalidException(string message) 
                : base(message) { }

            public static UserInvalidException UserInvalid(Entities.User users)
            {
                return new UserInvalidException(
                    $"Usuário: {users.FirstName} é Inválido.");
            }

            public static UserInvalidException UsersInvalid(IEnumerable<Entities.User> user)
            {
                return new UserInvalidException(
                    $"Usuários: {string.Join(",", user.Select(u => u.FirstName).ToList())}");
            }
        }

        public class IdentityAddUserException : DomainException
        {
            private IdentityAddUserException(string message)
                : base(message) { }

            public static IdentityAddUserException AddUsers(IEnumerable<Entities.User> user)
            {
                return new IdentityAddUserException(
                    $"Erro ao adicionar os usuários com os email: {string.Join(",", user.Select(u => u.Email).ToList())}");
            }
        }

        public class UserNotFaundException : DomainException
        {
            private UserNotFaundException(string message)
                : base(message) { }

            public static UserNotFaundException UserNotFound(IEnumerable<Entities.User> users)
            {
                return new UserNotFaundException(
                    $"Usuários não encontrados: {string.Join(",", users.Select(u => u.Email).ToList())}");
            }

            public static UserNotFaundException UserNotFoundEmail(string email)
            {
                return new UserNotFaundException(
                    $"Email do usuário não encontrado:{email}");
            }

        }
    }
    #endregion
}
