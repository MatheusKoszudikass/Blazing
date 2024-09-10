using Blazing.Application.Dto;
using Blazing.Application.Interface.Category;
using Blazing.Application.Interface.Product;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Interfaces.Services.User;
using Blazing.Domain.Services;
using Blazing.Ecommerce.Data;
using Blazing.Ecommerce.Interface;
using Blazing.Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazing.Ecommerce.Dependencies
{
    public static class ConfigServiceCollectionExtensionsInfraEcommerce
    {

        public static IServiceCollection AddConfigInfraEcommerce(
            this IServiceCollection Services, IConfiguration Config)
        {
            Services.AddDbContext<BlazingDbContext>(options =>
                  options.UseSqlServer(Config.GetConnectionString("Blazing"), b => b.MigrationsAssembly("Blazing.Ecommerce")));



            //Product dependencies
            Services.AddScoped<IProductInfrastructureRepository, ProductInfrastructureRepository>();
            Services.AddScoped<IProductAppService<ProductDto>, ProductAppService>();
            Services.AddScoped<ICrudDomainService<Product>, ProductDomainService>();

            //Category dependencies
            Services.AddScoped<ICategoryInfrastructureRepository, CategoryInfrastructureRepository>();
            Services.AddScoped<ICategoryAppService<CategoryDto>, CategoryAppService>();
            Services.AddScoped<ICrudDomainService<Category>, CategoryDomainService>();

            //Users
            Services.AddScoped<IUserDomainService, UserDomainService>();
            return Services;
        }
    }
}
