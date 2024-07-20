using BlazingPizza.Repositories.Interface;
using BlazingPizza.Repositories.Interface.LocalCache.StorageCahceServices;
using BlazingPizza.Repositories.Services;
using BlazingPizza.Repositories.Services.LocalCache.StorageCahceServices;
using Blazored.LocalStorage;
using System.Text.Json.Serialization;

namespace BlazingPizza.Dependencias
{
    public static class ConfigServiceCollectionsExtensios
    {
        public static IServiceCollection AddConfigWeb(
           this IServiceCollection Services, IConfiguration Config)
        {
            Services.AddScoped<IGerenciaProdutosLocalStorageService, GerenciaProdutosLocalStorageService>();
            Services.AddScoped<InjectServicesWeb>();
            Services.AddScoped<IProdutoServices, ProdutoServices>();
            Services.AddBlazoredLocalStorage();

            var baseUrl = "https://localhost:7007";

            Services.AddScoped(HttpClient => new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            });

            return Services;
        }
    }
}
