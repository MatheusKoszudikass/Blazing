using Blazing.Application.Dto;


namespace Blazing.Test.Infrastructure
{
    /// <summary>
    /// Test class for the ProductRepository.
    /// </summary>
    /// <remarks>
    /// This class is used to test the ProductRepository methods.
    /// It uses the ProductRepositoryFixture to set up the necessary data and dependencies.
    /// </remarks>
    public class ProductRepositoryFixtureTest : IClassFixture<RepositoryFixtureTest>
    {
        //Products
        private readonly RepositoryFixtureTest _fixture;
        private readonly IEnumerable<ProductDto> _products;
        private readonly IEnumerable<Guid> _productIds;
        private readonly IEnumerable<ProductDto> _productsToUpdate;
        private readonly IEnumerable<Guid> _categoryIds;



        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepositoryFixtureTest"/> class.
        /// </summary>
        /// <param name="fixture">The fixture used to set up the necessary data and dependencies.</param>
        public ProductRepositoryFixtureTest(RepositoryFixtureTest fixture)
        {
            _fixture = fixture;
            _products = _fixture.PeopleOfData.GetProducts();
            _productIds = _fixture.PeopleOfData.GetIdsProduct();
            _productsToUpdate = _fixture.PeopleOfData.UpdateProduct();
            _categoryIds = _fixture.PeopleOfData.GetCategoryIds();
        }

        /// <summary>
        /// Test method for the ProductRepository methods.
        /// </summary>
        /// <remarks>
        /// This method tests the AddProducts, UpdateProduct, GetProductsByCategoryId, GetProductById, GetAll and DeleteProducts methods of the ProductRepository.
        /// </remarks>
        [Fact]
        public async Task ProductsAllTest()
        {
            var cts = CancellationToken.None;

            // Add products to the repository
            var resultAddAsync = await _fixture.ProductInfrastructureRepository.AddProducts(_products, cts);

            // Update products in the repository
            var resultUpdateAsync = await _fixture.ProductInfrastructureRepository.UpdateProduct(_productIds, _productsToUpdate, cts);

            // Get products by category ID
            var resultGetByCategoryIdAsync = await _fixture.ProductInfrastructureRepository.GetProductsByCategoryId(_categoryIds, cts);

            // Get product by ID
            var resultGetByIdAsync = await _fixture.ProductInfrastructureRepository.GetProductById(_productIds, cts);

            // Get all products
            var resultGetAllAsync = await _fixture.ProductInfrastructureRepository.GetAll(cts);

            // Check if product exists
            var resultNameExiste = await _fixture.ProductInfrastructureRepository.ExistsAsyncProduct(_productsToUpdate, cts);

            // Delete products by ID
            var resultDeleteProductById = await _fixture.ProductInfrastructureRepository.DeleteProducts(_productIds, cts);

            // Assert that the results are not null
            Assert.NotNull(resultAddAsync); // Add your specific assertions for adding a product.
            Assert.NotNull(resultUpdateAsync); // Add your specific assertions for updating a product.
            Assert.NotNull(resultGetByCategoryIdAsync); // Add your specific assertions for getting products by category ID.
            Assert.NotNull(resultGetByIdAsync); // Add your specific assertions for getting a product by ID.
            Assert.NotNull(resultGetAllAsync); // Add your specific assertions for getting all products.
            Assert.False(resultNameExiste); // Add your specific assertions for checking if a product exists.
            Assert.NotNull(resultDeleteProductById); // Add your specific assertions for deleting products by ID.
        }
    }
}

