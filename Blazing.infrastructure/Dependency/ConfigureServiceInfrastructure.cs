using Blazing.infrastructure.Data;
using Blazing.infrastructure.Interface;
using Blazing.infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.infrastructure.Dependency
{
    public static class ConfigureServiceInfrastructure
    {
        public static void AddInfrastructure(this IServiceCollection Services, IConfiguration Config)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Blazing"))
            //;
            Services.AddDbContext<BlazingDbContext>(options => options.UseSqlServer
           (Config.GetConnectionString("DevConnection") ?? throw new
           InvalidOperationException("Problema com a string de conexão. Verifique o arquivo appsettings.json")));


        }
    }
}
