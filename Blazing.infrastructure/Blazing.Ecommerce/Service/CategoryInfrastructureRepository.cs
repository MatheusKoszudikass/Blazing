using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Category;
using Blazing.Application.Interfaces.Product;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using Blazing.Ecommerce.Dependency;
using Blazing.Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Ecommerce.Service
{
    #region Responsibility for searching data in the database in the category table.
    public class CategoryInfrastructureRepository(ICategoryAppService<CategoryDto> categoryAppService, DependencyInjection dependencyInjection) : ICategoryInfrastructureRepository
    {
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
        /// <param name="categoryDtos">A collection of <see cref="CategoryDto"/> objects containing the updated category information.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were updated.</returns>
        public async Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryUpdate, CancellationToken cancellationToken)
        {
            if (!id.Any())
            {
                throw new CategoryExceptions.IdentityCategoryInvalidException(id);
            }

            var existingCategories = await _dependencyInjection._appContext.Category
                                         .Where(c => id.Contains(c.Id))
                                         .ToListAsync(cancellationToken: cancellationToken);

            var categoryDtos = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(existingCategories);

            var categoryDtoUpdateResult = await _categoryAppService.UpdateCategory(id, categoryDtos, categoryUpdate, cancellationToken);

            foreach (var updateCategoryDto in categoryDtoUpdateResult)
            {
                var existingCategory = existingCategories.SingleOrDefault(c => c.Id == updateCategoryDto.Id);
                if (existingCategory != null)
                {
                    _dependencyInjection._mapper.Map(updateCategoryDto, existingCategory);
                }
            }

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);
            return categoryDtoUpdateResult;
        }


        /// <summary>
        /// Deletes categories based on their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs to be deleted.</param>
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
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> objects that match the specified IDs.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetCategoryById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var category = await _dependencyInjection._appContext.Category
                                 .Where(c => id.Contains(c.Id)).ToListAsync(cancellationToken);

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(category);

            await _categoryAppService.GetById(id, categoryDto, cancellationToken);

            return categoryDto;
        }

        /// <summary>
        /// Retrieves all categories from the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of all <see cref="CategoryDto"/> objects.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _dependencyInjection._appContext.Category
                             .ToListAsync(cancellationToken: cancellationToken);

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(categories);

            await _categoryAppService.GetAll(categoryDto, cancellationToken);

            return categoryDto;

        }

        /// <summary>
        /// Checks if the specified categories exist in the repository.
        /// </summary>
        /// <param name="categoryNames">A collection of <see cref="CategoryDto"/> objects to check for existence.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the categories exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>
        public async Task<bool> ExistsAsync(IEnumerable<CategoryDto?> categories, CancellationToken cancellationToken)
        {
            var category = _dependencyInjection._mapper.Map<IEnumerable<Category>>(categories);

            var resultId = await _dependencyInjection._appContext.Category.AnyAsync(p => category.Select(c => c.Id).Contains(p.Id));

            var resultName = await _dependencyInjection._appContext.Category.AnyAsync(p => category.Select(x => x.Name).Contains(p.Name));

            await _categoryAppService.ExistsCategories(resultId, resultName, categories, cancellationToken);

            return resultId;
        }
    }
    #endregion
}
