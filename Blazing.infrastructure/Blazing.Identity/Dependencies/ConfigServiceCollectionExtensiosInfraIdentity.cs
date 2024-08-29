using Blazing.Application.Dto;
using Blazing.Application.Interfaces.User;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Services;
using Blazing.Ecommerce.Repository;
using Blazing.Ecommerce.Service;
using Blazing.Identity.Data;
using Blazing.Identity.Dependency;
using Blazing.Identity.Entities;
using Blazing.Identity.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazing.Identity.Dependencies
{
    public static class ConfigServiceCollectionExtensiosInfraIdentity
    {

        public static IServiceCollection AddConfigInfraIdentity(
            this IServiceCollection Services, IConfiguration Config)
        {

            Services.AddDbContext<BlazingIdentityDbContext>(options =>
                   options.UseSqlServer(Config.GetConnectionString("Blazing"), b => b.MigrationsAssembly("Blazing.Identity")));


            Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                         .AddEntityFrameworkStores<BlazingIdentityDbContext>()
                         .AddSignInManager()
                         .AddDefaultTokenProviders();

            Services.AddAuthentication();
            Services.AddAuthorization();

            Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<BlazingIdentityDbContext>();

            Services.AddScoped<DependencyInjection>();
            Services.AddScoped<BlazingIdentityMapper>();
  
            Services.AddScoped<ICrudDomainService<User>, UserDomainService>();
            Services.AddScoped<IUserAppService<UserDto>, UserAppService>();
            Services.AddScoped<Ecommerce.Repository.IUserInfrastructureRepository, Ecommerce.Service.UserInfrastructureRepository>();
            Services.AddScoped<Identity.Repository.IUserInfrastructureRepository, Identity.Service.UserInfrastructureRepository>();

            Services.Configure<IdentityOptions>(options =>
            {
                //Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;

                //Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;
            });

            Services.ConfigureApplicationCookie(options => 
            {
                //Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDanied";
                options.SlidingExpiration = true;
            });

            return Services;
        }
    }
}
