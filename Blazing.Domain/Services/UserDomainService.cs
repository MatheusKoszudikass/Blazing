using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.User;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Interfaces.Services.User;
using System.Linq;
using System.Text;

namespace Blazing.Domain.Services
{
    public class UserDomainService : IUserDomainService
    {

        /// <summary>
        /// Adds a list of user to the repository.
        /// </summary>
        /// <param name="user">The list of user to be added.</param>
        /// <returns>The list of user that have been added.</returns>
        /// <exception cref="DomainException.NotFoundException.FoundException">Thrown when the user list is null or empty.</exception>
        public async Task<IEnumerable<User?>> Add(IEnumerable<User> user, CancellationToken cancellationToken)
        {
            if (user == null || !user.Any())
                throw DomainException.NotFoundException.FoundException();

            try
            {

                return await Task.FromResult(user);
            }
            catch (DomainException)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates a collection of products based on the provided IDs and product data.
        /// </summary>
        /// <param name="id">The IDs of the products to update.</param>
        /// <param name="products">The collection of existing products with the current data.</param>
        /// <param name="productsUpdate">The collection of products containing the updated data.</param>
        /// <returns>A collection of updated products that were found and successfully updated.</returns>
        /// <exception cref="ProductExceptions.IdentityProductInvalidException">Thrown when the provided IDs are invalid or empty.</exception>
        /// <exception cref="ProductExceptions.ProductNotFoundException">Thrown when no products matching the provided IDs are found in the current product collection.</exception>
        /// <exception cref="ProductExceptions.ProductAlreadyExistsException">Thrown when the updated product collection is identical to the existing product collection.</exception>
        /// <exception cref="DomainException">Thrown when a domain-related error occurs during the update process.</exception>
        public async Task<IEnumerable<User?>> Update(IEnumerable<Guid> id, IEnumerable<User> originalUsers , IEnumerable<User> updatedUsers, CancellationToken cancellationToken)
        {
            if (id == null || !id.Any() || id.Contains(Guid.Empty))
                throw DomainException.IdentityInvalidException.Identities(id ?? []);

            var usersDict = originalUsers.Where(u => id.Contains(u.Id)).ToDictionary(u => u.Id);
            var updatedDict = updatedUsers.Where(u => id.Contains(u.Id)).ToDictionary(u => u.Id);

            if (usersDict.Count == 0)
                throw DomainException.NotFoundException.FoundException();

            try
            {
                var modifiedUser = updatedDict
                    .Where(update => usersDict.TryGetValue(update.Key, out var original)&& !AreUseEqual(original, update.Value))
                    .Select(update =>
                    {
                        var updatedUser = update.Value;
                        updatedUser.DataCreated = usersDict[update.Key].DataCreated;
                        updatedUser.DataUpdated = DateTime.Now;
                        return updatedUser;

                    }).ToList();

                if (modifiedUser.Count == 0)
                    throw UserException.UserAlreadyExistsException.FromExistingUsers(modifiedUser);

                 return await Task.FromResult(modifiedUser);
            }
            catch (DomainException)
            {

                throw;
            }
        }

        public bool AreUseEqual(Entities.User user1, Entities.User user2)
        {
            if (user1 == null && user2 == null)
                return false;

            //var AddressUser1 = user1?.Addresses?.FirstOrDefault(a => a.Id == user1.Id);
            //var AddressUser2 = user2?.Addresses?.FirstOrDefault(address => address.Id == user2.Id);

            return user1.Status == user2.Status &&
                   NormalizeString(user1.FirstName) == NormalizeString(user2.FirstName) &&
                   NormalizeString(user1.LastName) == NormalizeString(user2.LastName) &&
                   NormalizeString(user1.UserName) == NormalizeString(user2.UserName) &&
                   NormalizeString(user1.Email) == NormalizeString(user2.Email) &&
                   user1.PasswordHash == user2.PasswordHash &&
                   user1.PhoneNumber == user2.PhoneNumber;

                   //AddressUser1.Id == AddressUser2.Id &&
                   //NormalizeString(AddressUser1.Street) == NormalizeString(AddressUser2.Street) &&
                   //NormalizeString(AddressUser1.Number) == NormalizeString(AddressUser2.Number) &&
                   //NormalizeString(AddressUser1.Complement) == NormalizeString(AddressUser2.Complement) &&
                   //NormalizeString(AddressUser1.Neighborhood) == NormalizeString(AddressUser2.Neighborhood) &&
                   //NormalizeString(AddressUser1.City) == NormalizeString(AddressUser2.City) &&
                   //NormalizeString(AddressUser1.State) == NormalizeString(AddressUser2.State) &&
                   //NormalizeString(AddressUser1.PostalCode) == NormalizeString(AddressUser2.PostalCode);
        }

        public string NormalizeString(string? input)
        {
            if (input == null)
                return string.Empty;
            else
                return input.Trim().Normalize(NormalizationForm.FormKC).ToLowerInvariant();
        }

        public async Task<IEnumerable<User?>> Delete(IEnumerable<Guid> id, IEnumerable<User> user, CancellationToken cancellationToken)
        {

            return await Task.FromResult(user);
        }

        public async Task<IEnumerable<User?>> GetById(IEnumerable<Guid> id, IEnumerable<User> user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user);
        }

        public async Task<IEnumerable<User?>> GetAll(IEnumerable<User> user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user);
        }

        public async Task<bool> ExistsAsync(bool id, bool existsName, IEnumerable<User> users, CancellationToken cancellationToken)
        {
            try
            {
                var userId = users.Select(u => u.Id).ToList();
                var userName = users.Select(u => u.UserName).ToList();



                return await Task.FromResult(id);
            }
            catch (DomainException)
            {

                throw;
            }

        }
    }
}
