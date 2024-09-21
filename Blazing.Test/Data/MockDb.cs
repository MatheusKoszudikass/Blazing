using Blazing.Ecommerce.Data;
using Blazing.Identity.Data;
using Blazing.Identity.Dependencies;
using Blazing.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazing.Test.Data
{
    public class MockDb : IDbContextFactory<BlazingDbContext>
    {
        public BlazingDbContext CreateDbContext()
        {

            var options = new DbContextOptionsBuilder<BlazingDbContext>()
                .UseInMemoryDatabase($"InMemoryTest{DateTime.Now.ToFileTime()}").Options;


            //var options = new DbContextOptionsBuilder<BlazingDbContext>()
            //    .UseSqlServer($"Server=DESKTOP-QRUO464\\SQLEXPRESS;Database=Blazing;User Id=sa;Password=root;TrustServerCertificate=True").Options;


            return new BlazingDbContext(options);
        }

        public BlazingIdentityDbContext CreateDbContextIdentity()
        {
            var options = new DbContextOptionsBuilder<BlazingIdentityDbContext>()
                .UseInMemoryDatabase($"InMemoryTest{DateTime.Now.ToFileTime()}").Options;

            //ConfigureServices();

            return new BlazingIdentityDbContext(options);
        }

        //public void ConfigureServices()
        //{
        //    var Services = new ServiceCollection();

        //    Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //        .AddEntityFrameworkStores<BlazingDbContext>()
        //        .AddDefaultTokenProviders();
        //}
    }
}
