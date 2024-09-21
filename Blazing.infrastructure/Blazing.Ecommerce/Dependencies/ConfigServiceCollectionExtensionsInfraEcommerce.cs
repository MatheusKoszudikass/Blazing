using Blazing.Application.Dto;
using Blazing.Application.Interface.Category;
using Blazing.Application.Interface.Permission;
using Blazing.Application.Interface.Product;
using Blazing.Application.Mappings;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interface.Services;
using Blazing.Domain.Interface.Services.User;
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
            this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<BlazingDbContext>(options =>
                  options.UseSqlServer(config.GetConnectionString("Blazing"),
                      b => b.MigrationsAssembly("Blazing.Ecommerce")));


            service.AddAutoMapper(typeof(BlazingProfile));
            service.AddScoped<DependencyInjection>();

            //Product dependencies
            service.AddScoped<IProductInfrastructureRepository, ProductInfrastructureRepository>();
            service.AddScoped<IProductAppService, ProductAppService>();
            service.AddScoped<ICrudDomainService<Product>, ProductDomainService>();

            //Category dependencies
            service.AddScoped<ICategoryInfrastructureRepository, CategoryInfrastructureRepository>();
            service.AddScoped<ICategoryAppService<CategoryDto>, CategoryAppService>();
            service.AddScoped<ICrudDomainService<Category>, CategoryDomainService>();

            //Users
            service.AddScoped<IUserDomainService, UserDomainService>();

            //Permission
            service.AddScoped<IPermissionAppService, PermissionAppService>();
            service.AddScoped<IPermissionInfrastructureRepository, PermissionInfrastructureRepository>();
            service.AddScoped<ICrudDomainService<Permission>, PermissionDomainService>();

            return service;
        }
    }
}
