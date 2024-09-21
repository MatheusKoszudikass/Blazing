using Blazing.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Interface.Permission
{
    /// <summary>
    /// Interface defining application services for managing permissions.
    /// Provides methods for adding, updating, deleting, retrieving, and checking the existence of permissions.
    /// </summary>
    public interface IPermissionAppService
    {
        Task<IEnumerable<PermissionDto?>> AddPermission(IEnumerable<PermissionDto> permissionDto, 
            CancellationToken cancellationToken);

        Task<IEnumerable<PermissionDto?>> UpdatePermission(IEnumerable<Guid> id, 
            List<PermissionDto> permissionDto, List<PermissionDto> permissionDtoUpdate, CancellationToken cancellationToken);

        Task<IEnumerable<PermissionDto?>> DeletePermission(IEnumerable<Guid> id,
            IEnumerable<PermissionDto> permissionDto, CancellationToken cancellationToken);

        Task<IEnumerable<PermissionDto?>> GetById(IEnumerable<Guid> id, 
            IEnumerable<PermissionDto> permissionDto, CancellationToken cancellationToken);

        Task<IEnumerable<PermissionDto?>> GetAll(IEnumerable<PermissionDto?> permissionDto, CancellationToken cancellationToken);

        Task<bool?> ExistsPermission(bool id, bool nameExists, IEnumerable<PermissionDto?> permissionDto,
            CancellationToken cancellationToken);
    }
}
