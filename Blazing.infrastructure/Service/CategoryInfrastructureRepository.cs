using Blazing.Application.Dto;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.infrastructure.Dependency;
using Blazing.infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blazing.infrastructure.Service
{
    #region Responsibility for searching data in the database in the category table.
    public class CategoryInfrastructureRepository(CategoryAppService categoryAppService, DependencyInjection dependencyInjection) : ICategoryInfrastructureRepository
    {
        private readonly CategoryAppService _categoryAppService = categoryAppService;
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;

        /// <summary>
        /// Adds a collection of categories to the repository.
        /// </summary>
        /// <param name="categoryDto">A collection of <see cref="CategoryDto"/> objects representing the categories to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were added.</returns>
        public async Task<IEnumerable<CategoryDto?>> AddCategories(IEnumerable<CategoryDto> categoryDto)
        {
            var categoryResult = await _categoryAppService.AddCategory(categoryDto);

            var category = _dependencyInjection._mapper.Map<IEnumerable<Category>>(categoryResult);

            await _dependencyInjection._appContext.Category.AddRangeAsync(category);

            await _dependencyInjection._appContext.SaveChangesAsync();

            return categoryDto;

        }

        /// <summary>
        /// Updates existing categories based on their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs to be updated.</param>
        /// <param name="categoryDtos">A collection of <see cref="CategoryDto"/> objects containing the updated category information.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were updated.</returns>
        public async Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryDtos)
        {
            var categoryResult = await _categoryAppService.UpdateCategory(id, categoryDtos);

            var existingCategory = await _dependencyInjection._appContext.Category
                                         .ToListAsync();

            var existingCategoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(existingCategory);

            await _categoryAppService.UpdateCategory(id, existingCategoryDto);

            foreach (var category in existingCategory)
            {
                var updateCategoryDto = categoryDtos.Where(x => x.Id == category.Id).FirstOrDefault();
                if (updateCategoryDto != null)
                {
                    category.Name = updateCategoryDto.Name;
                }
            }

            await _dependencyInjection._appContext.SaveChangesAsync();
            return categoryDtos;
        }


        /// <summary>
        /// Deletes categories based on their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> that were deleted.</returns>
        public async Task<IEnumerable<CategoryDto?>> DeleteCategory(IEnumerable<Guid> id)
        {
            var category = await _dependencyInjection._appContext.Category
                                 .Where(c => id.Contains(c.Id)).ToListAsync();

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(category);

            await _categoryAppService.DeleteCategory(id, categoryDto);

            _dependencyInjection._appContext.Category.RemoveRange(category);

            await _dependencyInjection._appContext.SaveChangesAsync();

            return categoryDto;
        }


        /// <summary>
        /// Retrieves categories by their IDs.
        /// </summary>
        /// <param name="id">A collection of category IDs.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the collection of <see cref="CategoryDto"/> objects that match the specified IDs.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetCategoryById(IEnumerable<Guid> id)
        {
            var category = await _dependencyInjection._appContext.Category
                                 .Where(c => id.Contains(c.Id)).ToListAsync();

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(category);

            await _categoryAppService.GetById(id, categoryDto);

            return categoryDto;
        }

        /// <summary>
        /// Retrieves all categories from the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result of a collection of all <see cref="CategoryDto"/> objects.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetAll()
        {
            var categories = await _dependencyInjection._appContext.Category
                             .Include(p => p.Products)
                             .ToListAsync();

            var categoryDto = _dependencyInjection._mapper.Map<IEnumerable<CategoryDto>>(categories);

            await _categoryAppService.GetAll(categoryDto);

            return categoryDto;

        }

        /// <summary>
        /// Checks if the specified categories exist in the repository.
        /// </summary>
        /// <param name="categoryNames">A collection of <see cref="CategoryDto"/> objects to check for existence.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the categories exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>
        public async Task<bool> ExistsAsync(IEnumerable<CategoryDto?> categoryNames)
        {
            var category = _dependencyInjection._mapper.Map<IEnumerable<Category>>(categoryNames);

            var categoryResult = await _dependencyInjection._appContext.Category.AnyAsync(p => category.Select(x => x.Name).Contains(p.Name));

            var categoryResultDto = await _categoryAppService.ExistsCategories(categoryResult);

            return categoryResult;
        }
    }
    #endregion
}
