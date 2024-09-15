using BenchmarkDotNet.Attributes;
using Blazing.Application.Dto;
using Blazing.Application.Interface.Product;
using Blazing.Application.Mappings;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Ecommerce.Dependency;
using Blazing.Ecommerce.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Blazing.Ecommerce.Repository
{
    #region Responsibility for searching data in the database in the product table.
    /// <summary>
    /// Repository class for managing Product domain objects.
    /// </summary>
    [MemoryDiagnoser]
    public class ProductInfrastructureRepository(ProductMapping product, IMemoryCache memoryCache,DependencyInjection dbContext, IProductAppService<ProductDto> productInfrastructureRepository) : IProductInfrastructureRepository
    {
        private readonly ProductMapping _productMapping = product;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly DependencyInjection _dependencyInjection = dbContext;
        private readonly IProductAppService<ProductDto> _productAppService = productInfrastructureRepository;


        /// <summary>
        /// Adds a product to the repository.
        /// </summary>
        /// <param name="productDto">The product to add.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The added product.</returns>
        public async Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDto, CancellationToken cancellationToken)
        {
            var newProduct = productDto.ToList();
            await ExistsAsyncProduct(newProduct, cancellationToken);

            var productDtoResult = await _productAppService.AddProducts(newProduct, cancellationToken);

            var productResult = _dependencyInjection._mapper.Map<IEnumerable<Product>>(productDtoResult);

            await _dependencyInjection._appContext.Products.AddRangeAsync(productResult, cancellationToken);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(productDtoResult);
        }

        /// <summary>
        /// Updates a product in the repository.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDtoUpdate">The updated product.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated product.</returns>
        public async Task<IEnumerable<ProductDto?>> UpdateProduct(IEnumerable<Guid> id, IEnumerable<ProductDto> productDtoUpdate, CancellationToken cancellationToken)
        {
            var ids = id.ToList();
            if (!ids.Any() || ids.Contains(Guid.Empty))
            {
                throw new ProductExceptions.IdentityProductInvalidException(ids);
            }

            // Obtém os produtos existentes com as propriedades relacionadas carregadas
            var existingProducts = await _dependencyInjection._appContext.Products
                .Include(a => a.Assessment)
                    .ThenInclude(r => r.RevisionDetail)
                .Include(d => d.Dimensions)
                .Include(a => a.Attributes)
                .Include(a => a.Availability)
                .Include(i => i.Image)
                .Where(p => ids.Contains(p.Id))
                .ToListAsync(cancellationToken);

            // Mapeia os produtos existentes para DTOs
            var productDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(existingProducts);

            // Atualiza os produtos usando o serviço
            var productDtoUpdateResult = await _productAppService.UpdateProduct(ids, productDto, productDtoUpdate, cancellationToken);

            // Atualiza as propriedades das entidades existentes com base nos DTOs atualizados
            var updatedProductDos = productDtoUpdateResult.ToList();
            foreach (var updatedProductDto in updatedProductDos)
            {
                var existingProduct = existingProducts.SingleOrDefault(p => p.Id == updatedProductDto.Id);
                if (existingProduct != null)
                {
                    _dependencyInjection._mapper.Map(updatedProductDto, existingProduct);
                }
            }

            // Salva as alterações no banco de dados
            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return updatedProductDos;
        }


        /// <summary>
        /// Gets products by category ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The products in the category.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var ids = id.ToList();

            if (!_memoryCache.TryGetValue(ids, out IEnumerable<Product>? productsCategories))
            {
                productsCategories = await _dependencyInjection._appContext.Products
                    .Include(a => a.Assessment)
                    .ThenInclude(r => r.RevisionDetail)
                    .Include(a => a.Attributes)
                    .Include(a => a.Availability)
                    .Include(i => i.Image)
                    .Take(5)
                    .Where(p => ids.Contains(p.CategoryId)).ToListAsync(cancellationToken);
            }

            var categoryResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(productsCategories);

            var productsByCategoryId = categoryResultDto.ToList();
            await _productAppService.GetProductsByCategoryId(ids, productsByCategoryId, cancellationToken);

            return productsByCategoryId;

        }

        /// <summary>
        /// Deletes products by ID.
        /// </summary>
        /// <param name="id">The IDs of the products to delete.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The deleted products.</returns>
        public async Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var products = await _dependencyInjection._appContext.Products
                                 .Include(d => d.Dimensions)
                                 .Include(a => a.Assessment)
                                          .ThenInclude(r => r.RevisionDetail)
                                 .Include(a => a.Attributes)
                                 .Include(a => a.Availability)
                                 .Include(i => i.Image)
                                 .Where(p => id.Contains(p.Id)).ToListAsync(cancellationToken);

            foreach (var product in products)
            {
                if (product.Dimensions != null)
                {
                    _dependencyInjection._appContext.Dimensions.Remove(product.Dimensions);
                }

                if (product.Assessment != null)
                {
                    _dependencyInjection._appContext.Assessments.Remove(product.Assessment);

                    if (product.Assessment.RevisionDetail.Any())
                    {
                        _dependencyInjection._appContext.Revisions.RemoveRange(product.Assessment.RevisionDetail);
                    }
                }

                if (product.Attributes != null)
                {
                    _dependencyInjection._appContext.Attributes.Remove(product.Attributes);
                }

                if (product.Availability != null)
                {
                    _dependencyInjection._appContext.Availabilities.Remove(product.Availability);
                }

                if (product.Image != null)
                {
                    _dependencyInjection._appContext.Image.Remove(product.Image);
                }
            }

            var productDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(products);

            var deleteProducts = productDto.ToList();
            await _productAppService.DeleteProducts(id, deleteProducts, cancellationToken);

            _dependencyInjection._appContext.Products.RemoveRange(products);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return deleteProducts;
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The product.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var cacheKey = id.ToList();
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Product>? products))
            {
                products = await _dependencyInjection._appContext.Products
                                        .Include(a => a.Assessment)
                                        .ThenInclude(r => r.RevisionDetail)
                                        .Include(d => d.Dimensions)
                                        .Include(a => a.Attributes)
                                        .Include(a => a.Availability)
                                        .Include(i => i.Image)
                                        .AsNoTracking()
                                        .Take(5)
                                        .Where(p => cacheKey.Contains(p.Id))
                                        .ToListAsync(cancellationToken);
            }

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(products);

            var productById = productResultDto.ToList();
            var productResult = await _productAppService.GetProductById(cacheKey, productById, cancellationToken);

            return productById;
        }

        public async Task<IEnumerable<ProductDto?>> GetAll(int page, int pageSize, CancellationToken cancellationToken)
        {
            const string cacheKey = $"products_all";

            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Product>? products))
            {
                products = await _dependencyInjection._appContext.Products
                    .Include(a => a.Assessment)
                    .ThenInclude(r => r.RevisionDetail)
                    .Include(d => d.Dimensions)
                    .Include(a => a.Attributes)
                    .Include(a => a.Availability)
                    .Include(i => i.Image)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };

                _memoryCache.Set(cacheKey, products, cacheExpiryOptions);
            }

            var productResultDto = new List<ProductDto?>();
            if (products == null) return productResultDto;
            var productCache = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
             productResultDto = _productMapping.ReturnProductDto(productCache).ToList();

            return productResultDto;
        }


        /// <summary>
        /// Checks if any products with the specified names exist in the database.
        /// </summary>
        /// <param name="product">A collection of product names to check.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if any products with the specified names exist, false otherwise.</returns>
        public async Task<bool> ExistsAsyncProduct(IEnumerable<ProductDto> product, CancellationToken cancellationToken)
        {
            var productId = await _dependencyInjection._appContext.Products.AnyAsync(p => product.Select(x => x.Id).Contains(p.Id), cancellationToken);

            var nameExists = await _dependencyInjection._appContext.Products.AnyAsync(p => product.Select(x => x.Name).Contains(p.Name), cancellationToken);

            await _productAppService.ExistsProduct(productId, nameExists, product, cancellationToken);

            return productId;
        }
    }
    #endregion
}

