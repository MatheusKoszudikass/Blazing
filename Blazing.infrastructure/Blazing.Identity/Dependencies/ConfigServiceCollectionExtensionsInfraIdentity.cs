using Blazing.Application.Dto;
using Blazing.Application.Interface.User;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Services;
using Blazing.Ecommerce.Interface;
using Blazing.Ecommerce.Repository;
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
    public static class ConfigServiceCollectionExtensionsInfraIdentity
    {
        public static IServiceCollection AddConfigInfraIdentity(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BlazingIdentityDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Blazing"),
                    b => b.MigrationsAssembly("Blazing.Identity")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddEntityFrameworkStores<BlazingIdentityDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddAutoMapper(typeof(IdentityMapper));
            // Configure Authorization
            services.AddAuthorization();

            // Register additional services
            services.AddScoped<DependencyInjection>();
            services.AddScoped<BlazingIdentityMapper>();

            // Users
            services.AddScoped<ICrudDomainService<User>, UserDomainService>();
            services.AddScoped<IUserAppService<UserDto>, UserAppService>();
            services.AddScoped<IUserInfrastructureRepository, UserInfrastructureRepository>();
            services.AddScoped<Interface.IUserInfrastructureRepository, Repository.UserInfrastructureRepository>();

            // Roles
            services.AddScoped<Interface.IRoleInfrastructureRepository, Repository.RoleInfrastructureRepository>();

            return services;
        }
    }
}