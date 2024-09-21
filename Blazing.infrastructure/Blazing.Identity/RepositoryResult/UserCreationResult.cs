using Blazing.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.RepositoryResult
{
    public class UserCreationResult
    {
        public List<ApplicationUser> SuccessfulUsers { get; init; } = [];
        public IdentityError[] FailedUsers { get; set; } = [];
    }
}
