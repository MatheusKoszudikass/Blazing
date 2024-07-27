using BlazingPizza.Api.Entites;
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Api.Repositories.Interface
{
    /// <summary>
    /// Metodos nescessarios para cria serviço de categorias.
    /// </summary>
    public interface ICategoriaRepository
    {
        Task<IEnumerable<CategoriasDto?>> AddCategoria(List<CategoriasDto> categoriasDtos);
        Task<CategoriasDto?> UpdateCategoria(Guid id, CategoriasDto categoriasDto);
        Task<IEnumerable<CategoriasDto?>> DeleteCategoria(List<Guid> id);
        Task<CategoriasDto?> GetItemCategoria(Guid id);
        Task<IEnumerable<CategoriasDto?>> GetIAllCategoria();

    }
}
