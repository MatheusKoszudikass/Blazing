using Blazing.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Test.Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryControllerFixtureTest"/> class.
    /// </summary>
    /// <param name="fixture">The fixture used to set up the necessary data and dependencies.</param>
    public class CategoryControllerFixtureTest(ControllerFixtureTest fixture) : IClassFixture<ControllerFixtureTest>
    {
        private readonly IEnumerable<CategoryDto> _category = fixture.PeopleOfData.GetCategoryItems();
        private readonly IEnumerable<Guid> _idCategory = fixture.PeopleOfData.GetCategoryIds();
        private readonly IEnumerable<CategoryDto> _categoryUpdated = fixture.PeopleOfData.UpdateCategory();


        /// <summary>
        /// Tests all methods of the CategoryController.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task ProductControllerTest()
        {
            var cts = CancellationToken.None;
            var originalCategoryDto = _category.ToList();
            var updatedCategoryDto = _categoryUpdated.ToList();

            fixture.CategoryInfrastructureRepository.Setup(repo => repo.AddCategories(originalCategoryDto, cts)).ReturnsAsync(originalCategoryDto);
            fixture.CategoryInfrastructureRepository.Setup(repo => repo.UpdateCategory(_idCategory, updatedCategoryDto, cts)).ReturnsAsync(updatedCategoryDto);
            fixture.CategoryInfrastructureRepository.Setup(repo => repo.GetCategoryById(_idCategory, cts)).ReturnsAsync(updatedCategoryDto);
            fixture.CategoryInfrastructureRepository.Setup(repo => repo.GetAll(1, 50, cts)).ReturnsAsync(updatedCategoryDto);
            fixture.CategoryInfrastructureRepository.Setup(repo => repo.DeleteCategory(_idCategory, cts)).ReturnsAsync(updatedCategoryDto);


            var resultAddCategories = await fixture.CategoryController.AddCategories(originalCategoryDto, cts);
            var okResult = Assert.IsType<OkObjectResult>(resultAddCategories.Result);
            var returnProducts = Assert.IsType<List<CategoryDto>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);

            var resultUpdateCategories = await fixture.CategoryController.UpdateCategories(updatedCategoryDto, cts);
            okResult = Assert.IsType<OkObjectResult>(resultUpdateCategories.Result);
            returnProducts = Assert.IsType<List<CategoryDto>>(okResult.Value);
            Assert.Single(returnProducts);

            var resultGetByCategory = await fixture.CategoryController.GetCategoryById(_idCategory, cts);
            okResult = Assert.IsType<OkObjectResult>(resultGetByCategory.Result);
            returnProducts = Assert.IsType<List<CategoryDto>>(okResult.Value);
            Assert.Single(returnProducts);

            var resultGetAll = await fixture.CategoryController.GetAllCategories(1, 2, cts);
            okResult = Assert.IsType<OkObjectResult>(resultGetAll.Result);
            returnProducts = Assert.IsType<List<CategoryDto>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);

            var resultDeletedCategories = await fixture.CategoryController.DeleteCategories(_idCategory, cts);
            okResult = Assert.IsType<OkObjectResult>(resultDeletedCategories.Result);
            returnProducts = Assert.IsType<List<CategoryDto>>(okResult.Value);
            Assert.Single(returnProducts);
        }
    }
}
