using Blazing.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazing.Identity.Data
{
    //    public class BlazingIdentityDbContext(DbContextOptions<BlazingIdentityDbContext> options ) 
    //        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)

    public class BlazingIdentityDbContext(DbContextOptions<BlazingIdentityDbContext> options)
        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
            ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
            ApplicationRoleClaim, ApplicationUserToken>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("UserIdentity");
            builder.Entity<ApplicationRole>().ToTable("RoleIdentity");
            builder.Entity<ApplicationUserClaim>().ToTable("UserRoleClaim");
            builder.Entity<ApplicationUserRole>().ToTable("UserRole");
            builder.Entity<ApplicationUserLogin>().ToTable("UserLogin");
            builder.Entity<ApplicationRoleClaim>().ToTable("RoleClaim");
            builder.Entity<ApplicationUserToken>().ToTable("UserToken");
        }
    }
}

