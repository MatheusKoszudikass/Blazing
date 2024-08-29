using Blazing.Application.Dto;
using Blazing.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Repository
{
    public interface IUserInfrastructureRepository
    {
        Task<UserCreationResult?> AddUsersAsync(IEnumerable<UserDto> UserDto, CancellationToken cancellationToken);

        Task<UserCreationResult?> UpdateUsersAsync(IEnumerable<Guid> id, IEnumerable<UserDto> userDto, CancellationToken cancellationToken);

        Task<UserCreationResult?> DeleteUsersAsync(IEnumerable<UserDto> userDto, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto?>> GetUsersByIdAsync(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto?>> GetAllUsersAsync(CancellationToken cancellationToken);

        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe,
            CancellationToken cancellationToken);

        Task<string> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken);

        Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken);
    }
}
