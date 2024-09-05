using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Category;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;
using System.Text;

namespace Blazing.Domain.Services
{
    #region Domain product service.
    public class ProductDomainService : ICrudDomainService<Product>
    {
        /// <summary>
        /// Adds a list of products to the repository.
        /// </summary>
        /// <param name="product">The list of products to be added.</param>
        /// <returns>The list of products that have been added.</returns>
        /// <exception cref="ProductExceptions.ProductNotFoundException">Thrown when the product list is null or empty.</exception>
        public async Task<IEnumerable<Product?>> Add(IEnumerable<Product> product, CancellationToken cancellationToken)
        {
            if (product == null || !product.Any())
                throw new ProductExceptions.ProductNotFoundException(product ?? []);

            try
            { 
                foreach  ( var item in product)
                {
                   item.DataCreated = DateTime.Now;
                   item.DataUpdated = null;
                   item.DataDeleted = null;
                    if (item.Dimensions != null)
                    {
                        item.Dimensions.DataCreated = DateTime.Now;
                        item.Dimensions.DataUpdated = null;
                        item.Dimensions.DataDeleted = null;
                    }

                    if (item.Assessment != null)
                    {
                        item.Assessment.DataCreated = DateTime.Now;
                        item.Assessment.DataUpdated = null;
                        item.Assessment.DataDeleted = null;
                    }

                    if (item.Attributes != null)
                    {
                        item.Attributes.DataCreated = DateTime.Now;
                        item.Attributes.DataUpdated = null;
                        item.Attributes.DataDeleted = null;
                    }


                    if (item.Availability != null)
                    {
                        item.Availability.DataCreated = DateTime.Now;
                        item.Availability.DataUpdated = null;
                        item.Availability.DataDeleted = null;
                    }


                    if (item.Image != null)
                    {
                        item.Image.DataCreated = DateTime.Now;
                        item.Image.DataUpdated = null;
                        item.Image.DataDeleted = null;
                    }
                }

                await Task.CompletedTask;

                return product;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates products based on their IDs.
        /// Throws IdentityProductInvalidException if no IDs are provided,
        /// and ProductNotFoundException if no products match the given IDs.
        /// Throws ProductAlreadyExistsException if all products already exist.
        /// </summary>
        /// <param name="id">The IDs of the products to update.</param>
        /// <param name="originalProducts">The original products.</param>
        /// <param name="updatedProducts">The updated products.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated products.</returns>
        /// <exception cref="DomainException">Thrown when a domain-related error occurs during the update process.</exception>
        public async Task<IEnumerable<Product?>> Update(IEnumerable<Guid> id, IEnumerable<Product> originalProducts, IEnumerable<Product> updatedProducts, CancellationToken cancellationToken)
        {
            if (id == null || !id.Any() || id.Contains(Guid.Empty))
                throw new ProductExceptions.IdentityProductInvalidException(id ?? []);


            var productsDict  = originalProducts.Where(p => id.Contains(p.Id)).ToDictionary(p => p.Id);
            var updatesDict = updatedProducts.Where(p => id.Contains(p.Id)).ToDictionary(p => p.Id);

            if (productsDict.Count == 0)
                throw new ProductExceptions.ProductNotFoundException(originalProducts);

            try
            {
                var modifiedProducts = updatesDict
                 .Where(update => productsDict.TryGetValue(update.Key, out var original) && !AreProductsEqual(original, update.Value))
                 .Select(update =>
                 {
                   var updatedProduct = update.Value;
                   updatedProduct.DataCreated = productsDict[update.Key].DataCreated;
                   updatedProduct.DataUpdated = DateTime.Now;
                   return updatedProduct;
                 }).ToList();

                if(modifiedProducts.Count == 0)
                   throw new ProductExceptions.ProductAlreadyExistsException(updatedProducts);

                await Task.CompletedTask;

                return modifiedProducts;
            }
            catch (DomainException)
            {

                throw;
            }
            
        }

        /// <summary>
        /// Compares two products to determine if they are equal based on their properties.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns><c>true</c> if the products have the same values for all relevant properties; otherwise, <c>false</c>.</returns>
        private static bool AreProductsEqual(Product product1, Product product2)
        {
            if (product1 == null && product2 == null)
                return false;

            return   product1.Id == product2.Id &&
                     NormalizeString(product1.Name) == NormalizeString(product2.Name) &&
                     NormalizeString(product1.Description) == NormalizeString(product2.Description) &&
                     NormalizeString(product1.Price.ToString()) == NormalizeString(product2.Price.ToString()) &&
                     NormalizeString(product1.Currency) == NormalizeString(product2.Currency) &&
                     product1.CategoryId == product2.CategoryId &&
                     NormalizeString(product1.Brand) == NormalizeString(product2.Brand) &&
                     NormalizeString(product1.SKU) == NormalizeString(product2.SKU) &&
                     product1.StockQuantity == product2.StockQuantity &&
                     NormalizeString(product1.StockLocation) == NormalizeString(product2.StockLocation) &&

                     product1.DimensionsId == product2.DimensionsId &&

                     product1.Dimensions.Weight == product2.Dimensions.Weight &&
                     product1.Dimensions.Height == product2.Dimensions.Height &&
                     product1.Dimensions.Width == product2.Dimensions.Width &&
                     product1.Dimensions.Depth == product2.Dimensions.Depth &&
                     NormalizeString(product1.Dimensions.Unit) == NormalizeString(product2.Dimensions.Unit) &&

                     product1.AssessmentId == product2.AssessmentId &&

                     product1.Assessment.Average == product2.Assessment.Average &&
                     product1.Assessment.NumberOfReviews == product2.Assessment.NumberOfReviews &&
                     product1.Assessment.RevisionId == product2.Assessment.RevisionId &&

                     product1.AttributesId == product2.AttributesId &&

                     NormalizeString(product1.Attributes.Color) == NormalizeString(product2.Attributes.Color) &&
                     NormalizeString(product1.Attributes.Material) == NormalizeString(product2.Attributes.Material) &&
                     NormalizeString(product1.Attributes.Model) == NormalizeString(product2.Attributes.Model) &&

                     product1.AvailabilityId == product2.AvailabilityId &&

                     product1.Availability.IsAvailable == product2.Availability.IsAvailable &&
                     product1.Availability.EstimatedDeliveryDate == product2.Availability.EstimatedDeliveryDate &&

                     product1.ImageId == product2.ImageId &&

                     NormalizeString(product1.Image.Url) == NormalizeString(product2.Image.Url) &&
                     NormalizeString(product1.Image.AltText) == NormalizeString(product2.Image.AltText);
        }

        /// <summary>
        /// Normalizes a string by trimming leading and trailing whitespace, normalizing it to the specified form,
        /// and converting it to lowercase.
        /// If the input is null, returns an empty string.
        /// </summary>
        /// <param name="input">The string to normalize. Can be null.</param>
        /// <returns>The normalized string. If the input is null, returns an empty string.</returns>
        private static string NormalizeString(string? input)
        {
            return input == null ? string.Empty : input.Trim().Normalize(NormalizationForm.FormC).ToLowerInvariant();
        }

        /// <summary>
        /// Deletes products based on a list of provided IDs.
        /// </summary>
        /// <param name="id">The list of product IDs to be deleted.</param>
        /// <param name="product">The list of products to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>The list of products that were deleted.</returns>
        /// <exception cref="ProductExceptions.IdentityProductInvalidException">Thrown when the list of provided IDs is empty.</exception>
        /// <exception cref="ProductExceptions.ProductNotFoundException">Throws when no product is deleted.</exception>
        public async Task<IEnumerable<Product?>> Delete(IEnumerable<Guid> id, IEnumerable<Product> product, CancellationToken cancellationToken)
        {
            if (!id.Any())
                throw new ProductExceptions.IdentityProductInvalidException(id);

            else if(!product.Any(p => p.Id == id.FirstOrDefault()))
                throw new ProductExceptions.ProductNotFoundException(product);

            try
            {
                foreach (var item in product)
                {
                    item.DataDeleted = DateTime.Now;

                    if (item.Dimensions != null)
                        item.Dimensions.DataDeleted = DateTime.Now;
                    

                    if (item.Assessment != null)
                        item.Assessment.DataDeleted = DateTime.Now;

                    if (item.Attributes != null)
                        item.Attributes.DataDeleted = DateTime.Now;

                    if (item.Availability != null)
                        item.Availability.DataDeleted = DateTime.Now;

                    if (item.Image != null)
                        item.Image.DataDeleted = DateTime.Now;
                }

                await Task.CompletedTask;
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
        /// <exception cref="ProductExceptions.IdentityProductInvalidException">Thrown when the given ID is invalid.</exception>
        /// <exception cref="ProductExceptions.ProductNotFoundException">Throws when no products not found .</exception>
        public async Task<IEnumerable<Product?>> GetById(IEnumerable<Guid> id, IEnumerable<Product> product, CancellationToken cancellationToken)
        {
            if (!id.Any())
                throw new ProductExceptions.IdentityProductInvalidException(id);
            
            var firstId = id.FirstOrDefault();
            if (!product.Any(p => p.Id == firstId))
                if (!product.Any(c => c.CategoryId == firstId))
                    throw new ProductExceptions.ProductNotFoundException(product);
            try
            {
                if (!product.Any())
                    throw new ProductExceptions.ProductNotFoundException(product);

                await Task.CompletedTask;

                return product;
            }
            catch (DomainException)
            {
                throw; // Re-throws the caught DomainException
            }
        }

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <param name="products">The list of products to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of products.</returns>
        /// <exception cref="ProductExceptions.ProductNotFoundException">Thrown when no products are found.</exception>
        public async Task<IEnumerable<Product?>> GetAll(IEnumerable<Product?> products,
            CancellationToken cancellationToken)
        {
            if (products == null || !products.Any())
                throw new ProductExceptions.ProductNotFoundException(products ?? []);
            try
            {
                await Task.CompletedTask;

                return products;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if the specified products exist in the repository.
        /// Throws an exception if the products already exist.
        /// </summary>
        /// <param name="id">A flag indicating whether the products exist by ID.</param>
        /// <param name="existsName"></param>
        /// <param name="products">The products to check for existence.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the products exist (<c>true</c> if they exist, <c>false</c> otherwise).</returns>
        /// <exception cref="ProductExceptions.IdentityProductInvalidException">Thrown if the products already exist by ID.</exception>
        /// <exception cref="ProductExceptions.ProductAlreadyExistsException">Thrown if the products already exist by name.</exception>
        public async Task<bool> ExistsAsync(bool id, bool existsName, IEnumerable<Product> products,
            CancellationToken cancellationToken)
        {
            try
            {
                var productId = products.Select(p => p.Id).ToList();
                var nameProduct = products.Select(p => p.Name).ToList();

                if (id)
                    throw new ProductExceptions.IdentityProductInvalidException(productId, id);
                
                else if (existsName)
                    throw new ProductExceptions.ProductAlreadyExistsException(nameProduct);

                await Task.CompletedTask;

                return id || existsName;
            }
            catch (DomainException)
            {
                throw;
            }
        }
    }
    #endregion
}