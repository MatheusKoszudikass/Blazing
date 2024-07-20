
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Repositories.Interface.LocalCache.StorageCahceServices
{
    public interface IGerenciaProdutosLocalStorageService
    {

        Task<IEnumerable<ProdutoDtos>> GetCollectionItem();
        Task RemoveCollectionItem();

    }
}
