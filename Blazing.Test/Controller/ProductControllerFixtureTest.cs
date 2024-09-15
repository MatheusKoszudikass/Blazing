using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blazing.Test.Controller
{
    /// <summary>
    /// Test class for the ProductController.
    /// Initializes a new instance of the <see cref="ProductControllerFixtureTest"/> class.
    /// </summary>
    /// <remarks>
    /// This class is used to test the ProductController methods.
    /// It uses the ControllerFixtureTest fixture to set up the necessary data and dependencies.
    /// </remarks>
    public class ProductControllerFixtureTest(ControllerFixtureTest fixture) : IClassFixture<ControllerFixtureTest>
    {
        private readonly IEnumerable<ProductDto> _products = fixture.PeopleOfData.GetProducts();
        private readonly IEnumerable<Guid> _idProducts = fixture.PeopleOfData.GetIdsProduct();
        private readonly IEnumerable<Guid> _idProductsCategory = fixture.PeopleOfData.GetCategoryIds();
        private readonly IEnumerable<ProductDto> _productsUpdated = fixture.PeopleOfData.UpdateProduct();

        /// <summary>
        /// Test method for the ProductController methods.
        /// </summary>
        [Fact]
        public async Task ProductControllerTest()
        {
            fixture.MemoryCache.Remove($"products_all");

            var cts = CancellationToken.None;
            var originalProducts = _products.ToList();
            var updatedProducts = _productsUpdated.ToList();

              fixture.ProductInfrastructureRepository.Setup(repo =>  repo.AddProducts(originalProducts, cts)).ReturnsAsync(originalProducts);
              fixture.ProductInfrastructureRepository.Setup(repo => repo.UpdateProduct(_idProducts,updatedProducts, cts)).ReturnsAsync(updatedProducts);
              fixture.ProductInfrastructureRepository.Setup(repo => repo.GetProductsByCategoryId(_idProductsCategory, cts)).ReturnsAsync(updatedProducts);
              fixture.ProductInfrastructureRepository.Setup(repo => repo.DeleteProducts(_idProducts, cts)).ReturnsAsync(updatedProducts);
              fixture.ProductInfrastructureRepository.Setup(repo => repo.GetProductById(_idProducts, cts)).ReturnsAsync(updatedProducts);
              fixture.ProductInfrastructureRepository.Setup(repo => repo.GetAll(1, 50, cts)).ReturnsAsync(updatedProducts);

            var resultAddProducts = await fixture.ProductController.AddProducts(originalProducts, cts);
            var okResult = Assert.IsType<OkObjectResult>(resultAddProducts.Result);
            var returnProducts = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);

            var resultUpdateProduct = await fixture.ProductController.UpdateProduto(_idProducts, updatedProducts, cts);
            okResult = Assert.IsType<OkObjectResult>(resultUpdateProduct.Result);
            returnProducts = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);

            var resultGetByCategory = await fixture.ProductController.GetProductsByCategoryId(_idProductsCategory, cts);
            okResult = Assert.IsType<OkObjectResult>(resultGetByCategory.Result);
            returnProducts = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Single(returnProducts);

            var resultGetById = await fixture.ProductController.GetProductById(_idProducts, cts);
            okResult = Assert.IsType<OkObjectResult>(resultGetById.Result);
            returnProducts = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);

            var resultGetAll = await fixture.ProductController.GetAll(1, 2, cts);
            okResult = Assert.IsType<OkObjectResult>(resultGetAll.Result);
            returnProducts = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);

            var resultDeleted = await fixture.ProductController.DeleteProducts(_idProducts, cts);
            okResult = Assert.IsType<OkObjectResult>(resultDeleted.Result);
            returnProducts = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);

            fixture.MemoryCache.Remove($"products_all");
        }
    }
}
