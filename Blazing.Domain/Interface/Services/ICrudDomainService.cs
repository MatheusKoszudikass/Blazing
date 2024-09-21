using Blazing.Domain.Entities;

namespace Blazing.Domain.Interface.Services
{
    #region Interface Crud Domain Service.
    /// <summary>
    /// Product domain layer interface
    /// </summary>
    public interface ICrudDomainService<T> where T : BaseEntity
    {
        /// <summary>
        /// Adds a collection of object.
        /// </summary>
        /// <param name="obj">A collection of object to be added.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a collection of the added object.</returns>
        Task<IEnumerable<T>> Add(IEnumerable<T> obj, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a object by its ID.
        /// </summary>
        /// <param name="id">The ID of the object to be updated.</param>
        /// <param name="obj">The updated object details.</param>
        /// <param name="objUpdate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with the updated object.</returns>
        Task<IEnumerable<T>> Update(IEnumerable<Guid> id, IEnumerable<T> obj, IEnumerable<T> objUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a collection of object by their IDs.
        /// </summary>
        /// <param name="id">A collection of object IDs to be deleted.</param>
        /// <param name="obj">A collection of object to be deleted.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a collection of the deleted object.</returns>
        Task<IEnumerable<T>> Delete(IEnumerable<Guid> id, IEnumerable<T> obj, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a object by its ID.
        /// </summary>
        /// <param name="id">The ID of the object to be retrieved.</param>
        /// <param name="obj">The product details.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with the object details.</returns>
        Task<IEnumerable<T>> GetById(IEnumerable<Guid> id, IEnumerable<T> obj, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all object.
        /// </summary>
        /// <param name="obj">The object details.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a collection of all object.</returns>
        Task<IEnumerable<T>> GetAll(IEnumerable<T> obj, CancellationToken cancellationToken);

        /// <summary>
        /// Checks if a specified condition exists asynchronously.
        /// </summary>
        /// <param name="boolean">A boolean value indicating whether the condition to check exists.</param>
        /// <param name="booleanI"></param>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A Task representing the asynchronous operation, with a boolean result indicating the existence of the condition.</returns>
        Task<bool> ExistsAsync(bool boolean, bool booleanI, IEnumerable<T> obj, CancellationToken cancellationToken);
    }
    #endregion
}
