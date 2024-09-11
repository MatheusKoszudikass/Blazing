using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace Blazing.Test.Application
{
    /// <summary>
    /// This class is a test fixture for testing the CategoryApplication.
    /// It uses the ApplicationFixtureTest to set up the necessary data and dependencies.
    /// </summary>
    public class CategoryApplicationFixtureTest(ApplicationFixtureTest fixture) : IClassFixture<ApplicationFixtureTest>
    {
        private readonly IEnumerable<CategoryDto> _categoriesDto = fixture.PeopleOfData.GetCategoryItems();
        private readonly IEnumerable<Guid> _idCategories = fixture.PeopleOfData.GetCategoryIds();
        private readonly IEnumerable<CategoryDto> _categoriesDtoUpdate = fixture.PeopleOfData.UpdateCategory();

        /// <summary>
        /// Tests all methods of the CategoryApplication.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task CategoryApplicationTest()
        {
            var cts = CancellationToken.None;

            var originalCategories = _categoriesDto.ToList();
            var updatedCategories = _categoriesDtoUpdate.ToList();

            var resultBool = await fixture.CategoryAppService.ExistsCategories(false, false, _categoriesDto, cts);

            var resultAddAsync = await fixture.CategoryAppService.AddCategory(_categoriesDto, cts);

            var resultUpdateAsync =
                await fixture.CategoryAppService.UpdateCategory(_idCategories, originalCategories, updatedCategories,
                    cts);

            var resultGetByIdAsync = await fixture.CategoryAppService.GetById(_idCategories, updatedCategories, cts);

            var resultGetAllAsync = await fixture.CategoryAppService.GetAll(updatedCategories, cts);

            var resultDeleteAsync =
                await fixture.CategoryAppService.DeleteCategory(_idCategories, updatedCategories, cts);

            Assert.False(resultBool);
            Assert.NotNull(resultAddAsync);
            Assert.NotNull(resultUpdateAsync);
            Assert.NotNull(resultGetByIdAsync);
            Assert.NotNull(resultGetAllAsync);
            Assert.NotNull(resultDeleteAsync);

            CompareCategories(originalCategories, resultAddAsync);
            CompareCategories(updatedCategories, resultUpdateAsync);
            CompareCategories(updatedCategories, resultGetByIdAsync);
            CompareCategories(updatedCategories, resultGetAllAsync);
            CompareCategories(updatedCategories, resultDeleteAsync);

        }

        /// <summary>
        /// Compares two collections of categories.
        /// </summary>
        /// <param name="categoriesOriginal">The original categories.</param>
        /// <param name="categoriesToUpdate">The categories to compare to the originals.</param>
        private static void CompareCategories(IEnumerable<CategoryDto> categoriesOriginal,
            IEnumerable<CategoryDto?> categoriesToUpdate)
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
}
