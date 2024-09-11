using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace Blazing.Test.Domain
{
    /// <summary>
    /// Test class for the CategoryDomainService.
    /// Initializes a new instance of the <see cref="CategoryDomainFixtureTest"/> class.
    /// <param name="fixture">The fixture to use for the test.</param>
    /// </summary>
    public class CategoryDomainFixtureTest(DomainFixtureTest fixture) : IClassFixture<DomainFixtureTest>
    {
        private readonly IEnumerable<CategoryDto> _category = fixture.PeopleOfData.GetCategoryItems();
        private readonly IEnumerable<Guid> _categoryIds = fixture.PeopleOfData.GetCategoryIds();
        private readonly IEnumerable<CategoryDto> _categoryToUpdate = fixture.PeopleOfData.UpdateCategory();

        /// <summary>
        /// Tests all methods of the CategoryDomainService.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task CategoryDomainTestAll()
        {
            var cts = CancellationToken.None;
            var category = fixture.Mapper.Map<IEnumerable<Category>>(_category);
            var originalCategory = category.ToList();
            var categoryToUpdate = fixture.Mapper.Map<IEnumerable<Category>>(_categoryToUpdate);
            var updatedCategory = categoryToUpdate.ToList();

            var resultBool = await fixture.CategoryDomainService.ExistsAsync(false, false, originalCategory, cts);

            var resultAddAsync = await fixture.CategoryDomainService.Add(originalCategory, cts);

            var resultUpdateAsync = await fixture.CategoryDomainService.Update(_categoryIds,
                originalCategory, updatedCategory, cts);

            var resultGetByIdAsync = await fixture.CategoryDomainService.GetById(_categoryIds, updatedCategory, cts);

            var resultGetAllAsync = await fixture.CategoryDomainService.GetAll(updatedCategory, cts);

            var resultDeleteAsync = await fixture.CategoryDomainService.Delete(_categoryIds,updatedCategory, cts);

            Assert.False(resultBool);
            Assert.NotNull(resultAddAsync);
            Assert.NotNull(resultUpdateAsync);
            Assert.NotNull(resultGetByIdAsync);
            Assert.NotNull(resultGetAllAsync);
            Assert.NotNull(resultDeleteAsync);

            CompareCategories(originalCategory, resultAddAsync);
            CompareCategories(updatedCategory, resultUpdateAsync);
            CompareCategories(updatedCategory, resultGetByIdAsync);
            CompareCategories(updatedCategory, resultGetAllAsync);
            CompareCategories(updatedCategory, resultDeleteAsync);
        }

        /// <summary>
        /// Compares two collections of categories.
        /// </summary>
        /// <param name="categoriesOriginal">The original categories.</param>
        /// <param name="categoriesToUpdate">The categories to compare to the originals.</param>
        private static void CompareCategories(IEnumerable<Category> categoriesOriginal, IEnumerable<Category?> categoriesToUpdate)
        {
            var enumerable = categoriesOriginal.ToList();
            foreach (var item in categoriesToUpdate)
            {
                var userAdd = enumerable.FirstOrDefault(u => u.Id == item.Id);
                Assert.Equal(item.Id, userAdd.Id);
                Assert.Equal(item.Name, userAdd.Name);
                Assert.Equal(item.DataCreated, userAdd.DataCreated);
                Assert.Equal(item.DataUpdated, userAdd.DataUpdated);
                Assert.Equal(item.DataDeleted, userAdd.DataDeleted);
            }
        }
    }
}
