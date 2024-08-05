using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Category;
using Blazing.Domain.Entities;
using Blazing.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Services
{
    #region Category Services.
    public class CategoryAppService(CategoryDomainService categoriasDomainService, IMapper mapper) : ICategoryAppService
    {
       
        private readonly CategoryDomainService _categoriaDomainService = categoriasDomainService;   
        private readonly IMapper _mapper = mapper;


        /// <summary>
        /// Adds a list of category to the domain.
        /// </summary>
        /// <param name="categoryDtos">The list of categoryDtos to be added.</param>
        /// <returns>The list of categoryDtos that have been added.</returns>
        public async Task<IEnumerable<CategoryDto?>> AddCategory(IEnumerable<CategoryDto> categoryDtos)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDtos);

            await _categoriaDomainService.Add(category);

            return categoryDtos;
        }

        /// <summary>
        /// Updates an existing categoryDtos based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the categoryDtos to update.</param>
        /// <param name="categoryDtos">The categoryDtos object containing the updated data.</param>
        /// <returns>The updated categoryDtos, if found.</returns>
        public async Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryDtos)
        {
            var category =  _mapper.Map<IEnumerable<Category>>(categoryDtos);

            await _categoriaDomainService.Update(id, category);

            return categoryDtos;
        }

        /// <summary>
        /// Deletes categoryDtos based on a list of provided IDs.
        /// </summary>
        /// <param name="id">The list of categoryDtos IDs to be deleted.</param>
        /// <returns>The list of categoryDtos that were deleted.</returns>
        public async Task<IEnumerable<CategoryDto?>> DeleteCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto?> categoryDtos)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDtos);

            await _categoriaDomainService.Delete(id, category);

            return categoryDtos;
        }

        /// <summary>
        /// Gets a specific categoryDtos based on the given ID.
        /// </summary>
        /// <param name="id">The ID of the categoryDtos to get.</param>
        /// <returns>The categoryDtos corresponding to the given ID.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetById(IEnumerable<Guid> id, IEnumerable<CategoryDto?> categoryDtos)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDtos);

            await _categoriaDomainService.GetById(id, category);

            return categoryDtos;
        }


        /// <summary>
        /// Gets all categoryDtos from the domain.
        /// </summary>
        /// <param name="categoryDtos">The list of categoryDtos to be added.</param>
        /// <returns>The list of all categoryDtos.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetAll(IEnumerable<CategoryDto?> categoryDtos)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDtos);

            await _categoriaDomainService.GetAll(category);

            return categoryDtos;
        }

        /// <summary>
        /// Checks if categories exist based on the provided flag.
        /// </summary>
        /// <param name="existsCategories">A boolean flag indicating the existence check.</param>
        public async Task<bool?> ExistsCategories(bool existsCategories)
        {
           
           var result = await _categoriaDomainService.ExistsAsync(existsCategories);

            return result;
        }
    }
    #endregion
}
