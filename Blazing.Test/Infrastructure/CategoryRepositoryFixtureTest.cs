using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var resultNameExiste = await fixture.CategoryInfrastructureRepository.ExistsAsync(originalCategory, cts);

            // Add categories to the repository
            var resultAddAsync = await fixture.CategoryInfrastructureRepository.AddCategories(originalCategory, cts);

            // Update categories in the repository
            var resultUpdateAsync = await fixture.CategoryInfrastructureRepository.UpdateCategory(_categoryIds, updatedCategory, cts);
            // Get category by ID
            var resultGetByIdAsync = await fixture.CategoryInfrastructureRepository.GetCategoryById(_categoryIds, cts);

            // Get all categories
            var resultGetAllAsync = await fixture.CategoryInfrastructureRepository.GetAll(page, pageSize, cts);

            // Delete categories
            var resultDeleteAsync = await fixture.CategoryInfrastructureRepository.DeleteCategory(_categoryIds, cts);


            Assert.NotNull(resultAddAsync);
            Assert.NotNull(resultUpdateAsync);
            Assert.NotNull(resultGetByIdAsync);
            Assert.NotNull(resultGetAllAsync);
            Assert.False(resultNameExiste);
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
                var userAdd = enumerable.FirstOrDefault(u => u.Id == item.Id);
                Assert.Equal(item.Id, userAdd.Id);
                Assert.Equal(item.Name, userAdd.Name);
            }
        }
    }
    #endregion
}
