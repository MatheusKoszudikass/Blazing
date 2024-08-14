using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Interfaces.Services;

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
            {
                throw new ProductExceptions.ProductNotFoundException([]);
            }

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
        public async Task<IEnumerable<Product?>> Update(IEnumerable<Guid> id, IEnumerable<Product> products, IEnumerable<Product> productsUpdate)
        {
            if (id == null || !id.Any() || id.Contains(Guid.Empty))
            {
                throw new ProductExceptions.IdentityProductInvalidException(id ?? []);
            }
            else if (!products.Any(p => id.Contains(p.Id)) || !productsUpdate.Any(p => id.Contains(p.Id)))
            {
                if (!products.Any(p => id.Contains(p.Id)))
                {
                    throw new ProductExceptions.IdentityProductInvalidException(id ?? []);
                }
                if (!productsUpdate.Any(p => id.Contains(p.Id)))
                {
                    throw new ProductExceptions.ProductNotFoundException(products);
                }
            }
            else if (AreProductCollectionsEqual(products, productsUpdate))
            {
                throw new ProductExceptions.ProductAlreadyExistsException(productsUpdate);
            }

            try
                {
                    var productDict = products.ToDictionary(p => p.Id);
                    var productUpdateDict = productsUpdate.ToDictionary(p => p.Id);
                    var filteredId = id.Where(id => id != Guid.Empty).ToList();

                    var acceptedProducts = new List<Product>();

                    foreach (var productId in filteredId)
                    {

                        if (productDict.TryGetValue(productId, out var originalProduct) &&
                            productUpdateDict.TryGetValue(productId, out var updatedProduct))
                        {
                                updatedProduct.DataCreated = originalProduct.DataCreated;
                                updatedProduct.Assessment.DataCreated = originalProduct.Assessment.DataCreated;
                                updatedProduct.Dimensions.DataCreated = originalProduct.Dimensions.DataCreated;
                                updatedProduct.Attributes.DataCreated = originalProduct.Attributes.DataCreated;
                                updatedProduct.Availability.DataCreated = originalProduct.Availability.DataCreated;
                                updatedProduct.Image.DataCreated = originalProduct.Image.DataCreated;

                                updatedProduct.DataUpdated = DateTime.Now;
                                updatedProduct.Assessment.DataUpdated = DateTime.Now;
                                updatedProduct.Dimensions.DataUpdated = DateTime.Now;
                                updatedProduct.Attributes.DataUpdated = DateTime.Now;
                                updatedProduct.Availability.DataUpdated = DateTime.Now;
                                updatedProduct.Image.DataUpdated = DateTime.Now;


                                acceptedProducts.Add(updatedProduct);
                            
                        }
                    }

                    await Task.CompletedTask;
                    return acceptedProducts;
                }
                catch (DomainException)
                {
                    throw;
                }
            
        }

        /// <summary>
        /// Determines whether two collections of products are equal by comparing their contents.
        /// </summary>
        /// <param name="collection1">The first collection of products.</param>
        /// <param name="collection2">The second collection of products.</param>
        /// <returns><c>true</c> if the collections contain the same products with the same properties; otherwise, <c>false</c>.</returns>
        private static bool AreProductCollectionsEqual(IEnumerable<Product> collection1, IEnumerable<Product> collection2)
        {
            var list1 = collection1.ToList();
            var list2 = collection2.ToList();

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (!AreProductsEqual(list1[i], list2[i]))
                    return false;
            }

            return true;
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

            return product1.Id == product2.Id &&
                   product1.Name == product2.Name &&
                   product1.Description == product2.Description &&
                   product1.Price == product2.Price &&
                   product1.Currency == product2.Currency &&
                   product1.CategoryId == product2.CategoryId &&
                   product1.Brand == product2.Brand &&
                   product1.SKU == product2.SKU &&
                   product1.StockQuantity == product2.StockQuantity &&
                   product1.StockLocation == product2.StockLocation &&

                   product1.DimensionsId == product2.DimensionsId &&

                   product1.Dimensions.Weight == product2.Dimensions.Weight &&
                   product1.Dimensions.Height == product2.Dimensions.Height &&
                   product1.Dimensions.Width == product2.Dimensions.Width &&
                   product1.Dimensions.Depth == product2.Dimensions.Depth &&
                   product1.Dimensions.Unit == product2.Dimensions.Unit &&

                   product1.AssessmentId == product2.AssessmentId &&

                   product1.Assessment.Average == product2.Assessment.Average &&
                   product1.Assessment.NumberOfReviews == product2.Assessment.NumberOfReviews &&
                   product1.Assessment.RevisionId == product2.Assessment.RevisionId &&

                   product1.AttributesId == product2.AttributesId &&

                   product1.Attributes.Color == product2.Attributes.Color &&
                   product1.Attributes.Material == product2.Attributes.Material &&
                   product1.Attributes.Model == product2.Attributes.Model &&

                   product1.AvailabilityId == product2.AvailabilityId &&

                   product1.Availability.IsAvailable == product2.Availability.IsAvailable &&
                   product1.Availability.EstimatedDeliveryDate == product2.Availability.EstimatedDeliveryDate &&

                   product1.ImageId == product2.ImageId &&

                   product1.Image.Url == product2.Image.Url &&
                   product1.Image.AltText == product2.Image.AltText;
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
                throw new ProductExceptions.IdentityProductInvalidException(id);
            }
            else if(!product.Any(p => p.Id == id.FirstOrDefault()))
            {
                throw new ProductExceptions.ProductNotFoundException(product);
            }

            try
            {
                if (!product.Any())
                {
                    throw new ProductExceptions.ProductNotFoundException([]);
                }

                foreach (var item in product)
                {
                    item.DataDeleted = DateTime.Now;

                    if (item.Dimensions != null)
                    {
                        item.Dimensions.DataDeleted = DateTime.Now;
                    }

                    if (item.Assessment != null)
                    {
                        item.Assessment.DataDeleted = DateTime.Now;
                    }

                    if (item.Attributes != null)
                    {
                        item.Attributes.DataDeleted = DateTime.Now;
                    }

                    if (item.Availability != null)
                    {
                        item.Availability.DataDeleted = DateTime.Now;
                    }

                    if (item.Image != null)
                    {
                        item.Image.DataDeleted = DateTime.Now;
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
        /// Gets products associated with a specific ID.
        /// </summary>
        /// <param name="id">The list of ID to filter the products.</param>
        /// <returns>The list of products associated with the given id.</returns>
        /// <exception cref="IdentityProductInvalidException">Thrown when the given ID is invalid.</exception>
        /// <exception cref="ProductNotFoundExceptions">Throws when no products not found .</exception>
        public async Task<IEnumerable<Product?>> GetById(IEnumerable<Guid> id, IEnumerable<Product> product, CancellationToken cancellationToken)
        {
            if (!id.Any())
            {
                throw new ProductExceptions.IdentityProductInvalidException(id);
            }

            var firstId = id.FirstOrDefault();
            if (!product.Any(p => p.Id == firstId))
            {
                if (!product.Any(c => c.CategoryId == firstId))
                {
                    throw new ProductExceptions.ProductNotFoundException(product);
                }
            }

            try
            {
                if (!product.Any())
                {
                    throw new ProductExceptions.ProductNotFoundException(product);
                }
                await Task.CompletedTask;
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
        public async Task<IEnumerable<Product?>> GetAll(IEnumerable<Product> products, CancellationToken cancellationToken)
        {
            if (!products.Any())
            {
                throw new ProductExceptions.ProductNotFoundException(products);
            }

            try
            {
                if (products == null || !products.Any())
                {
                    throw new ProductExceptions.ProductNotFoundException(products ?? []);
                }

                await Task.CompletedTask;
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
        public async Task<bool> ExistsAsync(bool id, bool existsName, IEnumerable<Product> products)
        {
            try
            {
             
                if (id)
                {
                    var produtsId = products.Select(p => p.Id).ToList();
                    throw new ProductExceptions.IdentityProductInvalidException(produtsId, id);
                }
                else if (existsName)
                {
                    var nameProduct = products.Select(p => p.Name).ToList();

                    throw new ProductExceptions.ProductAlreadyExistsException(nameProduct);

                }
                await Task.CompletedTask;
                return id;
            }
            catch (DomainException)
            {

                throw;
            }
        }
    }
    #endregion
}