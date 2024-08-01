using Blazing.Domain.Entities;

namespace Blazing.Domain.Interfaces.Services
{
    #region Interface Producto.
    /// <summary>
    /// Product domain layer interface
    /// </summary>
    public interface IProdutoDomainService
    {
        /// <summary>
        /// Adds a collection of products.
        /// </summary>
        /// <param name="product">A collection of products to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the added products.</returns>
        Task<IEnumerable<Product?>> AddProducts(IEnumerable<Product> product);

        /// <summary>
        /// Updates a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to be updated.</param>
        /// <param name="product">The updated product details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated product.</returns>
        Task<Product?> UpdateProduct(Guid id, Product product);

        /// <summary>
        /// Retrieves a collection of products by category ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to retrieve products from.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of products in the specified category.</returns>
        Task<IEnumerable<Product?>> GetProductsByCategoryId(Guid categoryId);

        /// <summary>
        /// Deletes a collection of products by their IDs.
        /// </summary>
        /// <param name="ids">A collection of product IDs to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the deleted products.</returns>
        Task<IEnumerable<Product?>> DeleteProducts(IEnumerable<Guid> ids);

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, with the product details.</returns>
        Task<Product?> GetProductById(Guid id);

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a collection of all products.</returns>
        Task<IEnumerable<Product?>> GetAll();
    }
    #endregion
}
