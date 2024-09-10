using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace Blazing.Test.Domain
{
    /// <summary>
    /// This class represents a test fixture for the Product domain.
    /// </summary>
    public class ProductDomainFixtureTest : IClassFixture<DomainFixtureTest>
    {
        private readonly DomainFixtureTest _domainFixtureTest;
        private readonly IEnumerable<ProductDto> _productsDto;
        private readonly IEnumerable<Guid> _idProductsDto;
        private readonly IEnumerable<ProductDto> _productToUpdateDto;
        private readonly IEnumerable<Guid> _categoryIdDto;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDomainFixtureTest"/> class.
        /// </summary>
        /// <param name="domainFixtureTest">The domain fixture test.</param>
        public ProductDomainFixtureTest(DomainFixtureTest domainFixtureTest)
        {
            _domainFixtureTest = domainFixtureTest;
            _productsDto = _domainFixtureTest.PeopleOfData.GetProducts();
            _idProductsDto = _domainFixtureTest.PeopleOfData.GetIdsProduct();
            _productToUpdateDto = _domainFixtureTest.PeopleOfData.UpdateProduct();
            _categoryIdDto = _domainFixtureTest.PeopleOfData.GetCategoryIds();
        }

        /// <summary>
        /// Tests the Product domain by performing various operations.
        /// </summary>
        /// <returns>A task representing the asynchronous execution of the test.</returns>
        [Fact]
        public async Task ProductDomainTestAll()
        {
            var cts = CancellationToken.None;
            var products = _domainFixtureTest.Mapper.Map<IEnumerable<Product>>(_productsDto);
            var originalProducts = products.ToList();
            var productToUpdate = _domainFixtureTest.Mapper.Map<IEnumerable<Product>>(_productToUpdateDto);
            var productToUpdated = productToUpdate.ToList();

            // Check if product exists

            var resultBool = await _domainFixtureTest.ProductDomainService.ExistsAsync(false, false, originalProducts, cts);

            // Add products to the domain
            var resultAddAsync = await _domainFixtureTest.ProductDomainService.Add(originalProducts, cts);

            // Update products to the domain

            var resultUpdateAsync = await _domainFixtureTest.ProductDomainService.Update(_idProductsDto, originalProducts, productToUpdated, cts);

            // Get products to the domain
            var resultGetByIdAsync = await _domainFixtureTest.ProductDomainService.GetById(_idProductsDto, productToUpdated, cts);

            // Get products by category ID
            var resultGetByCategoryIdAsync = await _domainFixtureTest.ProductDomainService.GetById(_categoryIdDto, productToUpdated, cts);

            // Get all products
            var resultGetAllAsync = await _domainFixtureTest.ProductDomainService.GetAll(productToUpdated, cts);

            // Delete products to the domain
            var resultDeleteAsync = await _domainFixtureTest.ProductDomainService.Delete(_idProductsDto, productToUpdated, cts);

            Assert.NotNull(resultAddAsync); // Add your specific assertions for adding a product.
            Assert.NotNull(resultUpdateAsync); // Add your specific assertions for updating a product.
            Assert.NotNull(resultGetByCategoryIdAsync); // Add your specific assertions for getting products by category ID.
            Assert.NotNull(resultGetByIdAsync); // Add your specific assertions for getting a product by ID.
            Assert.NotNull(resultGetAllAsync); // Add your specific assertions for getting all products.
            Assert.False(resultBool); // Add your specific assertions for checking if a product exists.
            Assert.NotNull(resultDeleteAsync); // Add your specific assertions for deleting products by ID.

            CompareProducts(originalProducts, resultAddAsync);
            CompareProducts(productToUpdated, resultUpdateAsync);
            CompareProducts(productToUpdated, resultGetByCategoryIdAsync);
            CompareProducts(productToUpdated, resultGetByIdAsync);
            CompareProducts(productToUpdated, resultGetAllAsync);
            CompareProducts(productToUpdated, resultDeleteAsync);

        }

        /// <summary>
        /// Compares the original products with the products returned from the domain service.
        /// </summary>
        /// <param name="productsOriginal">The original products.</param>
        /// <param name="productToUpdate">The products returned from the domain service.</param>
        private static void CompareProducts(IEnumerable<Product> productsOriginal, IEnumerable<Product?> productToUpdate)
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
        private static void CompareAssessments(Assessment? assessmentsOriginal, Assessment? assessmentToUpdate)
        {
            if (assessmentsOriginal.Id != assessmentToUpdate.Id) return;
            Assert.Equal(assessmentsOriginal.Average, assessmentToUpdate.Average);
            Assert.Equal(assessmentsOriginal.NumberOfReviews, assessmentToUpdate.NumberOfReviews);
            Assert.Equal(assessmentsOriginal.RevisionDetail, assessmentToUpdate.RevisionDetail);
            Assert.Equal(assessmentsOriginal.DataCreated, assessmentToUpdate.DataCreated);
            Assert.Equal(assessmentsOriginal.DataUpdated, assessmentToUpdate.DataUpdated);
            Assert.Equal(assessmentsOriginal.DataDeleted, assessmentToUpdate.DataDeleted);
        }

        /// <summary>
        /// Compares the original attributes with the attributes returned from the domain service.
        /// </summary>
        /// <param name="attributeOriginal">The original attributes.</param>
        /// <param name="attributeToUpdate">The attributes returned from the domain service.</param>
        private static void CompareAttributes(Attributes? attributeOriginal, Attributes? attributeToUpdate)
        {
            if (attributeOriginal.Id != attributeToUpdate.Id) return;
            Assert.Equal(attributeOriginal.Color, attributeToUpdate.Color);
            Assert.Equal(attributeOriginal.Material, attributeToUpdate.Material);
            Assert.Equal(attributeOriginal.Model, attributeToUpdate.Model);
            Assert.Equal(attributeOriginal.DataCreated, attributeToUpdate.DataCreated);
            Assert.Equal(attributeOriginal.DataUpdated, attributeToUpdate.DataUpdated);
            Assert.Equal(attributeOriginal.DataDeleted, attributeToUpdate.DataDeleted);
        }

        /// <summary>
        /// Compares the original availability with the availability returned from the domain service.
        /// </summary>
        /// <param name="availabilityOriginal">The original availability.</param>
        /// <param name="availabilityToUpdate">The availability returned from the domain service.</param>
        private static void CompareAvailability(Availability? availabilityOriginal,
            Availability? availabilityToUpdate)
        {
            if (availabilityOriginal.Id != availabilityToUpdate.Id) return;
            Assert.Equal(availabilityOriginal.IsAvailable, availabilityToUpdate.IsAvailable);
            Assert.Equal(availabilityOriginal.EstimatedDeliveryDate, availabilityToUpdate.EstimatedDeliveryDate);
            Assert.Equal(availabilityOriginal.DataCreated, availabilityToUpdate.DataCreated);
            Assert.Equal(availabilityOriginal.DataUpdated, availabilityToUpdate.DataUpdated);
            Assert.Equal(availabilityOriginal.DataDeleted, availabilityToUpdate.DataDeleted);
        }

        /// <summary>
        /// Compares the original image with the image returned from the domain service.
        /// </summary>
        /// <param name="imageOriginal">The original image.</param>
        /// <param name="imageToUpdate">The image returned from the domain service.</param>
        private static void CompareImage(Image? imageOriginal, Image? imageToUpdate)
        {
            if (imageOriginal.Id != imageToUpdate.Id) return;
            Assert.Equal(imageOriginal.Url, imageToUpdate.Url);
            Assert.Equal(imageOriginal.AltText, imageToUpdate.AltText);
            Assert.Equal(imageOriginal.DataCreated, imageToUpdate.DataCreated);
            Assert.Equal(imageOriginal.DataUpdated, imageToUpdate.DataUpdated);
            Assert.Equal(imageOriginal.DataDeleted, imageToUpdate.DataDeleted);
        }
    }
}
