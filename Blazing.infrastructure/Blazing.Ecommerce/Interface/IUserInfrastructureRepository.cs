using Blazing.Application.Dto;

namespace Blazing.Ecommerce.Interface
{
    public interface IUserInfrastructureRepository
    {
        Task<IEnumerable<UserDto?>> AddUsers(IEnumerable<UserDto> userDto, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto?>> UpdateUsers(IEnumerable<Guid> id, IEnumerable<UserDto?> userDto,
            CancellationToken cancellationToken);

        Task<IEnumerable<UserDto?>> DeleteUsers(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto?>> GetUsersById(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto?>> GetAllUsers(int page, int pageSize, CancellationToken cancellationToken);
    }
}
