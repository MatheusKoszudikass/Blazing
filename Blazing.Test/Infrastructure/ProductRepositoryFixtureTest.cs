using BenchmarkDotNet.Attributes;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using Iced.Intel;


namespace Blazing.Test.Infrastructure
{
    /// <summary>
    /// Test class for the ProductRepository.
    /// </summary>
    /// <remarks>
    /// This class is used to test the ProductRepository methods.
    /// It uses the ProductRepositoryFixture to set up the necessary data and dependencies.
    /// </remarks>
    public class ProductRepositoryFixtureTest(RepositoryFixtureTest fixture) : IClassFixture<RepositoryFixtureTest>
    {
        //Products
        private readonly IEnumerable<ProductDto> _products = fixture.PeopleOfData.GetProducts();
        private readonly IEnumerable<Guid> _productIds = fixture.PeopleOfData.GetIdsProduct();
        private readonly IEnumerable<ProductDto> _productsToUpdate = fixture.PeopleOfData.UpdateProduct();
        private readonly IEnumerable<Guid> _categoryIds = fixture.PeopleOfData.GetCategoryIds();


        /// <summary>
        /// Test method for the ProductRepository methods.
        /// </summary>
        /// <remarks>
        /// This method tests the AddProducts, UpdateProduct, GetProductsByCategoryId, GetProductById, GetAll and DeleteProducts methods of the ProductRepository.
        /// </remarks>
        [Fact]
        public async Task ProductsAllTest()
        {
            const int page = 1;
            const int pageSize = 2;
            var cts = CancellationToken.None;

            var originalProducts = _products.ToList();
            var updatedProducts = _productsToUpdate.ToList();

            // Check if product exists
            var resultNameExists = await fixture.ProductInfrastructureRepository.ExistsAsyncProduct(originalProducts, cts);

            // Add products to the repository
            var resultAddAsync = await fixture.ProductInfrastructureRepository.AddProducts(originalProducts, cts);

            // Update products in the repository
            var resultUpdateAsync = await fixture.ProductInfrastructureRepository.UpdateProduct(_productIds, updatedProducts, cts);

            // Get products by category ID
            var resultGetByCategoryIdAsync = await fixture.ProductInfrastructureRepository.GetProductsByCategoryId(_categoryIds, cts);

            // Get product by ID
            var resultGetByIdAsync = await fixture.ProductInfrastructureRepository.GetProductById(_productIds, cts);

            // Get all products
            var resultGetAllAsync = await fixture.ProductInfrastructureRepository.GetAll(page, pageSize,cts);

            // Delete products by ID
            var resultDeleteProductById = await fixture.ProductInfrastructureRepository.DeleteProducts(_productIds, cts);

            // Assert that the results are not null

            Assert.NotNull(resultAddAsync); // Add your specific assertions for adding a product.
            Assert.NotNull(resultUpdateAsync); // Add your specific assertions for updating a product.
            Assert.NotNull(resultGetByCategoryIdAsync); // Add your specific assertions for getting products by category ID.
            Assert.NotNull(resultGetByIdAsync); // Add your specific assertions for getting a product by ID.
            Assert.NotNull(resultGetAllAsync); // Add your specific assertions for getting all products.
            Assert.False(resultNameExists); // Add your specific assertions for checking if a product exists.
            Assert.NotNull(resultDeleteProductById); // Add your specific assertions for deleting products by ID.

            Assert.IsType<List<ProductDto>>(resultAddAsync);
            Assert.IsType<List<ProductDto>>(resultUpdateAsync);
            Assert.IsType<List<ProductDto>>(resultGetByCategoryIdAsync);
            Assert.IsType<List<ProductDto>>(resultGetByIdAsync);
            Assert.IsType<List<ProductDto>>(resultGetAllAsync);
            Assert.IsType<List<ProductDto>>(resultDeleteProductById);

            CompareProducts(originalProducts, resultAddAsync);
            CompareProducts(updatedProducts, resultUpdateAsync);
            CompareProducts(updatedProducts, resultGetByCategoryIdAsync);
            CompareProducts(updatedProducts, resultGetByIdAsync);
            CompareProducts(updatedProducts, resultGetAllAsync);
            CompareProducts(updatedProducts, resultDeleteProductById);
        }

