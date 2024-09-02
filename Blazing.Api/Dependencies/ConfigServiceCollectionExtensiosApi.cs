using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Category;
using Blazing.Application.Interfaces.Product;
using Blazing.Application.Mappings;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Services;
using Blazing.Ecommerce.Data;
using Blazing.Ecommerce.Repository;
using Blazing.Ecommerce.Service;
using Blazing.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Blazing.Api.Dependencies
{
    public static class ConfigServiceCollectionExtensiosAPi
    {
        public static IServiceCollection AddConfigApi(
            this IServiceCollection Services, IConfiguration Config)
        {
            //Context Identity
            Services.AddAutoMapper(typeof(BlazingProfile));

            Services.AddSwaggerGen();


            JsonSerializerOptions options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            return Services;
        }
    }
}
