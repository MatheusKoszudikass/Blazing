using Blazing.Domain.Entities;

namespace Blazing.Domain.Interfaces.Repository
{
    /// <summary>
    /// Metodos nescessarios para cria serviço de categorias.
    /// </summary>
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Product?>> AddCategoria(IEnumerable<Product> categoriasDtos);
        Task<Category?> UpdateCategoria(Guid id, Category categoriasDto);
        Task<IEnumerable<Category?>> DeleteCategoria(IEnumerable<Guid> id);
        Task<Category?> GetItemCategoria(Guid id);
        Task<IEnumerable<Category?>> GetIAllCategoria();

    }
}
