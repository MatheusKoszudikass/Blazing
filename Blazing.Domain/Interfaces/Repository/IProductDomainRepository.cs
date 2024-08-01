using Blazing.Domain.Entities;

namespace Blazing.Domain.Interfaces.Repository
{
    #region Contract Product .
    public interface IProductDomainRepository
    {
        /// <summary>
        /// Adds a collection of products asynchronously.
        /// </summary>
        /// <param name="products">The products to be added.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added product.</returns>
        Task<IEnumerable<Product?>> AddAsync(IEnumerable<Product> products);

        /// <summary>
        /// Updates a product identified by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to be updated.</param>
        /// <param name="product">The updated product information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated product.</returns>
        Task<Product?> UpdateAsync(Guid id, Product product);

        /// <summary>
        /// Retrieves products by their category ID asynchronously.
        /// </summary>
        /// <param name="categoryId">The ID of the category to retrieve products for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of products in the specified category.</returns>
        Task<IEnumerable<Product?>> GetByCategoryIdAsync(Guid categoryId);

        /// <summary>
        /// Deletes products identified by their IDs asynchronously.
        /// </summary>
        /// <param name="ids">The IDs of the products to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of deleted products.</returns>
        Task<IEnumerable<Product>> DeleteByIdAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Retrieves a product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product with the specified ID.</returns>
        Task<Product?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all products asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all products.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Checks if products with the specified names exist asynchronously.
        /// </summary>
        /// <param name="productNames">The names of the products to check for existence.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating if any products with the specified names exist.</returns>
        Task<bool> ExistsAsync(IEnumerable<string?> productNames);

    }
    #endregion

}
