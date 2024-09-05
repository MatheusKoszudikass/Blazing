using Blazing.Application.Dto;


namespace Blazing.Ecommerce.Repository
{
    #region Interface Product Infrastructure Repository.
    public interface IProductInfrastructureRepository
    {
        /// <summary>
        /// Adds a collection of products to the repository.
        /// </summary>
        /// <param name="productDto">A collection of <see cref="ProductDto"/> objects representing the products to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of <see cref="ProductDto"/> objects that were added.</returns>
        Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDto, CancellationToken cancellationToken);

        /// <summary>
        /// Updates existing products based on their IDs.
        /// </summary>
        /// <param name="id">A collection of product IDs to be updated.</param>
        /// <param name="productDto">A collection of <see cref="ProductDto"/> objects containing the updated product information.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of <see cref="ProductDto"/> objects that were updated.</returns>
        Task<IEnumerable<ProductDto?>> UpdateProduct(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves products that belong to specified categories.
        /// </summary>
        /// <param name="id">A collection of category IDs.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of <see cref="ProductDto"/> objects associated with the specified category IDs.</returns>
        Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(IEnumerable<Guid> id, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes products based on their IDs.
        /// </summary>
        /// <param name="id">A collection of product IDs to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of <see cref="ProductDto"/> objects that were deleted.</returns>
        Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves products by their IDs.
        /// </summary>
        /// <param name="id">A collection of product IDs.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of <see cref="ProductDto"/> objects that match the specified IDs.</returns>
        Task<IEnumerable<ProductDto?>> GetProductById(IEnumerable<Guid> id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of all <see cref="ProductDto"/> objects.</returns>
        Task<IEnumerable<ProductDto?>> GetAll(int page, int pageSize, CancellationToken cancellationToken);

        /// <summary>
        /// Checks if the specified products exist in the repository.
        /// </summary>
        /// <param name="productDto">A collection of <see cref="ProductDto"/> objects to check for existence.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the products exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>
        Task<bool> ExistsAsyncProduct(IEnumerable<ProductDto> productDto, CancellationToken cancellationToken);

    }
    #endregion
}
