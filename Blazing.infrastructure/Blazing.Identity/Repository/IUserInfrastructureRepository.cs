using Blazing.Application.Dto;
using Blazing.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Repository
{
    #region ContratcsUserIdentity

    /// <summary>
    /// Represents a repository for managing user infrastructure.
    /// </summary>
    public interface IUserInfrastructureRepository
    {
        Task<UserCreationResult?> AddUsersAsync(IEnumerable<UserDto> UserDto, CancellationToken cancellationToken);

        Task<UserCreationResult?> UpdateUsersAsync(IEnumerable<UserDto> userDto, CancellationToken cancellationToken);

        Task<UserCreationResult?> DeleteUsersAsync(IEnumerable<string> id, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto?>> GetUsersByIdAsync(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto?>> GetAllUsersAsync(CancellationToken cancellationToken);

        Task<SignInResult> LoginAsync(Login login,
            CancellationToken cancellationToken);

        Task<string> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken);

        Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken);
    }

    #endregion
}
