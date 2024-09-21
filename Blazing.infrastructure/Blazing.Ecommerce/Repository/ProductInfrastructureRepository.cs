using Blazing.Application.Dto;
using Blazing.Application.Interface.Product;
using Blazing.Application.Mappings;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Product;
using Blazing.Ecommerce.Dependencies;
using Blazing.Ecommerce.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blazing.Ecommerce.Repository
{
    #region Responsibility for searching data in the database in the product table.
    /// <summary>
    /// Repository class for managing Product domain objects.
    /// </summary>
    public class ProductInfrastructureRepository(ProductDtoMapping product, IMemoryCache memoryCache,DependencyInjection dbContext, IProductAppService productInfrastructureRepository) : IProductInfrastructureRepository
    {
        private readonly ProductDtoMapping _productMapping = product;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly DependencyInjection _dependencyInjection = dbContext;
        private readonly IProductAppService _productAppService = productInfrastructureRepository;
        private const string CacheKey = "products_all";

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

            var result =  await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            var productsDto = productDtoResult.ToList();
            if (result > 0)
                 await UpdateCacheProduct(productsDto, cancellationToken);

            return productsDto;
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
            if (ids.Count == 0 || ids.Contains(Guid.Empty))
                throw new ProductExceptions.IdentityProductInvalidException(ids);

            var productUpdate = productDtoUpdate.ToList();
            if (!productUpdate.Any(p => ids.Contains(p.Id)))
                throw new ProductExceptions.ProductNotFoundException(
                    _dependencyInjection._mapper.Map<IEnumerable<Product>>(productDtoUpdate));
            

            var existingProducts = await _dependencyInjection._appContext.Products
                .Include(a => a.Assessment)
                    .ThenInclude(r => r.RevisionDetail)
                .Include(d => d.Dimensions)
                .Include(a => a.Attributes)
                .Include(a => a.Availability)
                .Include(i => i.Image)
                .Where(p => ids.Contains(p.Id))
                .ToListAsync(cancellationToken);

            var productDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(existingProducts);

            var productDtoUpdateResult = await _productAppService.UpdateProduct(ids, productDto, productUpdate, cancellationToken);

            var updatedProductDos = productDtoUpdateResult.ToList();
            foreach (var updatedProductDto in updatedProductDos)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existingProduct = existingProducts.SingleOrDefault(p => p.Id == updatedProductDto.Id);
                if (existingProduct != null)
                {
                    _dependencyInjection._mapper.Map(updatedProductDto, existingProduct);
                }
            }

            var result =  await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            if(result > 0)
               await UpdateCacheProduct(updatedProductDos, cancellationToken);

            return updatedProductDos;
        }

        /// <summary>
        /// Gets products by category ID.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="id">The ID of the category.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The products in the category.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(int page, int pageSize, IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var idsCategory = id.ToList();
            if (idsCategory.Count == 0 || idsCategory.Contains(Guid.Empty))
                throw DomainException.IdentityInvalidException.Identities(idsCategory);

            if (!_memoryCache.TryGetValue(CacheKey, out List<Product>? products))
            {
                if (products == null)
                {
                    await CreateCacheProduct(cancellationToken);

                    _memoryCache.TryGetValue(CacheKey, out products);

                    if (products == null)
                        await _productAppService.GetAllProduct(_productMapping.ReturnProductDto(products, cancellationToken),
                            cancellationToken);
                }
            }

            var cacheProducts = products?
                .Where(p => idsCategory.Contains(p.CategoryId))
                .GroupBy(p => p.CategoryId)
                .SelectMany(g => g.Skip((page - 1) * pageSize).Take(pageSize));

            var categoryProductsResultDto = _productMapping.ReturnProductDto(cacheProducts, cancellationToken).ToList();

            await _productAppService.GetProductsByCategoryId(idsCategory, categoryProductsResultDto, cancellationToken);

            return categoryProductsResultDto;
        }

        /// <summary>
        /// Deletes products by ID.
        /// </summary>
        /// <param name="id">The IDs of the products to delete.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The deleted products.</returns>
        public async Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var idsProducts = id.ToList();
            if(idsProducts.Count == 0 || idsProducts.Contains(Guid.Empty))
                throw  DomainException.IdentityInvalidException.Identities(idsProducts);

            var products = await _dependencyInjection._appContext.Products
                 .Include(d => d.Dimensions)
                 .Include(a => a.Assessment)
                          .ThenInclude(r => r.RevisionDetail)
                 .Include(a => a.Attributes)
                 .Include(a => a.Availability)
                 .Include(i => i.Image)
                 .Where(p => idsProducts.Contains(p.Id)).ToListAsync(cancellationToken);


            var productDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(products);


            var resultDeletedProductDto = await _productAppService.DeleteProducts(idsProducts, productDto, cancellationToken);
            var deleteProducts = resultDeletedProductDto.ToList();

            foreach (var product in products)
            {
                cancellationToken.ThrowIfCancellationRequested();
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

            _dependencyInjection._appContext.Products.RemoveRange(products);

            var result = await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            if(result > 0)
                DeletedCacheProduct(deleteProducts, cancellationToken);

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
           var ids = id.ToList();
           if (ids.Count == 0 || ids.Contains(Guid.Empty))
               throw DomainException.IdentityInvalidException.Identities(ids);

           var products = await _dependencyInjection._appContext.Products
                .Include(a => a.Assessment)
                .ThenInclude(r => r.RevisionDetail)
                .Include(d => d.Dimensions)
                .Include(a => a.Attributes)
                .Include(a => a.Availability)
                .Include(i => i.Image)
                .AsNoTracking()
                .Where(p => ids.Contains(p.Id))
                .Take(5)
                .ToListAsync(cancellationToken);

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(products);

            var productById = productResultDto.ToList();
            var  result = await _productAppService.GetProductById(ids, productById, cancellationToken);

            return result;
        }

        /// <summary>
        /// Retrieves a paginated list of products from cache, or populates the cache if it's not available.
        /// If the cache is empty, it creates a new cache entry with all products and then retrieves the specified page.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>An IEnumerable of ProductDto containing the requested page of products.</returns>
        public async Task<IEnumerable<ProductDto?>> GetAll(int page, int pageSize, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(CacheKey, out List<Product>? products))
            {

                if (products == null)
                {
                    await CreateCacheProduct(cancellationToken);

                    _memoryCache.TryGetValue(CacheKey, out products);

                    if (products == null)
                        await _productAppService.GetAllProduct(_productMapping.ReturnProductDto(products, cancellationToken),
                            cancellationToken);
                }
            }
            
            var productCache = products?
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var resultProductDto = _productMapping.ReturnProductDto(productCache, cancellationToken);
            var result = resultProductDto.ToList();
            await _productAppService.GetAllProduct(result, cancellationToken);

            return result;
        }

        /// <summary>
        /// Verifies if any products with the specified names or IDs exist in the database.
        /// </summary>
        /// <param name="productDto">A collection of ProductDto objects to check.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result is a boolean indicating if any products with the specified names or IDs exist.</returns>
        public async Task<bool> ExistsAsyncProduct(IEnumerable<ProductDto> productDto, CancellationToken cancellationToken)
        {
            var product = _dependencyInjection._mapper.Map<IEnumerable<Product>>(productDto);

            foreach (var item in product)
            {
                var resultIsProductId = await IsProductIdExistsAsync(item.Id.ToString(), cancellationToken);

                var resultIsProductName = await IsProductNameExistsAsync(item.Name, cancellationToken);

                await _productAppService.ExistsProduct(resultIsProductId, resultIsProductName, productDto, cancellationToken);
            }

            return false;
        }

        /// <summary>
        /// Checks if a product with the specified ID exists in the database.
        /// </summary>
        /// <param name="productId">The ID of the product to check.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a boolean indicating whether the product exists based on the provided ID.</returns>
        private async Task<bool> IsProductIdExistsAsync(string productId, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(productId);
            return await _dependencyInjection._appContext.Products.AnyAsync(u => u.Id == id, cancellationToken);
        }

        /// <summary>
        /// Checks if a product with the specified name exists in the database.
        /// </summary>
        /// <param name="productName">The name of the product to check.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a boolean indicating whether the product exists based on the provided name.</returns>
        private async Task<bool> IsProductNameExistsAsync(string? productName, CancellationToken cancellationToken)
        {
            return await _dependencyInjection._appContext.Products.AnyAsync(u => u.Name == productName, cancellationToken);
        }


        /// <summary>
        /// Creates the cache for products if it does not already exist. It retrieves products from the database,
        /// sets them in the cache with specified expiration policies, and returns a boolean indicating success.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>
        /// A task that returns true if the cache was created, and false if the cache already existed.
        /// </returns>
        private async Task<bool> CreateCacheProduct(CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(CacheKey, out IEnumerable<Product>? cachedProducts))
            {
                return false;
            }

            try
            {
                var products = await _dependencyInjection._appContext.Products
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

                if (products.Count > 0)
                {
                    _memoryCache.Set(CacheKey, products, cacheExpiryOptions);
                }
                else
                {
                    var productDto = _productMapping.ReturnProductDto(products, cancellationToken);
                    await _productAppService.GetAllProduct(productDto, cancellationToken);
                }

                return true;
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }
        }

        /// <summary>
        /// Updates the cache with the given list of product DTOs. If a product exists in the cache, it is updated;
        /// otherwise, the product is added to the cache. If the cache is not available, it will be created.
        /// </summary>
        /// <param name="productDto">The list of product DTOs to update or add to the cache.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation of updating the cache.</returns>
        /// <exception cref="ProductExceptions.ProductNotFoundException">Thrown if the provided product list is null or empty.</exception>
        /// <exception cref="DomainException">Thrown if there is an issue during cache update or creation.</exception>
        private async Task UpdateCacheProduct(IEnumerable<ProductDto?> productDto, CancellationToken cancellationToken)
        {
            var productList = productDto.ToList();
            var product = _dependencyInjection._mapper.Map<List<Product>>(productList).ToList();

            if (productList == null ||  productList.Count == 0)
                throw new ProductExceptions.ProductNotFoundException(product);

            try
            {
                if (_memoryCache.TryGetValue(CacheKey, out List<Product>? cachedProducts))
                {
                    foreach (var itemProduct in product)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        var existingProduct = cachedProducts.Find(p => p.Id == itemProduct.Id);
                        if (existingProduct != null)
                        {
                            var index = cachedProducts.IndexOf(existingProduct);
                            if (index >= 0)
                            {
                                cachedProducts[index] = itemProduct;
                            }
                        }
                        else
                        {
                            cachedProducts.Add(itemProduct);
                            _memoryCache.Set(CacheKey, cachedProducts);
                        }
                    }
                }
                else
                {
                    await CreateCacheProduct(cancellationToken);
                }
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }
        }

        /// <summary>
        /// Removes products from the cache based on the provided list of `ProductDto`.
        /// 
        /// - Maps the `ProductDto` objects to the `Product` entity.
        /// - Checks if the product list or the mapped list is null or empty.
        /// - If the products exist in the cache, they are removed from the cached categories list.
        /// - Updates the cache after removing the products.
        /// 
        /// Throws a `ProductNotFoundException` if the product list is null or empty.
        /// Also cancels the operation if the `CancellationToken` is requested.
        /// </summary>
        private void DeletedCacheProduct(IEnumerable<ProductDto?> productDto, CancellationToken cancellationToken)
        {
            var productList = productDto.ToList();
            var products = _dependencyInjection._mapper.Map<List<Product>>(productList);

            if (productList == null || products.Count == 0)
                throw new ProductExceptions.ProductNotFoundException(products);

            try
            {
                if (_memoryCache.TryGetValue(CacheKey, out List<Product>? cachedProducts))
                {
                    foreach (var category in products)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var existingCategory = cachedProducts.Find(c => c.Id == category.Id);

                        // Se a categoria existir no cache, remova-a
                        if (existingCategory != null)
                        {
                            cachedProducts.Remove(existingCategory);
                        }
                    }

                    _memoryCache.Set(CacheKey, cachedProducts);
                }
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }
        }
    }
    #endregion
}

