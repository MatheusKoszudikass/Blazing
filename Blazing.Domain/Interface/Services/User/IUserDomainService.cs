namespace Blazing.Domain.Interface.Services.User
{
    public interface IUserDomainService : ICrudDomainService<Entities.User>
    {
        Task<bool> UserExistsAsync(bool id, bool userName, bool userEmail, IEnumerable<Entities.User> users, CancellationToken cancellationToken);
    }
}
