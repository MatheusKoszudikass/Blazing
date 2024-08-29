using Blazing.Domain.Entities;
using Blazing.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazing.Identity.Data
{
    public class BlazingIdentityDbContext(DbContextOptions<BlazingIdentityDbContext> options ) 
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<IdentityUser>().ToTable("IdentityUser");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("UserRoleClaim");

            base.OnModelCreating(builder);

            //builder.Ignore<AddCartItemDto>();
            ////builder.Ignore<AddressDto>();
            //builder.Ignore<AssessmentDto>();
            //builder.Ignore<AttributeDto>();
            //builder.Ignore<AvailabilityDto>();
            ////builder.Ignore<CartItemDto>();
            //builder.Ignore<CategoryDto>();
            //builder.Ignore<DimensionsDto>();
            //builder.Ignore<ImageDto>();
            //builder.Ignore<ProductDto>();
            //builder.Ignore<Revision>();
            ////builder.Ignore<ShoppingCartDto>();
            //builder.Ignore<UserDto>();
        }
    }
}