        /// <summary>
        /// Compares the original products with the products returned from the domain service.
        /// </summary>
        /// <param name="productsOriginal">The original products.</param>
        /// <param name="productToUpdate">The products returned from the domain service.</param>
        private static void CompareProducts(IEnumerable<ProductDto> productsOriginal, IEnumerable<ProductDto?> productToUpdate)
        {
            var enumerable = productsOriginal.ToList();
            foreach (var item in productToUpdate)
            {
                var userAdd = enumerable.FirstOrDefault(u => u.Id == item.Id);
                Assert.Equal(item.Id, userAdd.Id);
                Assert.Equal(item.Name, userAdd.Name);
                Assert.Equal(item.Description, userAdd.Description);
                Assert.Equal(item.Price, userAdd.Price);
                Assert.Equal(item.CategoryId, userAdd.CategoryId);
                Assert.Equal(item.AssessmentId, userAdd.AssessmentId);
                CompareAssessments(item.Assessment, userAdd.Assessment);
                CompareRevision(item.Assessment.RevisionDetail.First(), userAdd.Assessment.RevisionDetail.First());
                Assert.Equal(item.AttributesId, userAdd.AttributesId);
                CompareAttributes(item.Attributes, userAdd.Attributes);
                Assert.Equal(item.AvailabilityId, userAdd.AvailabilityId);
                CompareAvailability(item.Availability, userAdd.Availability);
                Assert.Equal(item.DimensionsId, userAdd.DimensionsId);
                CompareImage(item.Image, userAdd.Image);

            }
        }

        /// <summary>
        /// Compares two RevisionDto objects and asserts that their properties are equal.
        /// </summary>
        /// <param name="revisionOriginal">The original RevisionDto object.</param>
        /// <param name="revisionToUpdate">The updated RevisionDto object.</param>
        private static void CompareRevision(RevisionDto? revisionOriginal, RevisionDto? revisionToUpdate)
        {
            if (revisionOriginal.Id != revisionToUpdate.Id) return;
            Assert.Equal(revisionToUpdate.Comment, revisionOriginal.Comment);
            Assert.Equal(revisionOriginal.Date, revisionToUpdate.Date);
            Assert.Equal(Assert.IsType<DateTime>(revisionOriginal.DataCreated), Assert.IsType<DateTime>(revisionToUpdate.DataCreated));
        }

        /// <summary>
        /// Compares the original assessment with the assessment returned from the domain service.
        /// </summary>
        /// <param name="assessmentsOriginal">The original assessment.</param>
        /// <param name="assessmentToUpdate">The assessment returned from the domain service.</param>
        private static void CompareAssessments(AssessmentDto? assessmentsOriginal, AssessmentDto? assessmentToUpdate)
        {
            if (assessmentsOriginal.Id != assessmentToUpdate.Id) return;
            Assert.Equal(assessmentsOriginal.Average, assessmentToUpdate.Average);
            Assert.Equal(assessmentsOriginal.NumberOfReviews, assessmentToUpdate.NumberOfReviews);
        }

        /// <summary>
        /// Compares the original attributes with the attributes returned from the domain service.
        /// </summary>
        /// <param name="attributeOriginal">The original attributes.</param>
        /// <param name="attributeToUpdate">The attributes returned from the domain service.</param>
        private static void CompareAttributes(AttributeDto? attributeOriginal, AttributeDto? attributeToUpdate)
        {
            if (attributeOriginal.Id != attributeToUpdate.Id) return;
            Assert.Equal(attributeOriginal.Color, attributeToUpdate.Color);
            Assert.Equal(attributeOriginal.Material, attributeToUpdate.Material);
            Assert.Equal(attributeOriginal.Model, attributeToUpdate.Model);
        }

        /// <summary>
        /// Compares the original availability with the availability returned from the domain service.
        /// </summary>
        /// <param name="availabilityOriginal">The original availability.</param>
        /// <param name="availabilityToUpdate">The availability returned from the domain service.</param>
        private static void CompareAvailability(AvailabilityDto? availabilityOriginal,
            AvailabilityDto? availabilityToUpdate)
        {
            if (availabilityOriginal.Id != availabilityToUpdate.Id) return;
            Assert.Equal(availabilityOriginal.IsAvailable, availabilityToUpdate.IsAvailable);
            Assert.Equal(availabilityOriginal.EstimatedDeliveryDate, availabilityToUpdate.EstimatedDeliveryDate);
        }

        /// <summary>
        /// Compares the original image with the image returned from the domain service.
        /// </summary>
        /// <param name="imageOriginal">The original image.</param>
        /// <param name="imageToUpdate">The image returned from the domain service.</param>
        private static void CompareImage(ImageDto? imageOriginal, ImageDto? imageToUpdate)
        {
            if (imageOriginal.Id != imageToUpdate.Id) return;
            Assert.Equal(imageOriginal.Url, imageToUpdate.Url);
            Assert.Equal(imageOriginal.AltText, imageToUpdate.AltText);
        }
    }
}

