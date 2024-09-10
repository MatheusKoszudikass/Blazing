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
    public class CategoryRepositoryFixtureTest : IClassFixture<RepositoryFixtureTest>
    {
        private readonly RepositoryFixtureTest _fixture;

        private readonly IEnumerable<CategoryDto> _AddCategories;
        private readonly IEnumerable<CategoryDto> _categories;
        private readonly IEnumerable<Guid> _categoryIds;

        public CategoryRepositoryFixtureTest(RepositoryFixtureTest fixture)
        {
            _fixture = fixture;
            _AddCategories = _fixture.PeopleOfData.AddCategory();
            _categories = _fixture.PeopleOfData.GetCategoryItems();
            _categoryIds = _fixture.PeopleOfData.GetCategoryIds();
        }

        /// <summary>
        /// Test method for the CategoryRepository methods.
        /// </summary>
        /// <remarks>
        /// This method tests the AddCategories, UpdateCategory, GetCategoryById, GetAll, ExistsAsync and DeleteCategory methods of the CategoryRepository.
        /// </remarks>

        [Fact]
        public async Task CategoriesAllTest()
        {
            var page = 1;
            var pageSize = 3;
            var cts = CancellationToken.None;

            // Check if category exists
            var resultNameExiste = await _fixture.CategoryInfrastructureRepository.ExistsAsync(_categories, cts);

            // Add categories to the repository
            var resultAddAsync = await _fixture.CategoryInfrastructureRepository.AddCategories(_AddCategories, cts);

            // Update categories in the repository
            var resultUpdateAsync = await _fixture.CategoryInfrastructureRepository.UpdateCategory(_categoryIds, _categories, cts);
            // Get category by ID
            var resultGetByIdAsync = await _fixture.CategoryInfrastructureRepository.GetCategoryById(_categoryIds, cts);

            // Get all categories
            var resultGetAllAsync = await _fixture.CategoryInfrastructureRepository.GetAll(page, pageSize, cts);

            // Delete categories
            var resultDeleteAsync = await _fixture.CategoryInfrastructureRepository.DeleteCategory(_categoryIds, cts);



            Assert.NotNull(resultAddAsync);
            Assert.NotNull(resultUpdateAsync);
            Assert.NotNull(resultGetByIdAsync);
            Assert.NotNull(resultGetAllAsync);
            Assert.False(resultNameExiste);
            Assert.NotNull(resultDeleteAsync);

        }
    }
    #endregion
}
