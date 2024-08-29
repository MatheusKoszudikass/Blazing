using Blazing.Application.Dto;
using Blazing.Application.Interfaces.User;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Ecommerce.Dependency;
using Blazing.Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blazing.Ecommerce.Service
{
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
            return await Task.FromResult(usersResult);
        }

        /// <summary>
        /// Updates a UserDto in the repository.
        /// </summary>
        /// <param name="id">The ID of the UserDto to update.</param>
        /// <param name="usersDtoUpdate">The updated UserDto.</param>
        /// <returns>The updated UserDto.</returns>
        public async Task<IEnumerable<UserDto?>> UpdateUsers(IEnumerable<Guid> id, IEnumerable<UserDto> usersDtoUpdate, CancellationToken cancellationToken)
        {
            if (!id.Any())
            {
                throw DomainException.IdentityInvalidException.Identities(id);
            }

            // Obtém os produtos existentes com as propriedades relacionadas carregadas
            var existingUsers = await _dependencyInjection._appContext.Users
                                            .Include(u => u.Addresses)
                                            .Include(u => u.ShoppingCarts)
                                            .Where(p => id.Contains(p.Id)).ToListAsync(cancellationToken);

            // Mapeia os produtos existentes para DTOs
            var usersDto = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(existingUsers);

            // Atualiza os produtos usando o serviço
            var productDtoUpdateResult = await _userAppService.UpdateUsers(id, usersDto, usersDtoUpdate, cancellationToken);

            // Atualiza as propriedades das entidades existentes com base nos DTOs atualizados
            foreach (var updatedUsersDto in usersDtoUpdate)
            {
                var existingUser= existingUsers.SingleOrDefault(p => p.Id == updatedUsersDto.Id);
                if (existingUser != null)
                {
                    _dependencyInjection._mapper.Map(updatedUsersDto, existingUser);
                }
            }

            // Salva as alterações no banco de dados
            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return productDtoUpdateResult;
        }

        /// <summary>
        /// Deletes UserDto by ID.
        /// </summary>
        /// <param name="id">The IDs of the UserDto to delete.</param>
        /// <returns>The deleted UserDto.</returns>
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

            await _userAppService.DeleteUsers(id, usersDto, cancellationToken);

            _dependencyInjection._appContext.Users.RemoveRange(users);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(usersDto);
        }

        /// <summary>
        /// Gets a UserDto by ID.
        /// </summary>
        /// <param name="id">The ID of the UserDto.</param>
        /// <returns>The UserDto.</returns>
        public async Task<IEnumerable<UserDto?>> GetUsersById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var users = await _dependencyInjection._appContext.Users
                                .Include(a => a.Addresses)
                                .Where(p => id.Contains(p.Id)).ToListAsync(cancellationToken);

            var usersResultDto = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(users);

             await _userAppService.GetUserById(id, usersResultDto, cancellationToken);

            return await Task.FromResult(usersResultDto);
        }

        /// <summary>
        /// Retrieves all UserDto from the database, including related entities such as address, ShoppingCarts.
        /// </summary>
        /// <returns>A collection of UserDto objects.</returns>
        public async Task<IEnumerable<UserDto?>> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _dependencyInjection._appContext.Users
                                .ToListAsync(cancellationToken);

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(users);

            var usersResult = await _userAppService.GetAllUsers(productResultDto, cancellationToken);

            return await Task.FromResult(usersResult);
        }

        /// <summary>
        /// Checks if the specified userDto exist in the repository.
        /// </summary>
        /// <param name="categoryNames">A collection of <see cref="CategoryDto"/> objects to check for existence.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the userDto exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>

        public async Task<bool> ExistsAsync(IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            var users = _dependencyInjection._mapper.Map<IEnumerable<User>>(userDto);

            var resultId = await _dependencyInjection._appContext.Users.AnyAsync(u => users.Select(c => c.Id).Contains(u.Id), cancellationToken: cancellationToken);

            var resultName = await _dependencyInjection._appContext.Users.AnyAsync(u => users.Select(c => c.UserName).Contains(u.UserName), cancellationToken: cancellationToken);

            var resultEmail = await _dependencyInjection._appContext.Users.AnyAsync(u => users.Select(c => c.Email).Contains(u.Email), cancellationToken: cancellationToken);

            await _userAppService.ExistsUsers(resultId, resultName, resultName, userDto, cancellationToken);

            return resultId;
        }
    }
}
