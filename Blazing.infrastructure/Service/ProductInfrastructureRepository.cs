﻿using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Product;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
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
    public class ProductInfrastructureRepository(DependencyInjection dbContext, IProductAppService<ProductDto> productInfrastructureRepository) : IProductInfrastructureRepository
    {
        private readonly DependencyInjection _dependencyInjection = dbContext;
        private readonly IProductAppService<ProductDto> _productAppService = productInfrastructureRepository;


        /// <summary>
        /// Adds a product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        public async Task<IEnumerable<ProductDto?>> AddProducts(IEnumerable<ProductDto> productDto, CancellationToken cancellationToken )
        {
            await ExistsAsyncProduct(productDto, cancellationToken);

            var productDtoResult = await _productAppService.AddProducts(productDto, cancellationToken);

            var productResult  = _dependencyInjection._mapper.Map<IEnumerable<Product>>(productDtoResult);

            await _dependencyInjection._appContext.Products.AddRangeAsync(productResult, cancellationToken);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return productDtoResult;

        
        }

        /// <summary>
        /// Updates a product in the repository.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDtos">The updated product.</param>
        /// <returns>The updated product.</returns>
        public async Task<IEnumerable<ProductDto?>> UpdateProduct(IEnumerable<Guid> id, IEnumerable<ProductDto> productDtoUpdate, CancellationToken cancellationToken)
        {
            if (!id.Any())
            {
                throw new ProductExceptions.IdentityProductInvalidException(id);
            }

            // Obtém os produtos existentes com as propriedades relacionadas carregadas
            var existingProducts = await _dependencyInjection._appContext.Products
                .Include(a => a.Assessment)
                    .ThenInclude(r => r.RevisionDetail)
                .Include(d => d.Dimensions)
                .Include(a => a.Attributes)
                .Include(a => a.Availability)
                .Include(i => i.Image)
                .Where(p => id.Contains(p.Id))
                .ToListAsync(cancellationToken);

            // Mapeia os produtos existentes para DTOs
            var productDtos = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(existingProducts);

            // Atualiza os produtos usando o serviço
            var productDtoUpdateResult = await _productAppService.UpdateProduct(id, productDtos, productDtoUpdate, cancellationToken);

            // Atualiza as propriedades das entidades existentes com base nos DTOs atualizados
            foreach (var updatedProductDto in productDtoUpdate)
            {
                var existingProduct = existingProducts.SingleOrDefault(p => p.Id == updatedProductDto.Id);
                if (existingProduct != null)
                {
                    _dependencyInjection._mapper.Map(updatedProductDto, existingProduct);
                }
            }

            // Salva as alterações no banco de dados
            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return productDtoUpdateResult;
        }



        /// <summary>
        /// Gets products by category ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The products in the category.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryId(IEnumerable<Guid> id, CancellationToken cancellationToken )
        {

            var ProductCategory = await _dependencyInjection._appContext.Products
                                         .Include(a => a.Assessment)
                                                 .ThenInclude(r => r.RevisionDetail)
                                         .Include(a => a.Attributes)
                                         .Include(a => a.Availability)
                                         .Include(i => i.Image)
                                         .Where(p => id.Contains(p.CategoryId)).ToListAsync(cancellationToken);

            var categoryResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(ProductCategory);

            await _productAppService.GetProductsByCategoryId(id, categoryResultDto, cancellationToken);

            return categoryResultDto;

        }

        /// <summary>
        /// Deletes products by ID.
        /// </summary>
        /// <param name="id">The IDs of the products to delete.</param>
        /// <returns>The deleted products.</returns>
        public async Task<IEnumerable<ProductDto?>> DeleteProducts(IEnumerable<Guid> id, CancellationToken cancellationToken )
        {
            var products = await _dependencyInjection._appContext.Products
                                 .Include(d => d.Dimensions)
                                 .Include(a => a.Assessment)
                                          .ThenInclude(r => r.RevisionDetail)
                                 .Include(a => a.Attributes)
                                 .Include(a => a.Availability)
                                 .Include(i => i.Image)
                                 .Where(p => id.Contains(p.Id)).ToListAsync(cancellationToken);

            foreach (var product in products)
            {
                if (product.Dimensions != null)
                {
                    _dependencyInjection._appContext.Dimensions.Remove(product.Dimensions);
                }

                if (product.Assessment != null)
                {
                    _dependencyInjection._appContext.Assessments.Remove(product.Assessment);

                    if (product.Assessment.RevisionDetail != null && product.Assessment.RevisionDetail.Any())
                    {
                        _dependencyInjection._appContext.Revisions.RemoveRange(product.Assessment.RevisionDetail);
                    }
                }

                if (product.Attributes != null)
                {
                    _dependencyInjection._appContext.Attributes.Remove(product.Attributes);
                }

                if (product.Availability != null)
                {
                    _dependencyInjection._appContext.Availabilities.Remove(product.Availability);
                }

                if (product.Image != null)
                {
                    _dependencyInjection._appContext.Image.Remove(product.Image);
                }
            }


            var productDtos = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(products);

            await _productAppService.DeleteProducts(id, productDtos, cancellationToken);

            _dependencyInjection._appContext.Products.RemoveRange(products);

            await _dependencyInjection._appContext.SaveChangesAsync(cancellationToken);

            return productDtos;
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product.</returns>
        public async Task<IEnumerable<ProductDto?>> GetProductById(IEnumerable<Guid> id, CancellationToken cancellationToken )
        {
            var product = await _dependencyInjection._appContext.Products
                                .Include(a => a.Assessment)
                                         .ThenInclude(r => r.RevisionDetail)
                                .Include(d => d.Dimensions)
                                .Include(a => a.Attributes)
                                .Include(a => a.Availability)
                                .Include(i => i.Image)
                                .Where(p => id.Contains(p.Id)).ToListAsync(cancellationToken);

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(product);

            var productResult = await _productAppService.GetProductById(id, productResultDto, cancellationToken);

            return productResultDto;
        }


        /// <summary>
        /// Retrieves all products from the database, including related entities such as dimensions, assessments, attributes, and images.
        /// </summary>
        /// <returns>A collection of Product objects.</returns>
        public async Task<IEnumerable<ProductDto?>> GetAll(CancellationToken cancellationToken )
        {
            var products = await _dependencyInjection._appContext.Products
                                .Include(a => a.Assessment)
                                         .ThenInclude(r => r.RevisionDetail)
                                .Include(d => d.Dimensions)
                                .Include(a => a.Attributes)
                                .Include(a => a.Availability)
                                .Include(i => i.Image)
                                .ToListAsync(cancellationToken);

            var productResultDto = _dependencyInjection._mapper.Map<IEnumerable<ProductDto>>(products);

            var productsResult = await _productAppService.GetAllProduct(productResultDto, cancellationToken);

            return productResultDto;
        }

        /// <summary>
        /// Checks if any products with the specified names exist in the database.
        /// </summary>
        /// <param name="productNames">A collection of product names to check.</param>
        /// <returns>True if any products with the specified names exist, false otherwise.</returns>
        public async Task<bool> ExistsAsyncProduct(IEnumerable<ProductDto> product, CancellationToken cancellationToken)
        {
            var productId = await _dependencyInjection._appContext.Products.AnyAsync(p => product.Select(x => x.Id).Contains(p.Id),cancellationToken);

            var nameExists = await _dependencyInjection._appContext.Products.AnyAsync(p => product.Select(x => x.Name).Contains(p.Name),cancellationToken);
            
             await _productAppService.ExistsProduct(productId, nameExists, product);

            return productId;
        }
    }
    #endregion
}