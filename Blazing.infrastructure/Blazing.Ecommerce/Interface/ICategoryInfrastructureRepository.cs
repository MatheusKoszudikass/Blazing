using Blazing.Application.Dto;

namespace Blazing.Ecommerce.Interface
{
    public interface ICategoryInfrastructureRepository
    {
        Task<IEnumerable<CategoryDto?>> AddCategories(IEnumerable<CategoryDto> productDto, CancellationToken cancellationToken);

        Task<IEnumerable<CategoryDto?>> UpdateCategory(IEnumerable<Guid> id, IEnumerable<CategoryDto> productDto, CancellationToken cancellationToken);

        Task<IEnumerable<CategoryDto?>> DeleteCategory(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<IEnumerable<CategoryDto?>> GetCategoryById(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<IEnumerable<CategoryDto?>> GetAll(int page, int pageSize, CancellationToken cancellationToken);
    }
}
