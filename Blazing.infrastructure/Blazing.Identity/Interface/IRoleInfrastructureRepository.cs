using Blazing.Identity.Dto;
using Blazing.Identity.Interface;
using Blazing.Identity.RepositoryResult;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Interface
{
    public interface IRoleInfrastructureRepository
    {

        /// <summary>
        /// Adds a collection of object.
        /// </summary>
        /// <param name="obj">A collection of object to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the added object.</returns>
        Task<IEnumerable<IdentityResult>?> Add(IEnumerable<ApplicationRoleDto> obj, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a object by its ID.
        /// </summary>
        /// <param name="id">The ID of the object to be updated.</param>
        /// <param name="obj">The updated object details.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with the updated object.</returns>
        Task<IEnumerable<IdentityResult>?> Update(IEnumerable<Guid> id, IEnumerable<ApplicationRoleDto> obj, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a collection of object by their IDs.
        /// </summary>
        /// <param name="id">A collection of object IDs to be deleted.</param>
        /// <param name="obj">A collection of object to be deleted.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a collection of the deleted object.</returns>
        Task<IEnumerable<IdentityResult>?> Delete(IEnumerable<Guid> id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a object by its ID.
        /// </summary>
        /// <param name="id">The ID of the object to be retrieved.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with the object details.</returns>
        Task<IEnumerable<ApplicationRoleDto?>> GetById(IEnumerable<Guid> id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all object.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a collection of all object.</returns>
        Task<IEnumerable<ApplicationRoleDto?>> GetAll(int page, int pageSize, CancellationToken cancellationToken);

    }
}
