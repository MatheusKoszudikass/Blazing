using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Entities
{
    public class UserCreationResult
    {
        public List<ApplicationUser> SuccessfulUsers { get; set; } = [];
        public IdentityError[] FailedUsers { get; set; } = [];
    }
}
