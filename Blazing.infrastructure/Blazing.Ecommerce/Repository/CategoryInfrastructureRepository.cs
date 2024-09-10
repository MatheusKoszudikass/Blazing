﻿using Blazing.Application.Dto;
using Blazing.Application.Interface.Category;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Ecommerce.Dependency;
using Blazing.Ecommerce.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blazing.Ecommerce.Repository
{
    #region Responsibility for searching data in the database in the category table.
    public class CategoryInfrastructureRepository(IMemoryCache memoryCache,ICategoryAppService<CategoryDto> categoryAppService, DependencyInjection dependencyInjection) : ICategoryInfrastructureRepository
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly ICategoryAppService<CategoryDto> _categoryAppService = categoryAppService;
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;

        /// <summary>
        /// Adds a collection of categories to the repository.
        /// </summary>
        /// <param name="categoryDto">A collection of <see cref="CategoryDto"/> objects representing the categories to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were added.</returns>
        public async Task<IEnumerable<CategoryDto?>> AddCategories(IEnumerable<CategoryDto> categoryDto, CancellationToken cancellationToken)
        {
            await ExistsAsync(categoryDto, cancellationToken);

            var categoryResult = await _categoryAppService.AddCategory(categoryDto, cancellationToken);

            var category = _dependencyInjection._mapper.Map<IEnumerable<Category>>(categoryResult);

            await _dependencyInjection._appContext.Category.AddRangeAsync(category, cancellationToken);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return categoryDto;

        }

        /// <summary>
        /// Updates existing categories based on their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs to be updated.</param>
        /// <param name="categoryUpdate">A collection of <see cref="CategoryDto"/> objects containing the updated category information.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were updated.</returns>
        public async Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryUpdate, CancellationToken cancellationToken)
        {
            var enumerable = id.ToList();
            if (id == null || enumerable.Count == 0 || Guid.Empty == enumerable.First())
            {
                throw DomainException.IdentityInvalidException.Identities(enumerable);
            }

            var existingCategories = await _dependencyInjection._appContext.Category
                                         .Where(c => enumerable.Contains(c.Id))
                                         .ToListAsync(cancellationToken: cancellationToken);

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(existingCategories);

            var categoryDtoUpdateResult = await _categoryAppService.UpdateCategory(enumerable, categoryDto, categoryUpdate, cancellationToken);

            var updateCategory = categoryDtoUpdateResult.ToList();
            foreach (var updateCategoryDto in updateCategory)
            {
                var existingCategory = existingCategories.SingleOrDefault(c => c.Id == updateCategoryDto.Id);
                if (existingCategory != null)
                {
                    _dependencyInjection._mapper.Map(updateCategoryDto, existingCategory);
                }
            }

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);
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
                                 .Where(c => id.Contains(c.Id)).ToListAsync(cancellationToken: cancellationToken);

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

                        if (product.Assessment.RevisionDetail != null && product.Assessment.RevisionDetail.Any())
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

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(category);

            await _categoryAppService.DeleteCategory(id, categoryDto, cancellationToken);

            _dependencyInjection._appContext.Category.RemoveRange(category);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return categoryDto;
        }


        /// <summary>
        /// Retrieves categories by their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> objects that match the specified IDs.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetCategoryById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var enumerable = id.ToList();
            var cacheId = enumerable;
            if (!_memoryCache.TryGetValue(cacheId, out IEnumerable<Category>? category))
            {
                category = await _dependencyInjection._appContext.Category
                    .Take(5)
                    .Where(c => enumerable.Contains(c.Id))
                    .ToListAsync(cancellationToken);

                _memoryCache.Set(cacheId, category, TimeSpan.FromMinutes(5));
            }
    
            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(category);

            var categoryById = categoryDto.ToList();
            await _categoryAppService.GetById(enumerable, categoryById, cancellationToken);

            return categoryById;
        }

        /// <summary>
        /// Retrieves all categories from the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of all <see cref="CategoryDto"/> objects.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetAll(int page, int pageSize,CancellationToken cancellationToken)
        {
            var cachePage = $"{page}, {pageSize}";
            if (!_memoryCache.TryGetValue(cachePage, out IEnumerable<Category>? categories))
            {
                categories = await _dependencyInjection._appContext.Category
                                    .AsNoTracking()
                                    .Skip((page - 1 ) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken: cancellationToken);

                _memoryCache.Set(cachePage, categories, TimeSpan.FromMinutes(5));
            }

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(categories);

            await _categoryAppService.GetAll(categoryDto, cancellationToken);

            return categoryDto;

        }

        /// <summary>
        /// Checks if the specified categories exist in the repository.
        /// </summary>
        /// <param name="categories">A collection of <see cref="CategoryDto"/> objects to check for existence.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the categories exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>
        public async Task<bool> ExistsAsync(IEnumerable<CategoryDto?> categories, CancellationToken cancellationToken)
        {
            var category = _dependencyInjection._mapper.Map<IEnumerable<Category>>(categories);

            var resultId = await _dependencyInjection._appContext.Category.AnyAsync(p => category.Select(c => c.Id).Contains(p.Id), cancellationToken: cancellationToken);

            var resultName = await _dependencyInjection._appContext.Category.AnyAsync(p => category.Select(x => x.Name).Contains(p.Name), cancellationToken: cancellationToken);

            await _categoryAppService.ExistsCategories(resultId, resultName, categories, cancellationToken);

            return resultId;
        }
    }
    #endregion
}
