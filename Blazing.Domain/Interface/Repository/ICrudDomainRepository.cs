using Blazing.Domain.Entities;

namespace Blazing.Domain.Interface.Repository
{
    #region Contract object .
    public interface ICrudDomainRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Adds a collection of object asynchronously.
        /// </summary>
        /// <param name="obj">The objects to be added.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added object.</returns>
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> obj);

        /// <summary>
        /// Updates a object identified by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the object to be updated.</param>
        /// <param name="obj">The updated object information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated object.</returns>
        Task<IEnumerable<T>> UpdateAsync(IEnumerable<Guid> id, IEnumerable<T> obj);

        /// <summary>
        /// Deletes objects identified by their IDs asynchronously.
        /// </summary>
        /// <param name="id">The IDs of the objects to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of deleted objects.</returns>
        Task<IEnumerable<T>> DeleteByIdAsync(IEnumerable<Guid> id, IEnumerable<T> obj);

        /// <summary>
        /// Retrieves a object by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the object to be retrieved.</param>
        /// <param name="obj">The object to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the object with the specified ID.</returns>
        Task<IEnumerable<T>> GetByIdAsync(IEnumerable<Guid> id, IEnumerable<T> obj);

        /// <summary>
        /// Retrieves all objects asynchronously.
        /// </summary>
        /// <param name="obj">The objects to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all objects.</returns>
        Task<IEnumerable<T>> GetAllAsync(IEnumerable<T> obj);

        /// <summary>
        /// Checks if objects with the specified names exist asynchronously.
        /// </summary>
        /// <param name="boolean">The names of the objects to check for existence.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating if any objects with the specified names exist.</returns>
        Task<bool> ExistsAsync(bool boolean);

    }
    #endregion

}
