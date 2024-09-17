using Blazing.Application.Dto;

namespace Blazing.Application.Interface.Product
{
    #region Interface product App Service.
    /// <summary>
    /// Product application layer interface
    /// </summary>
    public interface IProductAppService<T> where T : BaseEntityDto
    {
        /// <summary>
        /// Adds a collection of productsDto.
        /// </summary>
        /// <param name="productDtos">A collection of productsDto to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the added productsDto.</returns>
        Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDtos, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a productsDto by its ID.
        /// </summary>
        /// <param name="id">The ID of the productsDto to be updated.</param>
        /// <param name="productDto">The updated productsDto details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated productsDto.</returns>
        Task<IEnumerable<ProductDto?>> UpdateProduct(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto, IEnumerable<ProductDto> productDtosUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a collection of productsDto by categoryDto ID.
        /// </summary>
        /// <param name="id">The ID of the categoryDto to retrieve productsDto from.</param>
        /// <param name="productDto">The productsDto to be retrieved.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a collection of productsDto in the specified categoryDto.</returns>
        Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(IEnumerable<Guid> id,
            IEnumerable<ProductDto?> productDto, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a collection of productsDto by their IDs.
        /// </summary>
        /// <param name="ids">A collection of productsDto IDs to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the deleted productsDto.</returns>
        Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a productsDto by its ID.
        /// </summary>
        /// <param name="id">The ID of the productsDto to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, with the productsDto details.</returns>
        Task<IEnumerable<ProductDto?>> GetProductById(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all productsDto.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a collection of all productsDto.</returns>
        Task<IEnumerable<ProductDto?>> GetAllProduct(IEnumerable<ProductDto?> productDto, CancellationToken cancellationToken);

        /// <summary>
        ///  checks the name to see if it already exists or not
        /// </summary>
        /// <param name="exists"></param>
        /// <returns></returns>
        Task<bool?> ExistsProduct(bool exists, bool existsProduct, IEnumerable<ProductDto> productDto, CancellationToken cancellationToken);
    }
    #endregion
}
