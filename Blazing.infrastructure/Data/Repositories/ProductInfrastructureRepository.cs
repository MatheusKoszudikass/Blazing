using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Repository;
using Blazing.infrastructure.Dependency;
using Microsoft.EntityFrameworkCore;

namespace Blazing.infrastructure.Data.Repositories
{
    #region Responsibility for searching data in the database in the product table.
    /// <summary>
    /// Repository class for managing Product domain objects.
    /// </summary>
    public class ProductInfrastructureRepository(DependencyInjection dbContext) : IProductDomainRepository
    {
        private readonly DependencyInjection _dependencyInjection = dbContext;


        /// <summary>
        /// Adds a product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        public async Task<IEnumerable<Product?>> AddAsync(IEnumerable<Product> product)
        {

            await _dependencyInjection._appContext.Products.AddRangeAsync(product);

            await _dependencyInjection._appContext.SaveChangesAsync();

            return product;
        }

        /// <summary>
        /// Updates a product in the repository.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product.</param>
        /// <returns>The updated product.</returns>
        public async Task<Product?> UpdateAsync(Guid id, Product product)
        {

            var productDb = await _dependencyInjection._appContext.Products
                                  .Include(d => d.Dimensions)
                                  .Include(a => a.Assessment)
                                          .ThenInclude(r => r.RevisionDetail)
                                  .Include(a => a.Attributes)
                                  .Include(a => a.Availability)
                                  .Include(i => i.Image)
                                  .SingleOrDefaultAsync(p => p.Id == id);

            var productEdit =  _dependencyInjection._mapper.Map(product, productDb);

            await _dependencyInjection._appContext.SaveChangesAsync();

            return productEdit;
        }

        /// <summary>
        /// Gets products by category ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>The products in the category.</returns>
        public async Task<IEnumerable<Product?>> GetByCategoryIdAsync(Guid categoryId)
        {

            var category = await _dependencyInjection._appContext.Products
                                 .Include(d => d.Dimensions)
                                 .Include(a => a.Assessment)
                                         .ThenInclude(r => r.RevisionDetail)
                                 .Include(a => a.Attributes)
                                 .Include(a => a.Availability)
                                 .Include(i => i.Image)
                                 .Where(p => p.CategoryId == categoryId)
                                 .ToListAsync();
                                 
            return category;

        }

        /// <summary>
        /// Deletes products by ID.
        /// </summary>
        /// <param name="ids">The IDs of the products to delete.</param>
        /// <returns>The deleted products.</returns>
        public async Task<IEnumerable<Product>> DeleteByIdAsync(IEnumerable<Guid> ids)
        {

            var products = await _dependencyInjection._appContext.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();

             _dependencyInjection._appContext.Products.RemoveRange(products);

            await _dependencyInjection._appContext.SaveChangesAsync();

            return products;
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product.</returns>
        public async Task<Product?> GetByIdAsync(Guid id)
        {

            var product = await _dependencyInjection._appContext.Products
                                .Include(d => d.Dimensions)
                                .Include(a => a.Assessment)
                                        .ThenInclude(r => r.RevisionDetail)
                                .Include(a => a.Attributes)
                                .Include(a => a.Availability)
                                .Include(i => i.Image)
                                .SingleOrDefaultAsync(p => p.Id == id);

            return product;
        }


        /// <summary>
        /// Retrieves all products from the database, including related entities such as dimensions, assessments, attributes, and images.
        /// </summary>
        /// <returns>A collection of Product objects.</returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            var products = await _dependencyInjection._appContext.Products
                                .Include(d => d.Dimensions)
                                .Include(a => a.Assessment)
                                        .ThenInclude(r => r.RevisionDetail)
                                .Include(a => a.Attributes)
                                .Include(a => a.Availability)
                                .Include(i => i.Image)
                                .ToListAsync();
            return products;
        }

        /// <summary>
        /// Checks if any products with the specified names exist in the database.
        /// </summary>
        /// <param name="productNames">A collection of product names to check.</param>
        /// <returns>True if any products with the specified names exist, false otherwise.</returns>
        public async Task<bool> ExistsAsync(IEnumerable<string?> productNames)
        {
           return await _dependencyInjection._appContext.Products.AnyAsync(p => productNames.Contains(p.Name));
        }
    }
    #endregion
}
