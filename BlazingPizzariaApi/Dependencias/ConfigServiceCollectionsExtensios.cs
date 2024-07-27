using BlazingPizza.Api.Controllers;
using BlazingPizza.Api.Data;
using BlazingPizza.Api.Profiles;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Api.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BlazingPizza.Api.Dependencias
{
    public static class ConfigServiceCollectionsExtensios
    {
        public static IServiceCollection AddConfig(
            this IServiceCollection Services, IConfiguration Config)
        {
            Services.AddDbContext<AppDbContext>(options => options.UseSqlServer
            (Config.GetConnectionString("DevConnection") ?? throw new
            InvalidOperationException("Problema com a string de conexão. Verifique o arquivo appsettings.json")));
            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            Services.AddScoped<InjectServicesApi>();
            Services.AddScoped<ICarrinhoCompraRepository, CarrinhoCompraRepository>();
            Services.AddScoped<ICategoriaRepository, CategoriaServices>();
            Services.AddScoped<IProdutoRepository, ProdutoServices>();
   

            return Services;
        }
    }
}
