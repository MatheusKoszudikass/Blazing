using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace Blazing.Test.Application
{
    public class ProductApplicationFixtureTest(ApplicationFixtureTest _fixture) : IClassFixture<ApplicationFixtureTest>
    {
        private readonly IEnumerable<ProductDto> _productsDto = _fixture.PeopleOfData.GetProducts();
        private readonly IEnumerable<Guid> _idsProductsDto = _fixture.PeopleOfData.GetIdsProduct();
        private readonly IEnumerable<Guid> _IdsCategories = _fixture.PeopleOfData.GetCategoryIds();
        private readonly IEnumerable<ProductDto> _updateProductsDto = _fixture.PeopleOfData.UpdateProduct();

        [Fact]
        public async Task ProductApplicationTest()
        {
            var cts = CancellationToken.None;

            var originalProducts = _productsDto.ToList();
            var updatedProducts = _updateProductsDto.ToList();

            var resultBool  = await _fixture.ProductAppService.ExistsProduct(false, false, _productsDto, cts);

            var resultAddAsync = await _fixture.ProductAppService.AddProducts(originalProducts, cts);

            var resultUpdateAsync = await _fixture.ProductAppService.UpdateProduct(_idsProductsDto, originalProducts, updatedProducts, cts);

            var resultGetByCategoriesId = await _fixture.ProductAppService.GetProductsByCategoryId(_IdsCategories, updatedProducts, cts);

            var resultGetByIdAsync = await _fixture.ProductAppService.GetProductById(_idsProductsDto, updatedProducts, cts);

            var resultGetAllAsync = await _fixture.ProductAppService.GetAllProduct(updatedProducts, cts);

            var resultDeleteAsync = await _fixture.ProductAppService.DeleteProducts(_idsProductsDto, updatedProducts, cts);

            Assert.False(resultBool);
            Assert.NotNull(resultAddAsync);
            Assert.NotNull(resultUpdateAsync);
            Assert.NotNull(resultGetByCategoriesId);
            Assert.NotNull(resultGetByIdAsync);
            Assert.NotNull(resultGetAllAsync);
            Assert.NotNull(resultDeleteAsync);

            
            CompareProducts(originalProducts, resultAddAsync);
            CompareProducts(updatedProducts, resultUpdateAsync);
            CompareProducts(updatedProducts, resultGetByCategoriesId);
            CompareProducts(updatedProducts, resultGetByIdAsync);
            CompareProducts(updatedProducts, resultGetAllAsync);
            CompareProducts(updatedProducts, resultDeleteAsync);

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

        private static void CompareRevision(RevisionDto? revisionOriginal, RevisionDto? revisionToUpdate)
        {
            if (revisionOriginal.Id != revisionToUpdate.Id) return;
            Assert.Equal(revisionToUpdate.Comment, revisionOriginal.Comment);
            Assert.Equal(revisionOriginal.Date, revisionToUpdate.Date);
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
