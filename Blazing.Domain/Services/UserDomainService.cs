using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.User;
using System.Linq;
using System.Text;
using Blazing.Domain.Interface.Services.User;

namespace Blazing.Domain.Services
{
    #region UserDomainServiceLogic 

    public class UserDomainService : IUserDomainService
    {
        /// <summary>
        /// Adds a list of user to the repository.
        /// </summary>
        /// <param name="user">The list of user to be added.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The list of user that have been added.</returns>
        /// <exception cref="DomainException.NotFoundException.FoundException">Thrown when the user list is null or empty.</exception>
        public async Task<IEnumerable<User>> Add(IEnumerable<User> user, CancellationToken cancellationToken)
        {
            if (user == null || !user.Any())
                throw DomainException.NotFoundException.FoundException();

            try
            {
                foreach (var item in user)
                {
                    item.DataCreated = DateTime.Now;
                    item.DataUpdated = null;
                    item.DataDeleted = null;

                    var address = item.Addresses?.FirstOrDefault();
                    if (address == null) continue;
                    address.DataCreated = DateTime.Now;
                    address.DataUpdated = null;
                    address.DataDeleted = null;
                }
                await Task.CompletedTask;
                return user;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a collection of users based on the provided IDs and user data.
        /// </summary>
        /// <param name="id">The IDs of the users to update.</param>
        /// <param name="originalUsers">The collection of existing users with the current data.</param>
        /// <param name="updatedUsers">The collection of users containing the updated data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of updated users that were found and successfully updated.</returns>
        /// <exception cref="DomainException.IdentityInvalidException.Identities">Thrown when the provided IDs are invalid or empty.</exception>
        /// <exception cref="DomainException.NotFoundException.FoundException">Thrown when the user list is null or empty.</exception>
        /// <exception cref="UserException.UserAlreadyExistsException.FromExistingUsers">Thrown when the updated user collection is identical to the existing user collection.</exception>
        public async Task<IEnumerable<User>> Update(IEnumerable<Guid> id, IEnumerable<User> originalUsers , IEnumerable<User> updatedUsers, CancellationToken cancellationToken)
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
                        usersDict[update.Key].Status = updatedUser.Status;
                        usersDict[update.Key].FirstName = updatedUser.FirstName;
                        usersDict[update.Key].LastName = updatedUser.LastName;
                        usersDict[update.Key].UserName = updatedUser.UserName;
                        usersDict[update.Key].Email = updatedUser.Email;
                        usersDict[update.Key].PhoneNumber = updatedUser.PhoneNumber;
                        if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
                        {
                            usersDict[update.Key].PasswordHash = updatedUser.PasswordHash;
                        }
                        updatedUser.DataCreated = usersDict[update.Key].DataCreated;
                        updatedUser.DataUpdated = DateTime.Now;
                        if (updatedUser.Addresses != null)
                        {
                            UpdateDetailsAddress(updatedUser.Addresses ?? throw UserException.UserNotFoundException.UserAddressNotFound(updatedUser),
                                usersDict[update.Key].Addresses ?? throw UserException.UserNotFoundException.UserAddressNotFound(usersDict[update.Key]));
                        }

                        return updatedUser;
                    }).ToList();

                if (modifiedUser.Count == 0)
                    throw UserException.UserAlreadyExistsException.FromExistingUsers(updatedUsers);

                await Task.CompletedTask;
                return modifiedUser;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the details of the addresses in the updatedAddress collection based on the addresses in the address collection.
        /// </summary>
        /// <param name="updatedAddress">The collection of addresses to update.</param>
        /// <param name="originalAddresses">The collection of addresses to use as a reference for updating.</param>
        private static void UpdateDetailsAddress(IEnumerable<Address> updatedAddress, IEnumerable<Address> originalAddresses)
        {
           var addressList = originalAddresses.ToList();

            if (originalAddresses == null || updatedAddress == null) return;
            foreach (var item in updatedAddress)
            {
                var existingAddress = originalAddresses.FirstOrDefault(a => a.UserId == item.UserId);
                if (existingAddress != null)
                {
                    existingAddress.UserId = item.UserId;
                    existingAddress.Street = item.Street;
                    existingAddress.Number = item.Number;
                    existingAddress.Complement = item.Complement;
                    existingAddress.Neighborhood = item.Neighborhood;
                    existingAddress.City = item.City;
                    existingAddress.State = item.State;
                    existingAddress.PostalCode = item.PostalCode;
                    item.DataCreated = existingAddress.DataCreated;
                    item.DataUpdated = DateTime.Now;
                    item.DataDeleted = null;
                }
                else
                {
                    item.DataCreated = DateTime.Now;
                    item.DataUpdated = DateTime.Now;
                    addressList.Add(item);
                }
            }
        }

