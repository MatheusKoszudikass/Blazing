using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Product;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Repository;
using Blazing.infrastructure.Dependency;
using Blazing.infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace Blazing.infrastructure.Service
{
    #region Responsibility for searching data in the database in the product table.
    /// <summary>
    /// Repository class for managing Product domain objects.
    /// </summary>
    public class ProductInfrastructureRepository(DependencyInjection dbContext, ProductAppService productInfrastructureRepository) : IProductInfrastructureRepository
    {
        private readonly DependencyInjection _dependencyInjection = dbContext;
        private readonly ProductAppService _productAppService = productInfrastructureRepository;


        /// <summary>
        /// Adds a product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        public async Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDto)
        {
            var productResult = await _productAppService.AddProducts(productDto);

            var resultExistsProduct = await ExistsAsyncProduct(productDto); 

            if (resultExistsProduct)
            {
                productDto = [];
                return productDto;
            }
            else
            {
                var product = _dependencyInjection._mapper.Map<IEnumerable<Product>>(productResult);

                await _dependencyInjection._appContext.Products.AddRangeAsync(product);

                await _dependencyInjection._appContext.SaveChangesAsync();

                return productDto;
            }

        
        }

        /// <summary>
        /// Updates a product in the repository.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDtos">The updated product.</param>
        /// <returns>The updated product.</returns>
        public async Task<IEnumerable<ProductDto?>> UpdateProduct(IEnumerable<Guid> id, IEnumerable<ProductDto> productDtos)
        {
            await _productAppService.UpdateProduct(id, productDtos);

            var existingProducts = await _dependencyInjection._appContext.Products
                                      .Include(d => d.Dimensions)

                                      .Include(a => a.Attributes)
                                      .Include(a => a.Availability)
                                      .Include(i => i.Image)
                                      .Where(p => id.Contains(p.Id))
                                      .ToListAsync();

            var existingProductsDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(existingProducts);

            await _productAppService.UpdateProduct(id, existingProductsDto);

            foreach (var product in existingProducts)
            {
                var updatedProductDto = productDtos.FirstOrDefault(dto => dto.Id == product.Id);
                if (updatedProductDto != null)
                {
                    // Atualizar as propriedades principais do produto
                    product.Name = updatedProductDto.Name;
                    product.Description = updatedProductDto.Description;
                    product.Price = updatedProductDto.Price;
                    product.Currency = updatedProductDto.Currency;
                    product.CategoryId = updatedProductDto.CategoryId;
                    product.Brand = updatedProductDto.Brand;
                    product.SKU = updatedProductDto.SKU;
                    product.StockQuantity = updatedProductDto.StockQuantity;
                    product.AssessmentId = updatedProductDto.AssessmentId;

                    product.DimensionsId = updatedProductDto.DimensionsId;
                    if (updatedProductDto.Dimensions != null)
                    {
                        product.Dimensions ??= new Dimensions();
                        product.Dimensions.Depth = updatedProductDto.Dimensions.Depth;
                        product.Dimensions.Width = updatedProductDto.Dimensions.Width;
                        product.Dimensions.Height = updatedProductDto.Dimensions.Height;
                    }

                    product.AttributesId = updatedProductDto.AttributesId;
                    if (updatedProductDto.Attributes != null)
                    {
                        product.Attributes ??= new Attributes();
                        product.Attributes.Color = updatedProductDto.Attributes.Color;
                        product.Attributes.Material = updatedProductDto.Attributes.Material;
                        product.Attributes.Model = updatedProductDto.Attributes.Model;
                    }

                    product.AvailabilityId = updatedProductDto.AvailabilityId;
                    if (updatedProductDto.Availability != null)
                    {
                        product.Availability ??= new Availability();
                        product.Availability.IsAvailable = updatedProductDto.Availability.IsAvailable;
                        product.Availability.EstimatedDeliveryDate = updatedProductDto.Availability.EstimatedDeliveryDate;
                    }

                    product.ImageId = updatedProductDto.ImageId;
                    if (updatedProductDto.Image != null)
                    {
                        product.Image ??= new Image();
                        product.Image.Url = updatedProductDto.Image.Url;
                        product.Image.AltText = updatedProductDto.Image.AltText;
                    }

                }
            }

            await _dependencyInjection._appContext.SaveChangesAsync();

            // Retornar os produtos atualizados como DTOs
            var updatedProducts = await _dependencyInjection._appContext.Products
                                          .Include(d => d.Dimensions)
                                          .Include(a => a.Attributes)
                                          .Include(a => a.Availability)
                                          .Include(i => i.Image)
                                          .Where(p => id.Contains(p.Id))
                                          .ToListAsync();

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(updatedProducts).AsEnumerable();

            return productResultDto;
        }



        /// <summary>
        /// Gets products by category ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The products in the category.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(IEnumerable<Guid> id)
        {

            var ProductCategory = await _dependencyInjection._appContext.Products
                                 .Include(d => d.Dimensions)
                                 .Include(a => a.Attributes)
                                 .Include(a => a.Availability)
                                 .Include(i => i.Image)
                                 .Where(p => id.Contains(p.CategoryId)).ToListAsync();

            var categoryResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto?>>(ProductCategory);

            await _productAppService.GetProductsByCategoryId(id, categoryResultDto);

            return categoryResultDto;

        }

        /// <summary>
        /// Deletes products by ID.
        /// </summary>
        /// <param name="id">The IDs of the products to delete.</param>
        /// <returns>The deleted products.</returns>
        public async Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> id)
        {
            var products = await _dependencyInjection._appContext.Products
                .Where(p => id.Contains(p.Id))
                .ToListAsync();

            var productDtos = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(products);

            await _productAppService.DeleteProducts(id, productDtos);

            _dependencyInjection._appContext.Products.RemoveRange(products);

            await _dependencyInjection._appContext.SaveChangesAsync();

            return productDtos;
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductById(IEnumerable<Guid> id)
        {
            var product = await _dependencyInjection._appContext.Products
                                .Include(d => d.Dimensions)
                                .Include(a => a.Attributes)
                                .Include(a => a.Availability)
                                .Include(i => i.Image)
                                .Where(p => id.Contains(p.Id)).ToListAsync();

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(product);

            var productResult = await _productAppService.GetProductById(id, productResultDto);

            return productResultDto;
        }


        /// <summary>
        /// Retrieves all products from the database, including related entities such as dimensions, assessments, attributes, and images.
        /// </summary>
        /// <returns>A collection of Product objects.</returns>
        public async Task<IEnumerable<ProductDto?>> GetAll()
        {
            var products = await _dependencyInjection._appContext.Products
                                .Include(d => d.Dimensions)
                                .Include(a => a.Attributes)
                                .Include(a => a.Availability)
                                .Include(i => i.Image)
                                .ToListAsync();

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(products);

            var productsResult = await _productAppService.GetAllProduct(productResultDto);

            return productsResult;
        }

        /// <summary>
        /// Checks if any products with the specified names exist in the database.
        /// </summary>
        /// <param name="productNames">A collection of product names to check.</param>
        /// <returns>True if any products with the specified names exist, false otherwise.</returns>
        public async Task<bool> ExistsAsyncProduct(IEnumerable<ProductDto?> productNames)
        {
            var productExists = await _dependencyInjection._appContext.Products.AnyAsync(p => productNames.Select(x => x.Name).Contains(p.Name));
            
            var productResultDto = await _productAppService.ExistsProduct(productExists);

            return productResultDto;
        }
    }
    #endregion
}
