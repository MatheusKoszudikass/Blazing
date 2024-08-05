using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Product;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Services;

namespace Blazing.Application.Services
{
    #region Application product service.
    public class ProductAppService(ProductDomainService produtosDomainService, IMapper mapper) : IProductAppService
    {
        private readonly ProductDomainService _produtoDomainService = produtosDomainService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adds a list of productsDto to the domain.
        /// </summary>
        /// <param name="productDto">The list of productsDto to be added.</param>
        /// <returns>The list of productsDto that have been added.</returns>
        public async Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDto)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productDto);

            await _produtoDomainService.Add(products);

            return productDto;
        }

        /// <summary>
        /// Updates an existing productDto based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the productDto to update.</param>
        /// <param name="productDto">The productDto object containing the updated data.</param>
        /// <returns>The updated productDto, if found.</returns>
        public async Task<IEnumerable<ProductDto?>> UpdateProduct(IEnumerable<Guid> id, IEnumerable<ProductDto> productDto)
        {
            var product = _mapper.Map<IEnumerable<Product>>(productDto);

            var productResultDto =  await _produtoDomainService.Update(id, product);

            productDto = _mapper.Map<IEnumerable<ProductDto>>(productResultDto);

            return productDto;
        }

        /// <summary>
        /// Gets productsDtos associated with a specific categoryDto ID.
        /// </summary>
        /// <param name="categoryId">The category ID to filter the productsDto.</param>
        /// <returns>The list of productsDto associated with the given categoryDto.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(IEnumerable<Guid> id, IEnumerable<ProductDto?> productDto)
        {
            var product = _mapper.Map<IEnumerable<Product>>(productDto);

            var productResult = await _produtoDomainService.GetById(id, product);

            var productDtoResult = _mapper.Map<IEnumerable<ProductDto>>(productResult);

            return productDtoResult;
        }

        /// <summary>
        /// Deletes productsDto based on a list of provided IDs.
        /// </summary>
        /// <param name="ids">The list of productDto IDs to be deleted.</param>
        /// <returns>The list of productsDto that were deleted.</returns>
        public async Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> id, IEnumerable<ProductDto?> productDto)
        {
            var product = _mapper.Map<IEnumerable<Product>>(productDto);

            var productResult = await _produtoDomainService.Delete(id, product);

            var productDtoResult = _mapper.Map<IEnumerable<ProductDto>>(productResult);

            return productDtoResult;

        }

        /// <summary>
        /// Gets a specific productDto based on the given ID.
        /// </summary>
        /// <param name="id">The ID of the productDto to get.</param>
        /// <returns>The productDto corresponding to the given ID.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductById(IEnumerable<Guid> id, IEnumerable<ProductDto?> productDto)
        {
            var product = _mapper.Map<IEnumerable<Product>>(productDto);

            var productResult = await _produtoDomainService.GetById(id, product);

            var productDtoResult = _mapper.Map<IEnumerable<ProductDto?>>(productResult);

            return productDtoResult;
        }

        /// <summary>
        /// Gets all productsDto from the domain.
        /// </summary>
        /// <returns>The list of all productsDto.</returns>
        public async Task<IEnumerable<ProductDto?>> GetAllProduct(IEnumerable<ProductDto?> productDto)
        {
            var product = _mapper.Map<IEnumerable<Product>>(productDto);

            await _produtoDomainService.GetAll(product);

            return productDto;
        }

        /// <summary>
        /// Checks if productsDto exist based on the provided flag.
        /// </summary>
        /// <param name="existsProducts">A boolean flag indicating the existence check.</param>
        public async Task<bool> ExistsProduct(bool existsProducts)
        {
           var result =  await _produtoDomainService.ExistsAsync(existsProducts);

            return result;
        }
    }
    #endregion
}
