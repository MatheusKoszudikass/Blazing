using Blazing.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Interfaces.Category
{
    #region Interface Category App Service
    public interface ICategoryAppService<T> where T : BaseEntityDto
    {

        /// <summary>
        /// Adds a collection of categoryDto.
        /// </summary>
        /// <param name="categoryDtos">A collection of categoryDto to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the added category.</returns>
        Task<IEnumerable<CategoryDto?>> AddCategory(IEnumerable<CategoryDto> categoryDtos, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a categoryDto by its ID.
        /// </summary>
        /// <param name="id">The ID of the categoryDto to be updated.</param>
        /// <param name="categoryDtos">The updated categoryDto details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated categoryDto.</returns>
        Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> categoryDtos, IEnumerable<CategoryDto> categoriesDtoUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a collection of categoryDto by their IDs.
        /// </summary>
        /// <param name="id">A collection of categoryDto IDs to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the deleted categoryDto.</returns>
        Task<IEnumerable<CategoryDto?>> DeleteCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto?> categoryDtos, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a categoryDto by its ID.
        /// </summary>
        /// <param name="id">The ID of the categoryDto to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, with the categoryDto details.</returns>
        Task<IEnumerable<CategoryDto?>> GetById(IEnumerable<Guid> id, IEnumerable<CategoryDto?> categoryDtos, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all categoryDto.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a collection of all categoryDto.</returns>
        Task<IEnumerable<CategoryDto?>> GetAll(IEnumerable<CategoryDto?> categoryDtos, CancellationToken cancellationToken);

        /// <summary>
        /// Checks if categoriesDto exist based on the provided flag.
        /// </summary>
        /// <param name="id">A boolean flag indicating whether to check for the existence of categoriesDto.</param>
        /// <param name="nameExists">A boolean flag indicating whether to check for the existence of categoriesDto.</param>
        Task<bool?> ExistsCategories(bool id, bool nameExists, IEnumerable<CategoryDto?> categoryDtos);
    }
    #endregion
}
