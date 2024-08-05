using Blazing.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.infrastructure.Interface
{
    public interface ICategoryInfrastructureRepository
    {
        Task<IEnumerable<CategoryDto?>> AddCategories(IEnumerable<CategoryDto> productDto);

        Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> productDto);

        Task<IEnumerable<CategoryDto?>> DeleteCategory(IEnumerable<Guid> id);

        Task<IEnumerable<CategoryDto?>> GetCategoryById(IEnumerable<Guid> id);

        Task<IEnumerable<CategoryDto?>> GetAll();

    }
}
