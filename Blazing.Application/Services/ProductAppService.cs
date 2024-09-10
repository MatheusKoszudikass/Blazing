using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Application.Interface.Product;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;

namespace Blazing.Application.Services
{
    #region Application product service.
    public class ProductAppService(IMapper mapper, ICrudDomainService<Product> produtoDomainService) : IProductAppService<ProductDto>
    {

        private readonly ICrudDomainService<Product> _produtoDomainService = produtoDomainService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adds a list of productsDto to the domain.
        /// </summary>
        /// <param name="productDto">The list of productsDto to be added.</param>
        /// <returns>The list of productsDto that have been added.</returns>
        public async Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDto, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productDto);

            var productResut = await _produtoDomainService.Add(products, cancellationToken);

            var productDtoResult = _mapper.Map<IEnumerable<ProductDto>>(productResut);

            return productDtoResult;
        }

        /// <summary>
        /// Updates an existing productDto based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the productDto to update.</param>
        /// <param name="productDto">The productDto object containing the updated data.</param>
        /// <returns>The updated productDto, if found.</returns>
        public async Task<IEnumerable<ProductDto?>> UpdateProduct(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto, IEnumerable<ProductDto> productsDtoUpdate, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productDto);
            var productsUpdate = _mapper.Map<IEnumerable<Product>>(productsDtoUpdate);

            var productResultDto = await _produtoDomainService.Update(id, products, productsUpdate, cancellationToken);

            productDto = _mapper.Map<IEnumerable<ProductDto>>(productResultDto);

            return productDto;
        }

        /// <summary>
        /// Gets productsDtos associated with a specific categoryDto ID.
        /// </summary>
        /// <param name="id">The category ID to filter the productsDto.</param>
        /// <returns>The list of productsDto associated with the given categoryDto.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productDto);

            var productResult = await _produtoDomainService.GetById(id, products, cancellationToken);

            var productDtoResult = _mapper.Map<IEnumerable<ProductDto>>(productResult);

            return productDtoResult;
        }

        /// <summary>
        /// Deletes productsDto based on a list of provided IDs.
        /// </summary>
        /// <param name="id">The list of productDto IDs to be deleted.</param>
        /// <returns>The list of productsDto that were deleted.</returns>
        public async Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productDto);

            var productResult = await _produtoDomainService.Delete(id, products, cancellationToken);

            productDto = _mapper.Map<IEnumerable<ProductDto>>(productResult);

            return productDto;
        }

        /// <summary>
        /// Gets a specific productDto based on the given ID.
        /// </summary>
        /// <param name="id">The ID of the productDto to get.</param>
        /// <returns>The productDto corresponding to the given ID.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductById(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productDto);

            var productResult = await _produtoDomainService.GetById(id, products, cancellationToken);

            var productDtoResult = _mapper.Map<IEnumerable<ProductDto?>>(productResult);

            return productDtoResult;
        }

        /// <summary>
        /// Gets all productsDto from the domain.
        /// </summary>
        /// <returns>The list of all productsDto.</returns>
        public async Task<IEnumerable<ProductDto?>> GetAllProduct(IEnumerable<ProductDto> productDto, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productDto);

            await _produtoDomainService.GetAll(products, cancellationToken);

            return productDto;
        }

        /// <summary>
        /// Checks if productsDto exist based on the provided flag.
        /// </summary>
        /// <param name="existsProducts">A boolean flag indicating the existence check.</param>
        public async Task<bool?> ExistsProduct(bool id, bool nameExists, IEnumerable<ProductDto> productDtos, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<IEnumerable<Product>>(productDtos);

            await _produtoDomainService.ExistsAsync(id, nameExists, product, cancellationToken);

            return nameExists;
        }
    }
    #endregion
}
