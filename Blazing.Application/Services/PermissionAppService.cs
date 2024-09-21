using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Application.Interface.Permission;
using Blazing.Domain.Entities;
using Blazing.Domain.Interface.Services;

namespace Blazing.Application.Services
{
    public class PermissionAppService(IMapper mapper, ICrudDomainService<Permission> crudDomainService) : IPermissionAppService
    {

        private readonly IMapper _mapper = mapper;
        private readonly ICrudDomainService<Permission> _crudDomainService = crudDomainService;

        /// <summary>
        /// Adds a collection of permissions using the provided Permission DTOs.
        /// Maps the Permission DTOs to domain Permission entities, invokes the add operation, 
        /// and then maps the result back to Permission DTOs for the response.
        /// </summary>
        /// <param name="permissionDto">The collection of Permission DTOs to be added.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation, containing the added Permission DTOs.</returns>

        public async Task<IEnumerable<PermissionDto?>> AddPermission(IEnumerable<PermissionDto> permissionDto, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<IEnumerable<Permission>>(permissionDto);

            var permissionResult = await _crudDomainService.Add(permission, cancellationToken);

            return _mapper.Map<IEnumerable<PermissionDto>>(permissionResult);
        }

        /// <summary>
        /// Updates a collection of permissions identified by their IDs using the provided Permission DTOs.
        /// Maps the original and updated Permission DTOs to domain Permission entities, invokes the update operation, 
        /// and then maps the result back to Permission DTOs for the response.
        /// </summary>
        /// <param name="id">The collection of IDs corresponding to the permissions to be updated.</param>
        /// <param name="permissionDto">The original Permission DTOs before the update.</param>
        /// <param name="permissionDtoUpdate">The updated Permission DTOs containing new values.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation, containing the updated Permission DTOs.</returns>
        public async Task<IEnumerable<PermissionDto?>> UpdatePermission(IEnumerable<Guid> id, List<PermissionDto> permissionDto, List<PermissionDto> permissionDtoUpdate, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<IEnumerable<Permission>>(permissionDto);
            var permissionUpdate = _mapper.Map<IEnumerable<Permission>>(permissionDtoUpdate);

            var permissionResult = await _crudDomainService.Update(id, permission, permissionUpdate, cancellationToken);

            return _mapper.Map<IEnumerable<PermissionDto>>(permissionResult);
        }

        /// <summary>
        /// Retrieves a collection of permissions based on the provided IDs.
        /// Maps the incoming Permission DTOs to domain Permission entities, invokes the retrieval operation,
        /// and then maps the result back to Permission DTOs for the response.
        /// </summary>
        /// <param name="id">The collection of IDs corresponding to the permissions to retrieve.</param>
        /// <param name="permissionDto">The Permission DTOs to be used for mapping.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation, containing the retrieved Permission DTOs.</returns>
        public async Task<IEnumerable<PermissionDto?>> GetById(IEnumerable<Guid> id, IEnumerable<PermissionDto> permissionDto, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<IEnumerable<Permission>>(permissionDto);

            var permissionResult = await _crudDomainService.GetById(id, permission, cancellationToken);

            return _mapper.Map<IEnumerable<PermissionDto>>(permissionResult);
        }

        /// <summary>
        /// Retrieves all permissions from the data source.
        /// Maps the incoming collection of Permission DTOs to domain Permission entities,
        /// invokes the retrieval operation, and then maps the result back to Permission DTOs for the response.
        /// </summary>
        /// <param name="permissionDto">The collection of Permission DTOs to be used for mapping.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation, containing the collection of retrieved Permission DTOs.</returns>
        public async Task<IEnumerable<PermissionDto?>> GetAll(IEnumerable<PermissionDto?> permissionDto, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<IEnumerable<Permission>>(permissionDto);

            var permissionResult = await _crudDomainService.GetAll(permission, cancellationToken);

            return _mapper.Map<IEnumerable<PermissionDto>>(permissionResult);
        }

        /// <summary>
        /// Deletes specified permissions from the data source.
        /// Maps the incoming collection of Permission DTOs to domain Permission entities,
        /// invokes the deletion operation, and then maps the result back to Permission DTOs for the response.
        /// </summary>
        /// <param name="id">The collection of identifiers for the permissions to be deleted.</param>
        /// <param name="permissionDto">The collection of Permission DTOs to be used for mapping.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation, containing the collection of deleted Permission DTOs.</returns>

        public async Task<IEnumerable<PermissionDto?>> DeletePermission(IEnumerable<Guid> id, IEnumerable<PermissionDto> permissionDto, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<IEnumerable<Permission>>(permissionDto);

            var permissionResult = await _crudDomainService.Delete(id, permission, cancellationToken);

            return _mapper.Map<IEnumerable<PermissionDto>>(permissionResult);
        }

        /// <summary>
        /// Checks for the existence of permissions based on specified criteria.
        /// Maps the incoming collection of Permission DTOs to domain Permission entities,
        /// and calls the underlying service to determine if the permissions exist.
        /// </summary>
        /// <param name="id">Indicates whether to check for the existence of permissions by ID.</param>
        /// <param name="nameExists">Indicates whether to check for the existence of permissions by name.</param>
        /// <param name="permissionDto">The collection of Permission DTOs used for mapping.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean indicating the existence of the permissions.</returns>

        public async Task<bool?> ExistsPermission(bool id, bool nameExists, IEnumerable<PermissionDto?> permissionDto, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<IEnumerable<Permission>>(permissionDto);

            var permissionResult = await _crudDomainService.ExistsAsync(id, nameExists, permission, cancellationToken);

            return permissionResult;
        }

    }
}
