using Blazing.Application.Dto;

namespace Blazing.Application.Interfaces.Product
{
    #region Interface product.
    /// <summary>
    /// Product application layer interface
    /// </summary>
    public interface IProductAppService
    {
        /// <summary>
        /// Adds a collection of productsDto.
        /// </summary>
        /// <param name="productDtos">A collection of productsDto to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the added productsDto.</returns>
        Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDtos);

        /// <summary>
        /// Updates a productsDto by its ID.
        /// </summary>
        /// <param name="id">The ID of the productsDto to be updated.</param>
        /// <param name="productDto">The updated productsDto details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated productsDto.</returns>
        Task<ProductDto?> UpdateProduct(Guid id, ProductDto productDto);

        /// <summary>
        /// Retrieves a collection of productsDto by categoryDto ID.
        /// </summary>
        /// <param name="categoryId">The ID of the categoryDto to retrieve productsDto from.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of productsDto in the specified categoryDto.</returns>
        Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(Guid categoryId);

        /// <summary>
        /// Deletes a collection of productsDto by their IDs.
        /// </summary>
        /// <param name="ids">A collection of productsDto IDs to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the deleted productsDto.</returns>
        Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> ids);

        /// <summary>
        /// Retrieves a productsDto by its ID.
        /// </summary>
        /// <param name="id">The ID of the productsDto to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, with the productsDto details.</returns>
        Task<ProductDto?> GetProductById(Guid id);

        /// <summary>
        /// Retrieves all productsDto.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a collection of all productsDto.</returns>
        Task<IEnumerable<ProductDto?>> GetAll();
    }
    #endregion
}
