﻿using Blazing.Application.Dto;
using Blazing.Ecommerce.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazing.Api.Controllers.Category
{
    #region Controller categories.

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ILogger<CategoryController> logger, ICategoryInfrastructureRepository categoriaRepository) : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger = logger;
        private readonly ICategoryInfrastructureRepository _categoriaRepository = categoriaRepository;

        /// <summary>
        /// Adds a list of new categories.
        /// </summary>
        /// <param name="newCategorias">List of DTOs of the categories to be added..</param>
        /// <returns>List of categories added.</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoryDto?>>> AddCategories([FromBody] IEnumerable<CategoryDto> newCategorias, CancellationToken cancellationToken)
        {
            var categoriesAdded = await _categoriaRepository.AddCategories(newCategorias, cancellationToken);
            var categoriesName = newCategorias.Select(c => c.Name).ToList();

            _logger.LogInformation("Categoria adicionadas com sucesso. nomes dos produtos: {name}. Total de categorias: {TotalCategories}.",
                categoriesName, newCategorias.Count());

            return Ok(categoriesAdded);
        }

        /// <summary>
        /// Updates existing categories in the database.
        /// </summary>
        /// Category ID and selected from the categories IEnumerable the id property to be updated.
        /// <param name="updateCategories">DTO containing the new category information.</param>
        /// <returns>DTO of the updated category.</returns>
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<IEnumerable<CategoryDto?>>> UpdateCategories([FromBody] IEnumerable<CategoryDto> updateCategories, CancellationToken cancellationToken)
        {
            var id = updateCategories.Select(p => p.Id).ToList();
            var categoriesUpdated = await _categoriaRepository.UpdateCategory(id, updateCategories, cancellationToken);

            _logger.LogInformation("Categorias foram atualizadas com sucesso utilizando os identificadores: {id}. Total de produtos editados: {TotalCategories}.",
                id, updateCategories.Count());

            return Ok(updateCategories); // Status 200
        }

        /// <summary>
        /// Deletes a list of categories by identifier.
        /// </summary>
        /// <param name="id">List of category IDs to delete.</param>
        /// <returns>List of category DTOs to delete.</returns>
        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult<CategoryDto?>> DeleteCategories([FromQuery]IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var categoriesDeleted = await _categoriaRepository.DeleteCategory(id, cancellationToken);

            _logger.LogInformation("Categorias deletadas com sucesso utilizando os identificadores: {id}. Total de categorias deletadas: {TotalCategories}.",
                id, categoriesDeleted.Count());

            return Ok(categoriesDeleted); // Status 200
        }

        /// <summary>
        /// Get the specific category by ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>Category DTO.</returns>
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById([FromQuery] IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.GetCategoryById(id, cancellationToken);

            _logger.LogInformation("Categorias recuperados com sucesso utilizando os identificadores de categoria: {id}. Total de produtos: {TotalCategories}",
                id, categoria.Count());

            return Ok(categoria); // Status 200
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>List of category DTOs.</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories([FromQuery]int page, int pageSize, CancellationToken cancellationToken)
        {
            if (pageSize > 50)
            {
                pageSize = 50;
            }
            var categories = await _categoriaRepository.GetAll(page, pageSize,cancellationToken);

            _logger.LogInformation("Categoria recuperados com sucesso. Total de categorias: {TotalCategories}",
                categories.Count());

            return Ok(categories); // Status 200
        }
    }
    #endregion
}
