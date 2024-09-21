using Blazing.Application.Dto;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.User;
using Blazing.Identity.Dependencies;
using Blazing.Identity.Entities;
using Blazing.Identity.Mappings;
using Blazing.Identity.RepositoryResult;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static System.Text.RegularExpressions.Regex;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blazing.Identity.Repository
{

    #region UserInfraIdentityRepository

    public class UserInfrastructureRepository(
        IMemoryCache memoryCache,
        DependencyInjection dependencyInjection,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        BlazingIdentityMapper mapper,
        Ecommerce.Interface.IUserInfrastructureRepository userInfrastructureRepository) : Identity.Interface.IUserInfrastructureRepository
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly BlazingIdentityMapper _mapper = mapper;
        private readonly Ecommerce.Interface.IUserInfrastructureRepository _userInfrastructureRepository = userInfrastructureRepository;

        /// <summary>
        /// Asynchronously adds a collection of users to the repository.
        /// </summary>
        /// <param name="userDto">An enumerable collection of UserDto objects representing the users to be added.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a UserCreationResult object
        /// which contains a list of successful users and a list of failed users.
        /// </returns>
        /// <exception cref="DomainException">Thrown if the userDto parameter is null.</exception>
        /// <exception cref="UserException">Thrown if any of the users already exist in the repository.</exception>
        public async Task<UserCreationResult?> AddUsersAsync(IEnumerable<UserDto> userDto,
            CancellationToken cancellationToken)
        {
            if (userDto == null)
                throw DomainException.NotFoundException.FoundException();

            var result = (await _mapper.UserMapperApplicationUser(userDto, cancellationToken))
                .Where(u => true)
                .ToList();

            await ExistsAsync(result, cancellationToken);

            var userCreationResult = new UserCreationResult
            {
                SuccessfulUsers = [],
                FailedUsers = []
            };


            foreach (var item in result)
            {
                item.CreationDate = DateTime.Now;
                item.EmailConfirmed = true;
                item.LockoutEnabled = false;

                var identityResult = await _userManager.CreateAsync(item, item.PasswordHash!);
                if (identityResult.Succeeded)
                    userCreationResult.SuccessfulUsers.Add(item);
                else
                    userCreationResult.FailedUsers = identityResult.Errors.ToArray();
            }

            userDto = await _mapper.UserMapperAddedDto(userCreationResult.SuccessfulUsers!, userDto, cancellationToken);

            await _userInfrastructureRepository.AddUsers(userDto, cancellationToken);

            return userCreationResult;
        }


        /// <summary>
        /// Updates users in the repository asynchronously.
        /// </summary>
        /// <param name="userDto">An enumerable collection of UserDto objects representing the users to be updated.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a UserCreationResult object
        /// which contains a list of successful users and a list of failed users.
        /// </returns>
        /// <exception cref="DomainException">Thrown if the userDto parameter is null.</exception>
        /// <exception cref="UserException">Thrown if any of the users do not exist in the repository.</exception>
        public async Task<UserCreationResult?> UpdateUsersAsync(IEnumerable<UserDto> userDto,
            CancellationToken cancellationToken)
        {
            if (userDto == null)
                throw DomainException.NotFoundException.FoundException();

            var userCreationResult = new UserCreationResult
            {
                SuccessfulUsers = [],
                FailedUsers = []
            };

            var userDtoUpdate = await _userInfrastructureRepository.UpdateUsers(userDto.Select(u => u.Id).ToList(),
                userDto, cancellationToken);

            foreach (var item in userDtoUpdate)
            {
                var user = await _userManager.FindByIdAsync(item.Id.ToString());
                if (user == null)
                    continue;

                var result = await UpdateUserDetailsAsync(user, item, cancellationToken);
                if (result.Succeeded)
                    userCreationResult.SuccessfulUsers.Add(user);
                else
                    userCreationResult.FailedUsers = result.Errors.ToArray();
            }

            return userCreationResult;
        }

        /// <summary>
        /// Asynchronously updates the details of an application user based on the provided user DTO.
        /// </summary>
        /// <param name="user">The application user to update.</param>
        /// <param name="userDto">The user DTO containing the updated details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the result of the update operation.</returns>
        /// <exception cref="UserException.UserAlreadyExistsException">
        /// Thrown if the users name or email already exists and is different from the existing user.
        /// </exception>
        private async Task<IdentityResult> UpdateUserDetailsAsync(ApplicationUser user, UserDto userDto,
            CancellationToken cancellationToken)
        {
            var userNameExists = await IsUserNameExistsAsync(userDto.UserName, cancellationToken);
            var emailExists = await IsEmailExistsAsync(userDto.Email, cancellationToken);

            if (userNameExists)
            {
                if (user.UserName == userDto.UserName)
                {
                }
                else
                {
                    throw UserException.UserAlreadyExistsException.FromNameExistingUser(userDto.UserName.ToString());
                }
            }

            if (emailExists)
            {
                if (user.Email == userDto.Email)
                {
                }
                else
                {
                    throw UserException.UserAlreadyExistsException.FromEmailExistingUser(userDto.Email.ToString());
                }
            }

            user.FirstName = userDto.FirstName;
            user.UserName = userDto.UserName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.UserName = userDto.UserName;
            user.LastUpdate = DateTime.Now;

            return await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// Deletes users by their IDs asynchronously.
        /// </summary>
        /// <param name="id">A collection of user IDs to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result of <see cref="UserCreationResult"/>
        /// containing the list of successfully deleted users and the list of users that failed to be deleted.
        /// </returns>
        /// <exception cref="DomainException">Thrown when the provided ID collection is null or empty.</exception>
        public async Task<UserCreationResult?> DeleteUsersAsync(IEnumerable<string> id,
            CancellationToken cancellationToken)
        {
            if (id == null || !id.Any())
                throw DomainException.NotFoundException.FoundException();

            var userCreationResult = new UserCreationResult
            {
                SuccessfulUsers = [],
                FailedUsers = []
            };

            var idGuid = id.Select(Guid.Parse).ToList();
            await _userInfrastructureRepository.DeleteUsers(idGuid, cancellationToken);

            foreach (var item in id)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var user = await _userManager.FindByIdAsync(item);
                if (user == null) continue;
                var resultIdentity = await _userManager.DeleteAsync(user);
                if (resultIdentity.Succeeded)
                    userCreationResult.SuccessfulUsers.Add(user);
                else
                    userCreationResult.FailedUsers = resultIdentity.Errors.ToArray();
            }

            return userCreationResult;
        }

        /// <summary>
        /// Retrieves a collection of UserDto objects based on their IDs.
        /// </summary>
        /// <param name="id">A collection of IDs of the UserDto objects to retrieve.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of UserDto objects.</returns>
        public async Task<IEnumerable<UserDto?>> GetUsersByIdAsync(IEnumerable<Guid> id,
            CancellationToken cancellationToken)
        {
            var result = await _userInfrastructureRepository.GetUsersById(id, cancellationToken);

            return result;
        }

        /// <summary>
        /// Retrieves all UserDto objects from the repository asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of UserDto objects.</returns>
        /// <exception cref="DomainException.NotFoundException">Thrown when no UserDto objects are found in the repository.</exception>
        public async Task<IEnumerable<UserDto?>> GetAllUsersAsync(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            var resultDto = await _userInfrastructureRepository.GetAllUsers(page, pageSize, cancellationToken);

            return resultDto;
        }

        /// <summary>
        /// Asynchronously logs in a user with the provided email and password.
        /// </summary>
        /// <param name="login.LoginIdentifier ">The email of the user to log in.</param>
        /// <param name="login.password">The password of the user to log in.</param>
        /// <param name="login.rememberMe">A boolean indicating whether to remember the user's login.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the result of the login attempt.</returns>
        public async Task<SignInResult> LoginAsync(Login login, CancellationToken cancellationToken)
        {
            const string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            var isEmail = IsMatch(login.LoginIdentifier, emailPattern);


            if (isEmail)
            {
                return await LoginWithEmailAsync(login);
            }
            else
            {
                return await LoginWithUsernameAsync(login);
            }
        }

        /// <summary>
        /// Asynchronously logs in a user with the provided email and password.
        /// </summary>
        /// <param name="login">The login details of the user to log in.</param>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the result of the login attempt.</returns>
        private async Task<SignInResult> LoginWithEmailAsync(Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.LoginIdentifier);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, login.RememberMe);
            }

            if (result.IsLockedOut)
            {
                throw UserException.UserLockedOutException.FromLockedOutExceptionEmail(user.Email);
            }

            return result;
        }

        /// <summary>
        /// Asynchronously logs in a user with the provided username and password.
        /// </summary>
        /// <param name="login">The login details of the user to log in.</param>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the result of the login attempt.</returns>
        private async Task<SignInResult> LoginWithUsernameAsync(Login login)
        {
            var result =
                await _signInManager.PasswordSignInAsync(login.LoginIdentifier, login.Password, login.RememberMe, true);

            if (result.IsLockedOut)
            {
                throw UserException.UserLockedOutException.FromLockedOutExceptionUserName(login.LoginIdentifier);
            }

            return result;
        }

        /// <summary>
        /// Generates a password reset token for the user with the specified email.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>The generated password reset token.</returns>
        /// <exception cref="UserException.UserNotFoundException">Thrown if the user with the specified email is not found.</exception>
        public async Task<string> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email) ??
                       throw UserException.UserNotFoundException.UserNotFoundEmail(email);


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await Task.CompletedTask;

            return token;
        }

        /// <summary>
        /// Resets the password for a user with the specified email using the provided token and new password.
        /// </summary>
        /// <param name="email">The email of the user whose password is to be reset.</param>
        /// <param name="token">The token used to reset the password.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the password reset operation.</returns>
        /// <exception cref="UserException.UserNotFoundException.UserNotFoundEmail">Thrown when the user with the specified email is not found.</exception>
        public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword,
            CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(email) ??
                       throw UserException.UserNotFoundException.UserNotFoundEmail(email);


            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            await Task.CompletedTask;

            return result;
        }

        /// <summary>
        /// Checks if the specified userDto exist in the repository.
        /// </summary>
        /// <param name="userIdentity">A collection of <see cref="ApplicationUser" /> objects to check for existence.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        ///     A task representing the asynchronous operation, with a result indicating whether the userDto exist (
        ///     <c>true</c> if they exist, <c>false</c> otherwise).
        /// </returns>
        private async Task<bool> ExistsAsync(IEnumerable<ApplicationUser> userIdentity,
            CancellationToken cancellationToken)
        {
            var userDto = (await _mapper.UserMapperDto(userIdentity, cancellationToken))
                .ToList();

            foreach (var item in userDto)
            {
                if (await IsUserIdExistsAsync(item.Id.ToString(), cancellationToken))
                    throw UserException.UserAlreadyExistsException.FromExistingId(item.Id.ToString());

                if (await IsUserNameExistsAsync(item.UserName, cancellationToken))
                    throw UserException.UserAlreadyExistsException.FromNameExistingUser(item.UserName.ToString());

                if (await IsEmailExistsAsync(item.Email, cancellationToken))
                    throw UserException.UserAlreadyExistsException.FromEmailExistingUser(item.Email.ToString());
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
            return await _dependencyInjection.AppContext.Users.AnyAsync(u => u.Id == id, cancellationToken);
        }

        /// <summary>
        /// Checks if a user with the specified username exists in the database.
        /// </summary>
        /// <param name="userName">The username of the user to check.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user exists.</returns>
        private async Task<bool> IsUserNameExistsAsync(string? userName, CancellationToken cancellationToken)
        {
            return await _dependencyInjection.AppContext.Users.AnyAsync(u => u.UserName == userName,
                cancellationToken);
        }

        /// <summary>
        /// Checks if a user with the specified email exists in the database.
        /// </summary>
        /// <param name="email">The email of the user to check.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user exists.</returns>
        private async Task<bool> IsEmailExistsAsync(string? email, CancellationToken cancellationToken)
        {
            return await _dependencyInjection.AppContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}

#endregion