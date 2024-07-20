using BlazingPizza.Api.Repositories.Security;
using BlazingPizza.Dependencias;
using BlazingPizza.Repositories.Interface;
using BlazingPizza.Repositories.Interface.LocalCache.StorageCahceServices;
using BlazingPizzaria.Models.DTOs;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using NuGet.Protocol;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BlazingPizza.Repositories.Services.LocalCache.StorageCahceServices
{
    public class GerenciaProdutosLocalStorageService : IGerenciaProdutosLocalStorageService
    {
        private const string _key = @"f7$9(Q/7tpsf$?i,";

        private readonly ILocalStorageService _localStorageService;

        private readonly IProdutoServices _produtoServices;

        private readonly InjectServicesWeb _injectServicesWeb;

        public GerenciaProdutosLocalStorageService(ILocalStorageService localStorageService, InjectServicesWeb InjectServicesWeb,
            IProdutoServices ProdutoServices)
        {
            _localStorageService = localStorageService;
            _injectServicesWeb = InjectServicesWeb;
            _produtoServices = ProdutoServices;
        }

        public async Task<IEnumerable<ProdutoDtos>> GetCollectionItem()
        {
            return await _localStorageService.GetItemAsync<IEnumerable<ProdutoDtos>>(_key)
                ?? await AddCollectionItem();

        }

        public async Task RemoveCollectionItem()
        {  
            await this._localStorageService.RemoveItemAsync(_key);
        }

        public async Task <IEnumerable<ProdutoDtos>> AddCollectionItem()
        {

            var produtoCollection = await this._produtoServices.GetItens();

            if (produtoCollection != null)
            {
                await this._localStorageService.SetItemAsync(_key, produtoCollection);
            }

            return produtoCollection;
        }
    }
}
