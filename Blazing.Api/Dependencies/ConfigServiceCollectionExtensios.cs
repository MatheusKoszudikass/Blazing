using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Category;
using Blazing.Application.Interfaces.Product;
using Blazing.Application.Mappings;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Services;
using Blazing.infrastructure.Data;
using Blazing.infrastructure.Interface;
using Blazing.infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazing.Api.Dependencies
{
    public static class ConfigServiceCollectionExtensios 
    {
        public static IServiceCollection AddConfig(
            this IServiceCollection Services, IConfiguration Config)
        {

            Services.AddAutoMapper(typeof(BlazingProfile));

            Services.AddDbContext<BlazingDbContext>();

            //Product dependencies
            Services.AddScoped<IProductInfrastructureRepository, ProductInfrastructureRepository>();
            Services.AddScoped<IProductAppService<ProductDto>, ProductAppService>();
            Services.AddScoped<ICrudDomainService<Product>, ProductDomainService>();

            //Category dependencies
            Services.AddScoped<ICategoryInfrastructureRepository, CategoryInfrastructureRepository>();
            Services.AddScoped<ICategoryAppService<CategoryDto>, CategoryAppService>();
            Services.AddScoped<ICrudDomainService<Category>, CategoryDomainService>();

             
            JsonSerializerOptions options = new ()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            return Services;
        }
    }
}
