using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazing.infrastructure.Data
{
    #region Context responsible for the application database context.
    /// <summary>
    /// Represents the context for the application's database.
    /// </summary>
    /// <remarks>
    /// This class inherits from DbContext and provides DbSet properties for each entity in the application's database.
    /// </remarks>
    public class BlazingDbContext(DbContextOptions<BlazingDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<AddCartItem> AddCartItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Attributes>  Attributes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Dimensions> Dimensions { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Revision> Revisions { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=host.docker.internal,1200;Database=Blazing;User Id=sa;Password=019Uf%HG0!{;TrustServerCertificate=True", sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5, // Número máximo de tentativas
                    maxRetryDelay: TimeSpan.FromSeconds(30), // Tempo máximo de espera entre tentativas
                    errorNumbersToAdd: null); // Lista de números de erro adicionais para tratar como transitórios
            });
        }

        }
    }
    #endregion