        /// <summary>
        /// Compares two users to determine if they are equal based on their properties.
        /// </summary>
        /// <param name="user1">The first user to compare.</param>
        /// <param name="user2">The second user to compare.</param>
        /// <returns><c>true</c> if the users have the same values for all relevant properties; otherwise, <c>false</c>.</returns>
        private static bool AreUseEqual(Entities.User user1, Entities.User user2)
        {
            if (user1 == null || user2 == null)
                return false;

            var AddressUser1 = user1.Addresses?.FirstOrDefault(a => a.UserId == user1.Id);
            var AddressUser2 = user2.Addresses?.FirstOrDefault(a => a.UserId == user2.Id);

            if (AddressUser1 == null)
            {
                if (AddressUser2 != null)
                {
                    return false;
                }
            }

            if (AddressUser1 == null || AddressUser2 == null)
                return false;

            return user1.Id == user2.Id &&
                   user1.Status == user2.Status &&
                   NormalizeString(user1.FirstName) == NormalizeString(user2.FirstName) &&
                   NormalizeString(user1.LastName) == NormalizeString(user2.LastName) &&
                   NormalizeString(user1.UserName) == NormalizeString(user2.UserName) &&
                   NormalizeString(user1.Email) == NormalizeString(user2.Email) &&
                   user1.PhoneNumber == user2.PhoneNumber &&

                   AddressUser1.UserId == AddressUser2.UserId &&
                   NormalizeString(AddressUser1.Street) == NormalizeString(AddressUser2.Street) &&
                   NormalizeString(AddressUser1.Number) == NormalizeString(AddressUser2.Number) &&
                   NormalizeString(AddressUser1.Complement) == NormalizeString(AddressUser2.Complement) &&
                   NormalizeString(AddressUser1.Neighborhood) == NormalizeString(AddressUser2.Neighborhood) &&
                   NormalizeString(AddressUser1.City) == NormalizeString(AddressUser2.City) &&
                   NormalizeString(AddressUser1.State) == NormalizeString(AddressUser2.State) &&
                   NormalizeString(AddressUser1.PostalCode) == NormalizeString(AddressUser2.PostalCode);
        }

        /// <summary>
        /// Normalizes a string by trimming leading and trailing whitespace, normalizing it to the specified form,
        /// and converting it to lowercase.
        /// </summary>
        /// <param name="input">The string to normalize.</param>
        private static string NormalizeString(string? input)
        {
            return input == null ? string.Empty : input.Trim().Normalize(NormalizationForm.FormKC).ToLowerInvariant();
        }

