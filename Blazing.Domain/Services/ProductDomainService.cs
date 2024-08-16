using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;
using System.Text;

namespace Blazing.Domain.Services
{
    #region Domain product service.
    public class ProductDomainService: ICrudDomainService<Product>
    {
       

        /// <summary>
        /// Adds a list of products to the repository.
        /// </summary>
        /// <param name="product">The list of products to be added.</param>
        /// <returns>The list of products that have been added.</returns>
        /// <exception cref="ProductNotFoundExceptions">Thrown when the product list is null or empty.</exception>
        public async Task<IEnumerable<Product?>> Add(IEnumerable<Product> product)
        {
            if (product == null || !product.Any())
                throw new ProductExceptions.ProductNotFoundException([]);

            try
            { 
                foreach  ( var item in product)
                {
                   item.DataCreated = DateTime.Now;
                   item.DataUpdated = null;
                   item.DataDeleted = null;
                    if (item.Dimensions != null)
                        item.Dimensions.DataCreated = DateTime.Now;
                        item.Dimensions.DataUpdated = null;
                        item.Dimensions.DataDeleted = null;

                    if (item.Assessment != null)
                        item.Assessment.DataCreated = DateTime.Now;
                        item.Assessment.DataUpdated = null;
                        item.Assessment.DataDeleted = null;

                    if (item.Attributes != null)
                        item.Attributes.DataCreated = DateTime.Now;
                        item.Attributes.DataUpdated = null;
                        item.Attributes.DataDeleted = null;
                    

                    if (item.Availability != null)
                        item.Availability.DataCreated = DateTime.Now;
                        item.Availability.DataUpdated = null;
                        item.Availability.DataDeleted = null;
                    

                    if (item.Image != null)
                        item.Image.DataCreated = DateTime.Now;
                        item.Image.DataUpdated = null;
                        item.Image.DataDeleted = null;
                    
                }

                return await Task.FromResult(product);
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a collection of products based on the provided IDs and product data.
        /// </summary>
        /// <param name="id">The IDs of the products to update.</param>
        /// <param name="products">The collection of existing products with the current data.</param>
        /// <param name="productsUpdate">The collection of products containing the updated data.</param>
        /// <returns>A collection of updated products that were found and successfully updated.</returns>
        /// <exception cref="ProductExceptions.IdentityProductInvalidException">Thrown when the provided IDs are invalid or empty.</exception>
        /// <exception cref="ProductExceptions.ProductNotFoundException">Thrown when no products matching the provided IDs are found in the current product collection.</exception>
        /// <exception cref="ProductExceptions.ProductAlreadyExistsException">Thrown when the updated product collection is identical to the existing product collection.</exception>
        /// <exception cref="DomainException">Thrown when a domain-related error occurs during the update process.</exception>
        public async Task<IEnumerable<Product?>> Update(IEnumerable<Guid> id, IEnumerable<Product> originalProducts, IEnumerable<Product> updatedProducts)
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

                return await Task.FromResult(modifiedProducts);
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

        private static string NormalizeString(string? input)
        {
            if (input == null)
                return string.Empty;
            else
                return input.Trim().Normalize(NormalizationForm.FormC).ToLowerInvariant();
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

                return await Task.FromResult(product);


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
                
     
                return await Task.FromResult(product);
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
        public async Task<IEnumerable<Product?>> GetAll(IEnumerable<Product> products, CancellationToken cancellationToken)
        {
            if (!products.Any())
                throw new ProductExceptions.ProductNotFoundException(products);
            

            try
            {
                if (products == null || !products.Any())
                    throw new ProductExceptions.ProductNotFoundException(products ?? []);


                return await Task.FromResult(products);
            }
            catch (DomainException)
            {
                throw;
            }
        }


        /// <summary>
        /// Checks if a specified condition exists asynchronously by calling the repository's ExistsAsync method.
        /// </summary>
        /// <param name="existsName">A boolean value indicating whether the condition to check exists.</param>
        /// <returns>A Task representing the asynchronous operation, with a boolean result indicating the existence of the condition.</returns>
        /// <exception cref="DomainException">Thrown when an error occurs during the repository check.</exception>
        public async Task<bool> ExistsAsync(bool id, bool existsName, IEnumerable<Product> products)
        {
            try
            {
                var produtsId = products.Select(p => p.Id).ToList();
                var nameProduct = products.Select(p => p.Name).ToList();

                if (id)
                    throw new ProductExceptions.IdentityProductInvalidException(produtsId, id);
                
                else if (existsName)
                    throw new ProductExceptions.ProductAlreadyExistsException(nameProduct);


                return await Task.FromResult(id);
            }
            catch (DomainException)
            {

                throw;
            }
        }
    }
    #endregion
}