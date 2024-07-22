using BlazingPizza.Api.Entites;
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Api.Repositories.Interface
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria?>> AddCategoria(List<CategoriasDtos> categoriasDtos);

        Task<IEnumerable<Categoria?>> GetItensPorCategorias(int id);


    }
}
