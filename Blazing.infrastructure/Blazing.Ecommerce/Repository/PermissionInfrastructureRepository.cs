using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Application.Interface.Permission;
using Blazing.Domain.Entities;
using Blazing.Ecommerce.Dependencies;
using Blazing.Ecommerce.Interface;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
// ReSharper disable All

namespace Blazing.Ecommerce.Repository
{
    public class PermissionInfrastructureRepository(DependencyInjection dependencyInjection,
        IPermissionAppService permissionAppService) : IPermissionInfrastructureRepository
    {
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;
        private readonly IPermissionAppService _permissionAppService = permissionAppService;

        public async Task<bool> AddPermissions(IEnumerable<PermissionDto> permissionDto, CancellationToken cancellationToken)
        { 
            //await ExistsAsync(permissionDto, cancellationToken);
            var result = await _permissionAppService.AddPermission(permissionDto, cancellationToken);

            var permission = _dependencyInjection._mapper.Map<IEnumerable<Permission>>(result);

            await _dependencyInjection._appContext.Permissions.AddRangeAsync(permission, cancellationToken);
            
            var resultSaved = await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);
            if (resultSaved > 0) 
                return false;

            return true;
        }
        public Task<IEnumerable<PermissionDto>> UpdatePermissions(IEnumerable<Guid> id, IEnumerable<PermissionDto> userDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionDto>> DeletePermissions(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionDto>> GetAllPermissions(int page, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(IEnumerable<PermissionDto> productDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionDto>> GetUsersById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
