using Blazing.Application.Dto;
using Blazing.Test.Api.Controller;
using Microsoft.AspNetCore.Mvc;

namespace BlazingPizzaTest.Controller
{
    #region Teste do controlador de produtos.
    /// <summary>
    /// Test class for the ProductRepository.
    /// </summary>
    /// <remarks>
    /// This class is used to test the ProductRepository methods.
    /// It uses the ProductRepositoryFixture to set up the necessary data and dependencies.
    /// </remarks>
    public class ProdutoControllerTest : IClassFixture<ControllerRepositoryFixtureTest>
    {
        private readonly ControllerRepositoryFixtureTest _fixture;
        private readonly IEnumerable<ProductDto> _products;
        private readonly IEnumerable<Guid> _productIds;
        private readonly IEnumerable<ProductDto> _productsToUpdate;
        private readonly IEnumerable<Guid> _categoryIds;


        public ProdutoControllerTest(ControllerRepositoryFixtureTest fixture)
        {
            _fixture = fixture;
            _products = _fixture.PeopleOfData.GetProducts();
            _productIds = _fixture.PeopleOfData.GetIdsProduct();
            _productsToUpdate = _fixture.PeopleOfData.UpdateProduct();
            _categoryIds = _fixture.PeopleOfData.GetCategoryIds();
        }

   
        public async Task ControllerProductAllTest ()
        {

            //Arrange
            var cts = new CancellationTokenSource().Token;
            //Act
            var resultProductAdd = await _fixture.ProductController.AddProducts(_products, cts);

            var resultPtroductToUpdate = await _fixture.ProductController.UpdateProduto(_productsToUpdate, cts);


            var okResult = Assert.IsType<OkObjectResult>(resultProductAdd?.Result);

            var actualValue = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);

            Assert.Equal(_products, actualValue);
        }
    }
    #endregion
}
