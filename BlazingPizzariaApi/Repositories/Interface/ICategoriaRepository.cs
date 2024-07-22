using BlazingPizza.Api.Entites;
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Api.Repositories.Interface
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<CategoriasDtos?>> AddCategoria(List<CategoriasDtos> categoriasDtos);

        Task<IEnumerable<CategoriasDtos?>> GetItensPorCategorias(int id);


    }
}
