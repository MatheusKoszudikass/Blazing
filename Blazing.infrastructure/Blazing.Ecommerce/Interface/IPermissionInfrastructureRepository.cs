using Blazing.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Ecommerce.Interface
{
    public interface IPermissionInfrastructureRepository
    {
        Task<bool> AddPermissions(IEnumerable<PermissionDto> permissionDto, CancellationToken cancellationToken);

        Task<IEnumerable<PermissionDto>> UpdatePermissions(IEnumerable<Guid> id, IEnumerable<PermissionDto> userDto,
            CancellationToken cancellationToken);

        Task<IEnumerable<PermissionDto>> DeletePermissions(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<IEnumerable<PermissionDto>> GetUsersById(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<IEnumerable<PermissionDto>> GetAllPermissions(int page, int pageSize, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(IEnumerable<PermissionDto> productDto, CancellationToken cancellationToken);
    }
}
