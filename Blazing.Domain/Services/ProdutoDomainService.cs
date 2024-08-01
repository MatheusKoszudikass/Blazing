using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;

namespace Blazing.Domain.Services
{
    #region Domain product service.
    public class ProdutoDomainService(IProductDomainRepository produtoRepository) : IProdutoDomainService
    {
        private readonly IProductDomainRepository _produtoRepository = produtoRepository;

        /// <summary>
        /// Adds a list of products to the repository.
        /// </summary>
        /// <param name="product">The list of products to be added.</param>
        /// <returns>The list of products that have been added.</returns>
        /// <exception cref="ProductNotFoundExceptions">Thrown when the product list is null or empty.</exception>
        /// <exception cref="ExistingProductException">Launched when one or more products already exist in the repository.</exception>
        public async Task<IEnumerable<Product?>> AddProducts(IEnumerable<Product> product)
        {
            if (product == null || !product.Any())
            {
                throw new ProductNotFoundExceptions([]);
            }

            try
            {
                var nomesProdutos = product.Select(n => n.Name).ToList();

                if (await _produtoRepository.ExistsAsync(nomesProdutos.AsEnumerable()))
                {
                    throw new ExistingProductException(nomesProdutos);
                }

                await _produtoRepository.AddAsync(product);

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
        public async Task<Product?> UpdateProduct(Guid id, Product product)
        {
            if (id == Guid.Empty || product.Id != id)
            {
                throw new IdentityProductInvalidException(id);
            }

            try
            {
                 await _produtoRepository.UpdateAsync(id, product);

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
        /// Gets products associated with a specific category ID.
        /// </summary>
        /// <param name="categoryId">The category ID to filter the products.</param>
        /// <returns>The list of products associated with the given category.</returns>
        /// <exception cref="IdentityProductInvalidException">Thrown when the given category ID is invalid.</exception>
        /// <exception cref="ProductNotFoundExceptions">Throws when no products are found for the given category.</exception>
        public async Task<IEnumerable<Product?>> GetProductsByCategoryId(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new IdentityProductInvalidException(categoryId);
            }

            try
            {
                var produtos = await _produtoRepository.GetByCategoryIdAsync(categoryId);

                if (!produtos.Any())
                {
                    throw new ProductNotFoundExceptions([]);
                }

                return produtos;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes products based on a list of provided IDs.
        /// </summary>
        /// <param name="ids">The list of product IDs to be deleted.</param>
        /// <returns>The list of products that were deleted.</returns>
        /// <exception cref="IdentityProductInvalidException">Thrown when the list of provided IDs is empty.</exception>
        /// <exception cref="ProductNotFoundExceptions">Throws when no product is deleted.</exception>
        public async Task<IEnumerable<Product?>> DeleteProducts(IEnumerable<Guid> ids)
        {
            if (!ids.Any())
            {
                throw new IdentityProductInvalidException(ids);
            }

            try
            {
                var produtosDeletados = await _produtoRepository.DeleteByIdAsync(ids);

                if (!produtosDeletados.Any())
                {
                    throw new ProductNotFoundExceptions([]);
                }

                return produtosDeletados;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a specific product based on the given ID.
        /// </summary>
        /// <param name="id">The ID of the product to get.</param>
        /// <returns>The product corresponding to the given ID.</returns>
        /// <exception cref="IdentityProductInvalidException">Thrown when the provided ID is invalid.</exception>
        /// <exception cref="ProductInvalidExceptions">Thrown when the product with the given ID is invalid.</exception>
        public async Task<Product?> GetProductById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new IdentityProductInvalidException(id);
            }

            try
            {
                var produto = await _produtoRepository.GetByIdAsync(id);

                return produto ?? throw new IdentityProductInvalidException(id);
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all products from the repository.
        /// </summary>
        /// <returns>The list of all products.</returns>
        /// <exception cref="ProductNotFoundExceptions">Thrown when the product list is null or empty.</exception>
        public async Task<IEnumerable<Product?>> GetAll()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllAsync();

                if (produtos == null || !produtos.Any())
                {
                    throw new ProductNotFoundExceptions([]);
                }

                return produtos;
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message);
            }
        }
    }
    #endregion
}