using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Product;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Entities;

namespace Blazing.Application.Services
{
    #region Application product service.
    public class ProducAppService(IProdutoDomainService produtoDomainService, IMapper mapper) : IProductAppService
    {
        private readonly IProdutoDomainService _produtoDomainService = produtoDomainService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adds a list of productsDto to the domain.
        /// </summary>
        /// <param name="productDtos">The list of productsDto to be added.</param>
        /// <returns>The list of productsDto that have been added.</returns>
        public async Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDtos)
        {
            var product = _mapper.Map<IEnumerable<Product>>(productDtos);

            await _produtoDomainService.AddProducts(product);

            return productDtos;
        }

        /// <summary>
        /// Updates an existing productDto based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the productDto to update.</param>
        /// <param name="productDto">The productDto object containing the updated data.</param>
        /// <returns>The updated productDto, if found.</returns>
        public async Task<ProductDto?> UpdateProduct(Guid id, ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _produtoDomainService.UpdateProduct(id, product);

            return productDto;
        }

        /// <summary>
        /// Gets productsDtos associated with a specific categoryDto ID.
        /// </summary>
        /// <param name="categoryId">The category ID to filter the productsDto.</param>
        /// <returns>The list of productsDto associated with the given categoryDto.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(Guid categoryId)
        {
            var product = await _produtoDomainService.GetProductsByCategoryId(categoryId);

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(product);

            return productDto;
        }

        /// <summary>
        /// Deletes productsDto based on a list of provided IDs.
        /// </summary>
        /// <param name="ids">The list of productDto IDs to be deleted.</param>
        /// <returns>The list of productsDto that were deleted.</returns>
        public async Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> ids)
        {
            var product = await _produtoDomainService.DeleteProducts(ids);

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(product);

            return productDto;

        }

        /// <summary>
        /// Gets a specific productDto based on the given ID.
        /// </summary>
        /// <param name="id">The ID of the productDto to get.</param>
        /// <returns>The productDto corresponding to the given ID.</returns>
        public async Task<ProductDto?> GetProductById(Guid id)
        {
            var product = await _produtoDomainService.GetProductById(id);

            var produtcDto = _mapper.Map<ProductDto>(product);

            return produtcDto;
        }

        /// <summary>
        /// Gets all productsDto from the domain.
        /// </summary>
        /// <returns>The list of all productsDto.</returns>
        public async Task<IEnumerable<ProductDto?>> GetAll()
        {
            var product = await _produtoDomainService.GetAll();

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(product);

            return productDto;
        }
    }
    #endregion
}