        /// <summary>
        /// Deletes users based on the provided IDs.
        /// </summary>
        /// <param name="id">The IDs of the users to delete.</param>
        /// <param name="user">The collection of users to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the deleted users.</returns>
        /// <exception cref="DomainException.IdentityInvalidException.Identities">Thrown if the provided IDs are invalid or empty.</exception>
        /// <exception cref="UserException.UserNotFoundException.UserNotFound">Thrown if the user collection is null or empty.</exception>
        public async Task<IEnumerable<User>> Delete(IEnumerable<Guid> id, IEnumerable<User> user, CancellationToken cancellationToken)
        {
            var enumerable = user.ToList();

            if (id == null || Guid.Empty == id.FirstOrDefault() || !id.Any())
                throw DomainException.IdentityInvalidException.Identities(id ?? []);

            if (user == null || !enumerable.Any())
                throw UserException.UserNotFoundException.UserNotFound(enumerable);
            try
            {
                foreach (var item in enumerable)
                {
                    item.PasswordHash = null;
                }

                await Task.CompletedTask;
                return enumerable;
            }
            catch (DomainException )
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves users from the database based on the provided IDs.
        /// </summary>
        /// <param name="id">The IDs of the users to retrieve.</param>
        /// <param name="users">The collection of users to search for.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of User objects.</returns>
        /// <exception cref="DomainException.IdentityInvalidException.Identities">Thrown when the provided IDs are invalid or empty.</exception>
        /// <exception cref="UserException.UserNotFoundException.UserNotFound">Thrown when no users matching the provided IDs are found in the user collection.</exception>
        public async Task<IEnumerable<User>> GetById(IEnumerable<Guid> id, IEnumerable<User> users, CancellationToken cancellationToken)
        {
            var usersManager = users.ToList();
            var byId = id.ToList();
            if (id == null || !byId.Any() || Guid.Empty == byId.FirstOrDefault())
                throw DomainException.IdentityInvalidException.Identities(id ?? []);
            if (users == null || !byId.Any())
                throw UserException.UserNotFoundException.UserNotFound(usersManager);

            try
            {

                foreach (var user in usersManager)
                {
                    user.PasswordHash = null;
                }

                await Task.CompletedTask;
                return usersManager;
            }
            catch (DomainException )
            {
                throw;
            }

        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <param name="users">A collection of User objects to retrieve.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of User objects.</returns>
        /// <exception cref="DomainException.NotFoundException">Thrown when there are no users to retrieve or if the input collection is null or empty.</exception>
        public async Task<IEnumerable<User>> GetAll(IEnumerable<User> users, CancellationToken cancellationToken)
        {
            if(users is null || !users.Any())
                throw DomainException.NotFoundException.FoundException();

            try
            {
                foreach (var user in users)
                {
                    user.PasswordHash = null;
                }

                await Task.CompletedTask;
                return users;
            }
            catch (DomainException)
            {
                throw;
            }

        }

        /// <summary>
        /// Checks if a user with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">A boolean indicating whether the ID exists.</param>
        /// <param name="userName"></param>
        /// <param name="users">A collection of User objects to check for existence.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the user exists (true if the ID exists, false otherwise).</returns>
        /// <exception cref="DomainException">Thrown when there is an error checking for the existence of the user.</exception>
        public async Task<bool> ExistsAsync(bool id, bool userName, IEnumerable<User> users,
            CancellationToken cancellationToken) {await Task.CompletedTask;return true;}

        /// <summary>
        /// Checks if a user with the specified ID, username, or email already exists in the database.
        /// </summary>
        /// <param name="id">A boolean indicating whether the ID exists.</param>
        /// <param name="userName">A boolean indicating whether the username exists.</param>
        /// <param name="email">A boolean indicating whether the email exists.</param>
        /// <param name="users">A collection of User objects to check for existence.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the user exists (true if the ID, username, or email exists, false otherwise).</returns>
        /// <exception cref="UserException.UserAlreadyExistsException">Thrown when the user with the specified ID, username, or email already exists.</exception>
        /// <exception cref="DomainException">Thrown when there is an error checking for the existence of the user.</exception>
        public Task<bool> UserExistsAsync(bool id, bool userName, bool email, IEnumerable<User> users, CancellationToken cancellationToken)
        {
            try
            {
                var enumerable = users.ToList();
                return Task.FromResult(users != null && enumerable.Any()  switch
                {
                    true when id => throw UserException.UserAlreadyExistsException.FromExistingId(enumerable.Select(u => u.Id).ToString()),
                    true when userName => throw UserException.UserAlreadyExistsException.FromNameExistingUser(enumerable.Select(u => u.UserName).ToString()),
                    true when email => throw UserException.UserAlreadyExistsException.FromEmailExistingUser(enumerable.Select(u => u.Email).ToString()),
                    _ => false
                  
                });
            }
            catch (DomainException)
            {
                throw;
            }
        }
    }
#endregion
}
