using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Interface.Category;

namespace Blazing.Application.Services
{
    #region Category Services.
    public class CategoryAppService(ICrudDomainService<Category> categoriasDomainService, IMapper mapper) : ICategoryAppService<CategoryDto>
    {
       
        private readonly ICrudDomainService<Category> _categoriaDomainService = categoriasDomainService;   
        private readonly IMapper _mapper = mapper;


        /// <summary>
        /// Adds a list of category to the domain.
        /// </summary>
        /// <param name="categoryDto">The list of categoryDtos to be added.</param>
        /// <returns>The list of categoryDto that have been added.</returns>
        public async Task<IEnumerable<CategoryDto?>> AddCategory(IEnumerable<CategoryDto> categoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDto);

            var categoryResult = await _categoriaDomainService.Add(category, cancellationToken);

            categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categoryResult);

            return categoryDto;
        }

        /// <summary>
        /// Updates an existing categoryDto based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the categoryDto to update.</param>
        /// <param name="categoryDto">The categoryDto object containing the updated data.</param>
        /// <returns>The updated categoryDto, if found.</returns>
        public async Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryDto, IEnumerable<CategoryDto> categoriesDtosUpdate, CancellationToken cancellationToken)
        {
            var category =  _mapper.Map<IEnumerable<Category>>(categoryDto);
            var categoryUpdate = _mapper.Map<IEnumerable<Category>>(categoriesDtosUpdate);

            var categoryResultDto =  await _categoriaDomainService.Update(id, category, categoryUpdate, cancellationToken);

            categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categoryResultDto);

            return categoryDto;
        }

        /// <summary>
        /// Deletes categoryDto based on a list of provided IDs.
        /// </summary>
        /// <param name="id">The list of categoryDto IDs to be deleted.</param>
        /// <returns>The list of categoryDto that were deleted.</returns>
        public async Task<IEnumerable<CategoryDto?>> DeleteCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDto);

            await _categoriaDomainService.Delete(id, category, cancellationToken);

            return categoryDto;
        }

        /// <summary>
        /// Gets a specific categoryDto based on the given ID.
        /// </summary>
        /// <param name="id">The ID of the categoryDto to get.</param>
        /// <returns>The categoryDto corresponding to the given ID.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetById(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDto);

            await _categoriaDomainService.GetById(id, category, cancellationToken);

            return categoryDto;
        }


        /// <summary>
        /// Gets all categoryDto from the domain.
        /// </summary>
        /// <param name="categoryDto">The list of categoryDto to be added.</param>
        /// <returns>The list of all categoryDto.</returns>
        public async Task<IEnumerable<CategoryDto?>> GetAll(IEnumerable<CategoryDto?> categoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDto);

            await _categoriaDomainService.GetAll(category, cancellationToken);

            return categoryDto;
        }

        /// <summary>
        /// Checks if categories exist based on the provided flag.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="existsName"></param>
        /// <param name="categoryDto">A boolean flag indicating the existence check.</param>
        /// <param name="cancellationToken"></param>
        public async Task<bool?> ExistsCategories(bool id, bool existsName, IEnumerable<CategoryDto?> categoryDto,
            CancellationToken cancellationToken)
        {
            var category = _mapper.Map<IEnumerable<Category>>(categoryDto);
            await _categoriaDomainService.ExistsAsync(id, existsName, category, cancellationToken);

            await Task.CompletedTask;
            return id;
        }
    }
    #endregion
}
