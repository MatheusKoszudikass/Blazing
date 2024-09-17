using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Category;
using System.Drawing.Printing;

namespace Blazing.Test.Infrastructure
{
    #region Test Category Repository
    /// <summary>
    /// Test class for the CategoryRepository.
    /// </summary>
    /// <remarks>
    /// This class is used to test the CategoryRepository methods.
    /// It uses the RepositoryFixtureTest to set up the necessary data and dependencies.
    /// </remarks>
    public class CategoryRepositoryFixtureTest(RepositoryFixtureTest fixture) : IClassFixture<RepositoryFixtureTest>
    {
        private readonly IEnumerable<Guid> _idProducts = fixture.PeopleOfData.GetIdsProduct();
        private readonly IEnumerable<Guid> _idEmpty = new List<Guid>();
        private readonly IEnumerable<CategoryDto> _addCategories = fixture.PeopleOfData.GetCategoryItems();
        private readonly IEnumerable<Guid> _categoryIds = fixture.PeopleOfData.GetCategoryIds();
        private readonly IEnumerable<CategoryDto> _updateCategory = fixture.PeopleOfData.UpdateCategory();

        /// <summary>
        /// Test method for the CategoryRepository methods.
        /// </summary>
        /// <remarks>
        /// This method tests the AddCategories, UpdateCategory, GetCategoryById, GetAll, ExistsAsync and DeleteCategory methods of the CategoryRepository.
        /// </remarks>

        [Fact]
        public async Task CategoriesAllTest()
        {
            const int page = 1;
            const int pageSize = 3;
            var cts = CancellationToken.None;
            var originalCategory = _addCategories.ToList();
            var updatedCategory = _updateCategory.ToList();

            // Check if category exists
            var resultNameExiste = await fixture.CategoryInfrastructureRepository.ExistsAsyncCategory(originalCategory, cts);

            // Add categories to the repository
            var resultAddAsync = await fixture.CategoryInfrastructureRepository.AddCategories(originalCategory, cts);
            await AddCategoriesException(cts);


            // Update categories in the repository
            var resultUpdateAsync = await fixture.CategoryInfrastructureRepository.UpdateCategory(_categoryIds, updatedCategory, cts);
            await UpdateCategoriesException(cts);

            // Get category by ID
            var resultGetByIdAsync = await fixture.CategoryInfrastructureRepository.GetCategoryById(_categoryIds, cts);
            await GetCategoryByIdException(cts);

            // Get all categories
            var resultGetAllAsync = await fixture.CategoryInfrastructureRepository.GetAll(page, pageSize, cts);
            await GetAllException(cts);

            // Delete categories
            var resultDeleteAsync = await fixture.CategoryInfrastructureRepository.DeleteCategory(_categoryIds, cts);
            await DeleteCategoriesException(cts);



            Assert.False(resultNameExiste);
            Assert.NotNull(resultAddAsync);
            Assert.NotNull(resultUpdateAsync);
            Assert.NotNull(resultGetByIdAsync);
            Assert.NotNull(resultGetAllAsync);
            Assert.NotNull(resultDeleteAsync);
            Assert.IsType<List<CategoryDto>>(resultAddAsync);
            Assert.IsType<List<CategoryDto>>(resultUpdateAsync);
            Assert.IsType<List<CategoryDto>>(resultGetByIdAsync);
            Assert.IsType<List<CategoryDto>>(resultGetAllAsync);
            Assert.IsType<List<CategoryDto>>(resultDeleteAsync);


            CompareCategories(originalCategory, resultAddAsync);
            CompareCategories(updatedCategory, resultUpdateAsync);
            CompareCategories(updatedCategory, resultGetByIdAsync);
            CompareCategories(updatedCategory, resultDeleteAsync);
        }

        /// <summary>
        /// Compares two collections of categories.
        /// </summary>
        /// <param name="categoriesOriginal">The original categories.</param>
        /// <param name="categoriesToUpdate">The categories to compare to the originals.</param>
        private static void CompareCategories(IEnumerable<CategoryDto> categoriesOriginal, IEnumerable<CategoryDto?> categoriesToUpdate)
        {
            var enumerable = categoriesOriginal.ToList();
            foreach (var item in categoriesToUpdate)
            {
                var userAdd = enumerable.Find(u => u.Id == item.Id);
                Assert.Equal(item.Id, userAdd.Id);
                Assert.Equal(item.Name, userAdd.Name);
            }
        }

