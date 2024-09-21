using Blazing.Application.Dto;
using Blazing.Application.Interface.Category;
using Blazing.Application.Mappings;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Category;
using Blazing.Ecommerce.Dependencies;
using Blazing.Ecommerce.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blazing.Ecommerce.Repository
{
    #region Responsibility for searching data in the database in the category table.
    public class CategoryInfrastructureRepository(CategoryDtoMapping category ,IMemoryCache memoryCache,
        ICategoryAppService<CategoryDto> categoryAppService, DependencyInjection dependencyInjection) : ICategoryInfrastructureRepository
    {
        private readonly CategoryDtoMapping _categoryMapping = category;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly ICategoryAppService<CategoryDto> _categoryAppService = categoryAppService;
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;
        private const string CacheKey  =  "categories_all";

        /// <summary>
        /// Adds a collection of categories to the repository.
        /// </summary>
        /// <param name="categoryDto">A collection of <see cref="CategoryDto"/> objects representing the categories to be added.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were added.</returns>
        public async Task<IEnumerable<CategoryDto?>> AddCategories(IEnumerable<CategoryDto> categoryDto, CancellationToken cancellationToken)
        {
            var newCategories = categoryDto.ToList();
            await ExistsAsyncCategory(newCategories, cancellationToken);

            var categoryResult = await _categoryAppService.AddCategory(newCategories, cancellationToken);

            var category = _dependencyInjection._mapper.Map<IEnumerable<Category>>(categoryResult);

            await _dependencyInjection._appContext.Category.AddRangeAsync(category, cancellationToken);

            var result = await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            var categoriesDto = categoryResult.ToList();
            if (result > 0)
                await UpdateCacheCategory(categoriesDto, cancellationToken);

            return categoriesDto;

        }

        /// <summary>
        /// Updates existing categories based on their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs to be updated.</param>
        /// <param name="categoryDtoUpdate">A collection of <see cref="CategoryDto"/> objects containing the updated category information.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were updated.</returns>
        public async Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryDtoUpdate, CancellationToken cancellationToken)
        {
            var idsList = id.ToList();
            if (idsList.Count == 0 || idsList.Any(i => i == Guid.Empty))
                throw  DomainException.IdentityInvalidException.Identities(idsList); 

            var categoryUpdate = categoryDtoUpdate.ToList();
            if (!categoryUpdate.Any(p => idsList.Contains(p.Id)))
                throw  CategoryExceptions.CategoryNotFoundException.NotFoundCategories(
                    _dependencyInjection._mapper.Map<IEnumerable<Category>>(categoryDtoUpdate));


            var existingCategories = await _dependencyInjection._appContext.Category
                 .Where(c => idsList.Contains(c.Id))
                 .ToListAsync(cancellationToken: cancellationToken);

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(existingCategories);

            var categoryDtoUpdateResult = await _categoryAppService.UpdateCategory(idsList, categoryDto, categoryUpdate, cancellationToken);

            var updateCategory = categoryDtoUpdateResult.ToList();
            foreach (var updateCategoryDto in updateCategory)
            {
                var existingCategory = existingCategories.Find(c => c.Id == updateCategoryDto.Id);
                if (existingCategory != null)
                {
                    _dependencyInjection._mapper.Map(updateCategoryDto, existingCategory);
                }
            }

            var result = await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            if (result > 0)
                await UpdateCacheCategory(updateCategory, cancellationToken);

            return updateCategory;
        }

        /// <summary>
        /// Deletes categories based on their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs to be deleted.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were deleted.</returns>
        public async Task<IEnumerable<CategoryDto?>> DeleteCategory(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var idList = id.ToList();
            if (idList.Count == 0 || idList.Any(guid => guid == Guid.Empty))
                throw DomainException.IdentityInvalidException.Identities(idList);

            var category = await _dependencyInjection._appContext.Category
                 .Include(p => p.Products)
                          .ThenInclude(d => d.Dimensions)
                  .Include(p => p.Products)
                           .ThenInclude(a => a.Assessment)
                                       .ThenInclude(r => r.RevisionDetail)
                 .Include(p => p.Products)
                          .ThenInclude(a => a.Attributes)
                 .Include(p => p.Products)
                          .ThenInclude(a => a.Availability)
                 .Include(p => p.Products)
                          .ThenInclude(i => i.Image)
                 .Where(c => idList.Contains(c.Id)).ToListAsync(cancellationToken: cancellationToken);

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(category);

            var deleteCategory = categoryDto.ToList();
            var resultDeleteCategory = await _categoryAppService.DeleteCategory(idList, deleteCategory, cancellationToken);

            foreach (var item in category)
            {
                foreach (var product in item.Products)
                {
                    if (product.Dimensions != null)
                    {
                        _dependencyInjection._appContext.Dimensions.RemoveRange(product.Dimensions);
                    }

                    if (product.Assessment != null)
                    {
                        _dependencyInjection._appContext.Assessments.Remove(product.Assessment);

                        if (!product.Assessment.RevisionDetail.Any())
                        {
                            _dependencyInjection._appContext.Revisions.RemoveRange(product.Assessment.RevisionDetail.ToList());
                        }
                    }

                    if (product.Attributes != null)
                    {
                        _dependencyInjection._appContext.Attributes.RemoveRange(product.Attributes);
                    }

                    if (product.Availability != null)
                    {
                        _dependencyInjection._appContext.Availabilities.RemoveRange(product.Availability);
                    }

                    if (product.Image != null)
                    {
                        _dependencyInjection._appContext.Image.RemoveRange(product.Image);
                    }
                }
            }

            _dependencyInjection._appContext.Category.RemoveRange(category);

            var result =  await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            if (result > 0)
                 DeleteCacheCategory(deleteCategory, cancellationToken);

            return resultDeleteCategory;
        }

        /// <summary>
        /// Retrieves categories by their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> objects that match the specified IDs.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetCategoryById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var idsCategory = id.ToList();
            if (idsCategory.Count == 0 || idsCategory.Contains(Guid.Empty))
                throw DomainException.IdentityInvalidException.Identities(idsCategory);

            var category = await _dependencyInjection._appContext.Category
                    .Take(5)
                    .Where(c => idsCategory.Contains(c.Id))
                    .ToListAsync(cancellationToken);
              
            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(category);

            var categoryById = categoryDto.ToList();
            var result = await _categoryAppService.GetById(idsCategory, categoryById, cancellationToken);

            return result;
        }

        /// <summary>
        /// Retrieves a paginated list of category from cache, or populates the cache if it's not available.
        /// If the cache is empty, it creates a new cache entry with all category and then retrieves the specified page.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of category per page.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>An IEnumerable of ProductDto containing the requested page of category.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetAll(int page, int pageSize,CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(CacheKey, out List<Category>? categories))
            {
                await CreateCacheCategories(cancellationToken);

                _memoryCache.TryGetValue(CacheKey, out categories);

                if (categories == null)
                    await _categoryAppService.GetAll(_categoryMapping.ReturnCategoryDto(categories, cancellationToken), cancellationToken);
            }

            var categoriesCache = categories?
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var resultCategoryDto = _categoryMapping.ReturnCategoryDto(categoriesCache, cancellationToken);
            var categoryDto = resultCategoryDto.ToList();
            var result =  await _categoryAppService.GetAll(categoryDto, cancellationToken);

            return result;
        }

        /// <summary>
        /// Verifies if any categories with the specified names or IDs exist in the database.
        /// </summary>
        /// <param name="categoryDto">A collection of CategoryDto objects to check.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result is a boolean indicating if any categories with the specified names or IDs exist.</returns>
        public async Task<bool> ExistsAsyncCategory(IEnumerable<CategoryDto> categoryDto, CancellationToken cancellationToken)
        {
            var categories = _dependencyInjection._mapper.Map<IEnumerable<Category>>(categoryDto);

            foreach (var item in categories)
            {
                var resultIsCategoryId = await IsCategoryIdExistsAsync(item.Id.ToString(), cancellationToken);
                var resultIsCategoryName = await IsCategoryNameExistsAsync(item.Name, cancellationToken);

                await _categoryAppService.ExistsCategories(resultIsCategoryId, resultIsCategoryName, categoryDto, cancellationToken);
            }

            return false;
        }

        /// <summary>
        /// Checks if a category with the specified ID exists in the database.
        /// </summary>
        /// <param name="categoryId">The ID of the category to check.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a boolean indicating whether the category exists based on the provided ID.</returns>
        private async Task<bool> IsCategoryIdExistsAsync(string categoryId, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(categoryId);
            return await _dependencyInjection._appContext.Category.AnyAsync(u => u.Id == id, cancellationToken);
        }

        /// <summary>
        /// Checks if a category with the specified name exists in the database.
        /// </summary>
        /// <param name="categoryName">The name of the category to check.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a boolean indicating whether the category exists based on the provided name.</returns>
        private async Task<bool> IsCategoryNameExistsAsync(string? categoryName, CancellationToken cancellationToken)
        {
            return await _dependencyInjection._appContext.Category.AnyAsync(u => u.Name == categoryName, cancellationToken);
        }


        /// <summary>
        /// Creates a cache for categories if it does not already exist.
        /// Retrieves the list of categories from the database and caches them.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the cache is created, otherwise false.</returns>
        /// <exception cref="DomainException">Thrown when a domain-level exception occurs during the operation.</exception>
        private async Task<bool> CreateCacheCategories(CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(CacheKey, out IEnumerable<Category>? category))
            {
                return false;
            }

            try
            {
                var categories = await _dependencyInjection._appContext.Category
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };

                if (categories.Count > 0)
                {
                    _memoryCache.Set(CacheKey, categories, cacheExpiryOptions);
                }
                else
                {
                    var categoryDto = _categoryMapping.ReturnCategoryDto(categories, cancellationToken);
                    await _categoryAppService.GetAll(categoryDto, cancellationToken);
                }

                return true;
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }

        }

        /// <summary>
        /// Updates the cache with the provided categories.
        /// If the category already exists in the cache, it is updated; otherwise, it is added to the cache.
        /// If the cache does not exist, it calls <see cref="UpdateCacheCategory"/> to create the cache.
        /// </summary>
        /// <param name="categoryDto">The list of category DTOs to update in the cache.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="CategoryExceptions.CategoryNotFoundException">Thrown if the provided category list is null or empty.</exception>
        /// <exception cref="DomainException">Thrown when a domain-level exception occurs during the operation.</exception>
        private async Task UpdateCacheCategory(IEnumerable<CategoryDto?> categoryDto, CancellationToken cancellationToken)
        {
            var categoryList = categoryDto.ToList();
            var category = _dependencyInjection._mapper.Map<List<Category>>(categoryList);

            if (categoryList == null || categoryList.Count == 0)
                throw CategoryExceptions.CategoryNotFoundException.NotFoundCategories(category);

            try
            {
                if (_memoryCache.TryGetValue(CacheKey, out List<Category>? cachedCategories))
                {
                    foreach (var itemCategory in category)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        var existingCategory = cachedCategories.Find(c => c.Id == itemCategory.Id);
                        if (existingCategory != null)
                        {
                            var index = cachedCategories.IndexOf(existingCategory);
                            if (index >= 0)
                            {
                                cachedCategories[index] = itemCategory;
                            }
                        }
                        else
                        {
                            cachedCategories.Add(itemCategory);
                            _memoryCache.Set(CacheKey, cachedCategories);
                        }
                    }
                }
                else
                {
                    await CreateCacheCategories(cancellationToken);
                }
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }
        }

        /// <summary>
        /// Removes specific categories from the cache based on the provided list of category DTOs.
        /// </summary>
        /// <param name="categoryDto">The list of categories to be removed, represented as CategoryDto.</param>
        /// <param name="cancellationToken">Cancellation token to propagate notification that the operation should be canceled.</param>
        /// <exception cref="CategoryExceptions.CategoryNotFoundException">
        /// Thrown when the provided category list is null or empty.
        /// </exception>
        /// <exception cref="DomainException">Thrown if an error occurs while accessing or updating the cache.</exception>
        /// <remarks>
        /// This method checks if the cache contains the list of categories and, if found, removes the categories
        /// from the cache based on their ID. The cache is updated with the modified list after removal.
        /// </remarks>
        private void DeleteCacheCategory(IEnumerable<CategoryDto?> categoryDto, CancellationToken cancellationToken)
        {
            var categoryList = categoryDto.ToList();
            var categoriesToRemove = _dependencyInjection._mapper.Map<List<Category>>(categoryList);

            if (categoryList == null || categoryList.Count == 0)
                throw CategoryExceptions.CategoryNotFoundException.NotFoundCategories(categoriesToRemove);

            try
            {
                if (_memoryCache.TryGetValue(CacheKey, out List<Category>? cachedCategories))
                {
                    foreach (var category in categoriesToRemove)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var existingCategory = cachedCategories.Find(c => c.Id == category.Id);

                        // Se a categoria existir no cache, remova-a
                        if (existingCategory != null)
                        {
                            cachedCategories.Remove(existingCategory);
                        }
                    }

                    _memoryCache.Set(CacheKey, cachedCategories);
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
