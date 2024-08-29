using System.Diagnostics;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.User;
using Blazing.Ecommerce.Repository;
using Blazing.Identity.Dependency;
using Blazing.Identity.Entities;
using Blazing.Identity.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blazing.Identity.Service
{
    public class UserInfrastructureRepository(DependencyInjection dependencyInjection,
        UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        BlazingIdentityMapper mapper, IUserInfrastructureRepository userInfrastructureRepository) : Identity.Repository.IUserInfrastructureRepository
    {
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly BlazingIdentityMapper _mapper = mapper;
        private readonly IUserInfrastructureRepository _userInfrastructureRepository = userInfrastructureRepository;

        /// <summary>
        /// Adds a collection of ApplicationUser to the repository.
        /// </summary>
        /// <param name="userDto">A collection of <see cref="UserDto"/> objects representing the ApplicationUser to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="UserDto"/> that were added.</returns>

        public async Task<UserCreationResult?> AddUsersAsync(IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            var result = await _mapper.UserMapperApplicationUser(userDto);

            await ExistsAsync(result, cancellationToken);

            var userCreationResult = new UserCreationResult
            {
                SuccessfulUsers = [],
                FailedUsers = [],
            };

            foreach (var item in result)
            {
                var identityResult = await _userManager.CreateAsync(item, item.PasswordHash!);
                if (identityResult.Succeeded)
                {
                    userCreationResult.SuccessfulUsers.Add(item);
                }
                else
                {
                    userCreationResult.FailedUsers = identityResult.Errors.ToArray();
                }

            }

            var userDtos = await _mapper.UserMapperUserDto(userCreationResult.SuccessfulUsers);

            var resultAdded = await _userInfrastructureRepository.AddUsers(userDtos, cancellationToken);

            return await Task.FromResult(userCreationResult);
        }

        public async Task<UserCreationResult?> UpdateUsersAsync(IEnumerable<Guid> id, IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            if (id == null || !id.Any())
                throw DomainException.IdentityInvalidException.Identities(id ?? []);

            if (userDto == null || !id.Any())
                throw DomainException.NotFoundException.FoundException();


            var result = await _mapper.UserMapperApplicationUser(userDto);
            await ExistsAsync(result, cancellationToken);


            var userCreationResult = new UserCreationResult
            {
                SuccessfulUsers = [],
                FailedUsers = [],
            };

            foreach (var item in result)
            {
                var resultIdentity = await _userManager.UpdateAsync(item);
                if (resultIdentity.Succeeded)
                {
                    userCreationResult.SuccessfulUsers.Add(item);
                }
                else
                {
                    userCreationResult.FailedUsers = resultIdentity.Errors.ToArray();
                }
            }

            return await Task.FromResult(userCreationResult);
        }

        public async Task<UserCreationResult?> DeleteUsersAsync(IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            if (userDto == null || !userDto.Any())
                throw DomainException.NotFoundException.FoundException();

            var result = await _mapper.UserMapperApplicationUser(userDto);

            var userCreationResult = new UserCreationResult
            {
                SuccessfulUsers = [],
                FailedUsers = [],
            };

            foreach (var item in result)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var resultIdentity = await _userManager.DeleteAsync(item);
                if (resultIdentity.Succeeded)
                {
                    userCreationResult.SuccessfulUsers.Add(item);
                }
                else
                {
                    userCreationResult.FailedUsers = resultIdentity.Errors.ToArray();
                }

                
            }

            return await Task.FromResult(userCreationResult);
        }

        /// <summary>
        /// Retrieves categories by their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> objects that match the specified IDs.</returns>
        public async Task<IEnumerable<UserDto?>> GetUsersByIdAsync(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var userIdentity = await _dependencyInjection._appContext.Users
                                 .Where(u => id.Contains(u.Id)).ToListAsync(cancellationToken);

            var resultDto = await _mapper.UserMapperUserDtoGet(userIdentity);

            return await Task.FromResult(resultDto);
        }

        public async Task<IEnumerable<UserDto?>> GetAllUsersAsync(CancellationToken cancellationToken)
        {

           var resultIdentity = await _userManager.Users.ToListAsync(cancellationToken);

           var result = await _mapper.UserMapperUserDtoGet(resultIdentity);
           
            if (!result.Any())
            {
                throw DomainException.NotFoundException.FoundException();
            }

            return await Task.FromResult(result);
        }


        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe,
            CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true);

            return await Task.FromResult(result);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw UserException.UserNotFaundException.UserNotFoundEmail(email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return await Task.FromResult(token);

        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw UserException.UserNotFaundException.UserNotFoundEmail(email);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);


            return await Task.FromResult(result);

        }

        /// <summary>
        /// Checks if the specified userDto exist in the repository.
        /// </summary>
        /// <param name="userIdentity">A collection of <see cref="ApplicationUser"/> objects to check for existence.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the userDto exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>

        public async Task<bool> ExistsAsync(IEnumerable<ApplicationUser> userIdentity, CancellationToken cancellationToken)
        {
            //var users = _dependencyInjection._mapper.Map<IEnumerable<User>>(userIdentity);

            var resultId = await _dependencyInjection._appContext.Users.AnyAsync(u => userIdentity.Select(c => c.Id).Contains(u.Id));

            var resultName = await _dependencyInjection._appContext.Users.AnyAsync(u => userIdentity.Select(c => c.UserName).Contains(u.UserName));

            var resultEmail = await _dependencyInjection._appContext.Users.AnyAsync(u => userIdentity.Select(c => c.Email).Contains(u.Email));

            //var usersDto = _dependencyInjection._mapper.Map<IEnumerable<UserDto>>(users);

            //await _userInfraService.ExistsAsync(resultId, resultName, resultEmail, usersDto, cancellationToken);

            return await Task.FromResult(resultId);
        }
    }
}
