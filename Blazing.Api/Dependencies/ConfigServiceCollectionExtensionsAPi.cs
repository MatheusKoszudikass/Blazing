using Blazing.Application.Dto;
using Blazing.Application.Mappings;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Services;
using Blazing.Ecommerce.Data;
using Blazing.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Blazing.Api.Dependencies
{
    public static class ConfigServiceCollectionExtensionsAPi
    {
        public static IServiceCollection AddConfigApi(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(BlazingProfile));

            //Mapping product
            services.AddScoped<ProductDtoMapping>();
            services.AddScoped<ProductMapping>();

            //Mapping category
            services.AddScoped<CategoryDtoMapping>();
            services.AddScoped<CategoryMapping>();

            services.AddSwaggerGen();

            return services;
        }
    }
}