        /// <summary>
        /// Tests the scenario where adding categories to the repository throws an exception.
        /// Asserts that adding categories with invalid identity raises a DomainException.IdentityInvalidException.
        /// Also verifies that checking for category existence with invalid identity raises the same exception.
        /// </summary>
        /// <param name="cts">The cancellation token to observe while waiting for the task to complete.</param>
        private async Task AddCategoriesException(CancellationToken cts)
        {
            await Assert.ThrowsAsync<DomainException.IdentityInvalidException>(async () => await fixture.CategoryInfrastructureRepository.AddCategories(_addCategories, cts));
            await Assert.ThrowsAsync<DomainException.IdentityInvalidException>(async () => await fixture.CategoryInfrastructureRepository.ExistsAsyncCategory(_addCategories, cts));
        }

        /// <summary>
        /// Tests the scenario where updating categories in the repository throws an exception.
        /// Asserts that updating categories with empty IDs raises a DomainException.IdentityInvalidException.
        /// Also verifies that updating categories with incorrect product IDs raises a CategoryExceptions.CategoryNotFoundException.
        /// </summary>
        /// <param name="cts">The cancellation token to observe while waiting for the task to complete.</param>
        private async Task UpdateCategoriesException(CancellationToken cts)
        {
            await Assert.ThrowsAsync<DomainException.IdentityInvalidException>(async () => await fixture.CategoryInfrastructureRepository.UpdateCategory(_idEmpty, _updateCategory, cts));
            await Assert.ThrowsAsync<CategoryExceptions.CategoryNotFoundException>(async () => await fixture.CategoryInfrastructureRepository.UpdateCategory(_idProducts, _updateCategory, cts));
        }

        /// <summary>
        /// Tests the scenario where retrieving a category by its ID throws an exception.
        /// Asserts that retrieving a category with empty IDs raises a DomainException.IdentityInvalidException.
        /// Also verifies that retrieving a category with incorrect product IDs raises a CategoryExceptions.CategoryNotFoundException.
        /// </summary>
        /// <param name="cts">The cancellation token to observe while waiting for the task to complete.</param>
        private async Task GetCategoryByIdException(CancellationToken cts)
        {
            await Assert.ThrowsAsync<DomainException.IdentityInvalidException>(async () => await fixture.CategoryInfrastructureRepository.GetCategoryById(_idEmpty, cts));
            await Assert.ThrowsAsync<CategoryExceptions.CategoryNotFoundException>(async () => await fixture.CategoryInfrastructureRepository.GetCategoryById(_idProducts, cts));
        }

        /// <summary>
        /// Tests the scenario where retrieving all categories or deleting a category throws an exception.
        /// Asserts that retrieving all categories with a page and page size that exceed available categories raises a CategoryExceptions.CategoryNotFoundException.
        /// Also verifies that deleting a category with incorrect product IDs raises a CategoryExceptions.CategoryNotFoundException.
        /// </summary>
        /// <param name="cts">The cancellation token to observe while waiting for the task to complete.</param>
        private async Task GetAllException(CancellationToken cts)
        {
            await Assert.ThrowsAsync<CategoryExceptions.CategoryNotFoundException>(async () => await fixture.CategoryInfrastructureRepository.GetAll(page: 2, pageSize: 50, cts));
            await Assert.ThrowsAsync<CategoryExceptions.CategoryNotFoundException>(async () => await fixture.CategoryInfrastructureRepository.DeleteCategory(_idProducts, cts));
        }

        /// <summary>
        /// Tests the scenario where deleting a category by ID throws an exception.
        /// Asserts that retrieving a category by ID with invalid category IDs raises a CategoryExceptions.CategoryNotFoundException.
        /// Also verifies that retrieving all categories with invalid category IDs and deleting categories with invalid IDs raises the same exception.
        /// </summary>
        /// <param name="cts">The cancellation token to observe while waiting for the task to complete.</param>
        private async Task DeleteCategoriesException(CancellationToken cts)
        {
            await Assert.ThrowsAsync<CategoryExceptions.CategoryNotFoundException>(async () => await fixture.CategoryInfrastructureRepository.GetCategoryById(_categoryIds, cts));
            await Assert.ThrowsAsync<CategoryExceptions.CategoryNotFoundException>(async () => await fixture.CategoryInfrastructureRepository.GetAll(page: 1, pageSize: 2, cts));
            await Assert.ThrowsAsync<CategoryExceptions.CategoryNotFoundException>(async () => await fixture.CategoryInfrastructureRepository.DeleteCategory(_categoryIds, cts));
        }
    }
    #endregion
}
