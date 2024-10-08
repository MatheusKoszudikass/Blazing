﻿using Blazing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blazing.Ecommerce.Data
{
    #region Context responsible for the application database context.
    /// <summary>
    /// Represents the context for the application's database.
    /// </summary>
    /// <remarks>
    /// This class inherits from DbContext and provides DbSet properties for each entity in the application's database.
    /// </remarks>
    public class BlazingDbContext(DbContextOptions<BlazingDbContext> options) : DbContext(options)
    {
        public DbSet<AddCartItem> AddCartItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Dimensions> Dimensions { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Revision> Revisions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(500);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity(j => j.ToTable("RolePermissions"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.User)
                .UsingEntity(j => j.ToTable("RolesUser"));
        }

    }
    #endregion
}

