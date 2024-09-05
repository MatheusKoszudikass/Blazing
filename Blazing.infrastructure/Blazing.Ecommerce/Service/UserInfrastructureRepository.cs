using Blazing.Application.Dto;
using Blazing.Application.Interfaces.User;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.User;
using Blazing.Ecommerce.Dependency;
using Blazing.Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blazing.Ecommerce.Service
{
    #region InfraEcommerceUserDtoRepository.

    public class UserInfrastructureRepository(IUserAppService<UserDto> userAppService, DependencyInjection dependencyInjection) : IUserInfrastructureRepository
    {
        private readonly IUserAppService<UserDto> _userAppService = userAppService;
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;

        /// <summary>
        /// Adds a collection of userDto to the repository.
        /// </summary>
        /// <param name="userDtos">A collection of <see cref="UserDto"/> objects representing the userDto to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="UserDto"/> that were added.</returns>
        public async Task<IEnumerable<UserDto?>> AddUsers(IEnumerable<UserDto> userDtos, CancellationToken cancellationToken)
        {
            await ExistsAsync(userDtos, cancellationToken);

            var usersResult = await _userAppService.AddUsers(userDtos, cancellationToken);

            var users = _dependencyInjection._mapper.Map<IEnumerable<User>>(usersResult);

            await _dependencyInjection._appContext.Users.AddRangeAsync(users, cancellationToken);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            usersResult = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(users);
            return usersResult;
        }

        /// <summary>
        /// Updates a UserDto in the repository.
        /// </summary>
        /// <param name="id">The ID of the UserDto to update.</param>
        /// <param name="usersDtoUpdate">The updated UserDto.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated UserDto.</returns>
        public async Task<IEnumerable<UserDto?>> UpdateUsers(IEnumerable<Guid> id, IEnumerable<UserDto> usersDtoUpdate, CancellationToken cancellationToken)
        {
            if (!id.Any() || Guid.Empty == id.First())
            {
                throw DomainException.IdentityInvalidException.Identities(id);
            }

            // Obtém os usuários existentes com suas propriedades relacionadas carregadas
            var existingUsers = await _dependencyInjection._appContext.Users
                                             .Include(u => u.Addresses)
                                             .Include(u => u.ShoppingCarts)
                                             .Where(u => id.Contains(u.Id)).ToListAsync(cancellationToken);

            if (!existingUsers.Any())
            {
                throw UserException.UserNotFoundException.UserNotFound(existingUsers);
            }

            var usersExistingDto = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(existingUsers);

            usersDtoUpdate = await _userAppService.UpdateUsers(id, usersExistingDto, usersDtoUpdate, cancellationToken );
            
            var users = _dependencyInjection._mapper.Map<IEnumerable<User>>(usersDtoUpdate);

            foreach (var userDto in usersDtoUpdate)
            {
                var existingUser = existingUsers.SingleOrDefault(u => u.Id == userDto.Id);
                await UpdateUserDetailsAsync(userDto, existingUser, cancellationToken);
                if (existingUser == null) continue;

                // Atualiza as propriedades do usuário
                existingUser.Status = userDto.Status;
                existingUser.FirstName = userDto.FirstName;
                existingUser.LastName = userDto.LastName;
                existingUser.UserName = userDto.UserName;
                existingUser.Email = userDto.Email;
                existingUser.PhoneNumber = userDto.PhoneNumber;
                if (!string.IsNullOrEmpty(userDto.PasswordHash))
                {
                    existingUser.PasswordHash = userDto.PasswordHash;
                }
                existingUser.DataUpdated = userDto.DataUpdated;

                if (userDto.Addresses != null)
                {

                    // Aqui mapeia e aplica as alterações nos endereços
                    _dependencyInjection._mapper.Map(userDto.Addresses, existingUser.Addresses);
                }
            }

            // Salva as alterações no banco de dados
            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            // Retorna os DTOs atualizados
            return usersDtoUpdate;
        }

        /// <summary>
        /// Updates the details of a user asynchronously.
        /// </summary>
        /// <param name="originalUserDto">The original user DTO.</param>
        /// <param name="updatedUserDto">The updated user DTO.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="UserException.UserAlreadyExistsException">
        /// Thrown if the users name or email already exists and is different from the existing user.
        /// </exception>
        private async Task UpdateUserDetailsAsync(UserDto originalUserDto, User updatedUserDto, CancellationToken cancellationToken)
        {
            var userNameExists  = await IsUserNameExistsAsync(originalUserDto.UserName, cancellationToken);
            var emailExists = await IsUserEmailExistsAsync(originalUserDto.Email, cancellationToken);

            if (userNameExists)
            {
                if(originalUserDto.UserName == updatedUserDto.UserName)
                {
                }
                else
                {
                    throw UserException.UserAlreadyExistsException.FromNameExistingUser(updatedUserDto.UserName.ToString());
                }
            }
            if(emailExists)
            {
                if (originalUserDto.Email == updatedUserDto.Email)
                {

                }
                else
                {
                    throw UserException.UserAlreadyExistsException.FromEmailExistingUser(originalUserDto.Email.ToString());

                }
            }
        }

        /// <summary>
        /// Deletes users from the database based on their IDs.
        /// </summary>
        /// <param name="id">The IDs of the users to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The deleted users as UserDto objects.</returns>
        public async Task<IEnumerable<UserDto?>> DeleteUsers(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var users = await _dependencyInjection._appContext.Users
                                 .Include(u => u.Addresses)
                                 .Include(u => u.ShoppingCarts)
                                 .Where(p => id.Contains(p.Id)).ToListAsync(cancellationToken);

            foreach (var user in users)
            {
                var address = user?.Addresses?.FirstOrDefault(a => a.UserId == user.Id);
                if (address != null)
                {
                    _dependencyInjection._appContext.Addresses.Remove(address);

                }

                var shoppingCarts = user?.ShoppingCarts?.FirstOrDefault(a => a.UserId == user.Id);
                if (shoppingCarts != null)
                {
                    _dependencyInjection._appContext.ShoppingCart.Remove(shoppingCarts);
                }
            }

            var usersDto = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(users);

            usersDto = await _userAppService.DeleteUsers(id, usersDto, cancellationToken);

            _dependencyInjection._appContext.Users.RemoveRange(users);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return usersDto;
        }

        /// <summary>
        /// Retrieves a collection of UserDto objects from the database based on their IDs.
        /// </summary>
        /// <param name="id">The IDs of the users to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of UserDto objects corresponding to the given IDs.</returns>
        public async Task<IEnumerable<UserDto?>> GetUsersById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var users = await _dependencyInjection._appContext.Users
                                .Include(a => a.Addresses)
                                .Where(p => id.Contains(p.Id)).ToListAsync(cancellationToken);

            var usersResultDto = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(users);

            var result = await _userAppService.GetUserById(id, usersResultDto, cancellationToken);

            return result;
        }

        /// <summary>
        /// Retrieves all UserDto objects from the repository.
        /// This method includes related entities such as addresses.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of UserDto objects.</returns>
        public async Task<IEnumerable<UserDto?>> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _dependencyInjection._appContext.Users
                                .Include(a => a.Addresses)
                                .ToListAsync(cancellationToken);

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(users);

            var usersResult = await _userAppService.GetAllUsers(productResultDto, cancellationToken);

            return usersResult;
        }

        /// <summary>
        /// Checks if the specified userDto exist in the repository.
        /// </summary>
        /// <param name="userDto">A collection of UserDto objects to check for existence.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result indicating whether the userDto exist (
        /// true if they exist, false otherwise).
        /// </returns>
        /// <exception cref="UserException.UserAlreadyExistsException">
        /// Thrown if the userDto already exists in the repository.
        /// </exception>
        public async Task<bool> ExistsAsync(IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            var user = _dependencyInjection._mapper.Map<IEnumerable<User>>(userDto);

            foreach (var item in user)
            {
                var resultIsUserId = await IsUserIdExistsAsync(item.Id.ToString(), cancellationToken);

                var resultIsUserName = await IsUserNameExistsAsync(item.UserName, cancellationToken);

                var resultIsUserEmail = await IsUserEmailExistsAsync(item.Email, cancellationToken);

                await _userAppService.ExistsUsers(resultIsUserId, resultIsUserName, resultIsUserEmail, userDto, cancellationToken);
            }

            return false;
        }

        /// <summary>
        /// Checks if a user with the specified ID exists in the database.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user exists.</returns>
        private async Task<bool> IsUserIdExistsAsync(string userId, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(userId);
            return await _dependencyInjection._appContext.Users.AnyAsync(u => u.Id == id, cancellationToken);
        }

        /// <summary>
        /// Checks if a user with the specified username exists in the database.
        /// </summary>
        /// <param name="userName">The username of the user to check.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user exists.</returns>
        private async Task<bool> IsUserNameExistsAsync(string? userName, CancellationToken cancellationToken)
        {
            return await _dependencyInjection._appContext.Users.AnyAsync(u => u.UserName == userName, cancellationToken);
        }

        /// <summary>
        /// Checks if a user with the specified email exists in the database.
        /// </summary>
        /// <param name="email">The email of the user to check.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user exists.</returns>
        private async Task<bool> IsUserEmailExistsAsync(string? email, CancellationToken cancellationToken)
        {
            return await _dependencyInjection._appContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
#endregion
}
