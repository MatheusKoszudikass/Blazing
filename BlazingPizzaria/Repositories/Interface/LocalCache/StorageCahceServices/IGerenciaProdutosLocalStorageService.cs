
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Repositories.Interface.LocalCache.StorageCahceServices
{
    public interface IGerenciaProdutosLocalStorageService
    {

        Task<IEnumerable<ProdutoDto>> GetCollectionItem();
        Task RemoveCollectionItem();

    }
}
