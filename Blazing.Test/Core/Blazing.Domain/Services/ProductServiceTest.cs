using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Services;
using BlazingPizzaTest.Data;
using Microsoft.Extensions.Logging;
using Moq;


namespace Blazing.Test.Core.Blazing.Domain.Services
{
    public class ProductServiceTest
    {
        private readonly ProdutoDomainService _produtoDomainService;
        private readonly Mock<IProductDomainRepository> _productRepositoryMock;
        private readonly PeopleOfData _peopleOfData;

        public ProductServiceTest()
        {
            _productRepositoryMock = new Mock<IProductDomainRepository>();
            _produtoDomainService = new ProdutoDomainService(_productRepositoryMock.Object);
            _peopleOfData = new PeopleOfData();
        }


        [Fact]
        public async Task ProdutoDomainService()
        {
            var productId = _peopleOfData.ProductId;

            var categoryId = _peopleOfData.CategoryId;

            var produtos = _peopleOfData.GetProducts();

            var updateProdutos = _peopleOfData.UpdateProduct();

            var ids = _peopleOfData.GetIdsProduct();


            var resultAddProduct = await _produtoDomainService.AddProducts(produtos);
            _productRepositoryMock.Setup(service => service.AddAsync(produtos));

            var resultUpdateProduct = await _produtoDomainService.UpdateProduct(productId, updateProdutos);

            _productRepositoryMock.Setup(service => service.UpdateAsync(productId, updateProdutos)).ReturnsAsync(updateProdutos);


            Assert.Equal(produtos, resultAddProduct);
            Assert.Equal(productId, resultUpdateProduct?.Id);

        }
    }
}
