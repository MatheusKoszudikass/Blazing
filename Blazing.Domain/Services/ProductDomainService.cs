using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;

namespace Blazing.Domain.Services
{
    #region Domain product service.
    public class ProductDomainService(ICrudDomainRepository<Product> produtoRepository) : ICrudDomainService<Product>
    {
       
        private readonly ICrudDomainRepository<Product> _crudDomainRepository = produtoRepository;

        /// <summary>
        /// Adds a list of products to the repository.
        /// </summary>
        /// <param name="product">The list of products to be added.</param>
        /// <returns>The list of products that have been added.</returns>
        /// <exception cref="ProductNotFoundExceptions">Thrown when the product list is null or empty.</exception>
        public async Task<IEnumerable<Product?>> Add(IEnumerable<Product> product)
        {
            if (product == null || !product.Any())
            {
                throw new ProductNotFoundExceptions([]);
            }

            try
            {
                await _crudDomainRepository.AddAsync(product);
                return product;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an existing product based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The product object containing the updated data.</param>
        /// <returns>The updated product, if found.</returns>
        /// <exception cref="IdentityProductInvalidException">Thrown when the product with the given ID is not found.</exception>
        /// <exception cref="ProductInvalidExceptions">Thrown when the product to be updated is invalid.</exception>
        public async Task<IEnumerable<Product?>> Update(IEnumerable<Guid> id, IEnumerable<Product> product)
        {
            if (!id.Any())
            {
                throw new IdentityProductInvalidException(id);
            }
            else if (!product.Any(p => p.Id == id.FirstOrDefault()))
            {
                throw new ProductNotFoundExceptions(product);
            }

            try
            {
                 await _crudDomainRepository.UpdateAsync(id, product);

                if (product == null)
                {
                    throw new ProductInvalidExceptions(new Product());
                }

                return product;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes products based on a list of provided IDs.
        /// </summary>
        /// <param name="id">The list of product IDs to be deleted.</param>
        /// <returns>The list of products that were deleted.</returns>
        /// <exception cref="IdentityProductInvalidException">Thrown when the list of provided IDs is empty.</exception>
        /// <exception cref="ProductNotFoundExceptions">Throws when no product is deleted.</exception>
        public async Task<IEnumerable<Product?>> Delete(IEnumerable<Guid> id, IEnumerable<Product> product)
        {
            if (!id.Any())
            {
                throw new IdentityProductInvalidException(id);
            }
            else if(!product.Any(p => p.Id == id.FirstOrDefault()))
            {
                throw new ProductNotFoundExceptions(product);
            }

            try
            {
                 await _crudDomainRepository.DeleteByIdAsync(id, product);

                if (!product.Any())
                {
                    throw new ProductNotFoundExceptions([]);
                }

                return product;


            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets products associated with a specific ID.
        /// </summary>
        /// <param name="id">The list of ID to filter the products.</param>
        /// <returns>The list of products associated with the given id.</returns>
        /// <exception cref="IdentityProductInvalidException">Thrown when the given ID is invalid.</exception>
        /// <exception cref="ProductNotFoundExceptions">Throws when no products not found .</exception>
        public async Task<IEnumerable<Product?>> GetById(IEnumerable<Guid> id, IEnumerable<Product> product)
        {
            if (!id.Any())
            {
                throw new IdentityProductInvalidException(id);
            }

            var firstId = id.FirstOrDefault();
            if (!product.Any(p => p.Id == firstId))
            {
                if (!product.Any(c => c.CategoryId == firstId))
                {
                    throw new ProductNotFoundExceptions(product);
                }
            }

            try
            {
                await _crudDomainRepository.GetByIdAsync(id, product);

                if (!product.Any())
                {
                    throw new ProductNotFoundExceptions(product);
                }

                return product;
            }
            catch (DomainException)
            {
                throw; // Re-throws the caught DomainException
            }


        }

        /// <summary>
        /// Gets all products from the repository.
        /// </summary>
        /// <returns>The list of all products.</returns>
        /// <exception cref="ProductNotFoundExceptions">Thrown when the product list is null or empty.</exception>
        public async Task<IEnumerable<Product?>> GetAll(IEnumerable<Product> products)
        {
            if (!products.Any())
            {
                throw new ProductNotFoundExceptions(products);
            }

            try
            {
                await _crudDomainRepository.GetAllAsync(products);

                if (products == null || !products.Any())
                {
                    throw new ProductNotFoundExceptions(products ?? []);
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message);
            }
        }


        /// <summary>
        /// Checks if a specified condition exists asynchronously by calling the repository's ExistsAsync method.
        /// </summary>
        /// <param name="exists">A boolean value indicating whether the condition to check exists.</param>
        /// <returns>A Task representing the asynchronous operation, with a boolean result indicating the existence of the condition.</returns>
        /// <exception cref="DomainException">Thrown when an error occurs during the repository check.</exception>
        public async Task<bool> ExistsAsync(bool exists)
        {
            try
            {
             
                if (exists)
                {
                    await _crudDomainRepository.ExistsAsync(exists);

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (DomainException)
            {

                throw;
            }
        }
    }
    #endregion
}