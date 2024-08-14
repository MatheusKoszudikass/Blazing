using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using Blazing.infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlazingPizza.Api.Controllers
{
    #region Controller categories.
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ILogger<CategoryController> logger, ICategoryInfrastructureRepository categoriaRepository) : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger = logger;
        private readonly ICategoryInfrastructureRepository _categoriaRepository = categoriaRepository;

        #region Adds a new category.
        /// <summary> 
        /// Adds a list of new categories.
        /// </summary> /// <param name="newCategorias">List of DTOs of the categories to be expanded.</param>
        /// <returns>List of expanded categories.</returns> 
        /// <response code="400">If the list of categories is null or empty.</response> 
        /// <response code="500">If an error occurs while adding the categories.</response>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoryDto?>>> AddCategories([FromBody]IEnumerable<CategoryDto> newCategorias, CancellationToken cancellationToken)
        {
            if (newCategorias == null || !newCategorias.Any())
            {
                return BadRequest("A lista de categorias não pode estar vazia."); // Status 400
            }

            try
            {
                var produtosAdicionados = await _categoriaRepository.AddCategories(newCategorias, cancellationToken);

                return Ok(produtosAdicionados); // Status 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar novas categorias."); // Log do erro
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }
        #endregion

        #region Update information in the category.
        /// <summary>
        /// Updates an existing category in the database.
        /// </summary>
        /// Category ID and selected from the categories IEnumerable the id property to be updated.
        /// <param name="categories">DTO containing the new category information.</param>
        /// <returns>Updated category DTO.</returns>
        /// <exception cref="ArgumentException">Thrown when the category ID is invalid or the category is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs while updating the category.</exception>
        [HttpPut("Api/Category/update")]
        public async Task<ActionResult<IEnumerable<CategoryDto?>>> UpdateCategories([FromBody]IEnumerable<CategoryDto> categories, CancellationToken cancellationToken)
        {
           var ids = categories.Select(p => p.Id).ToList();
            if (categories == null || !categories.Any())
            {
                return BadRequest(); // Status 400
            }
  
            var editProduto = await _categoriaRepository.UpdateCategory(ids, categories, cancellationToken);

            if (editProduto == null)
            {
                return NotFound(); // Status 404
            }

            return Ok(editProduto); // Status 200
        }
        #endregion

        #region Excludes categories.
        /// <summary>
        /// Deletes a list of categories by ID.
        /// </summary>
        /// <param name="id">List of category IDs to delete.</param>
        /// <returns>List of category DTOs to delete.</returns>
        /// <exception cref="ArgumentException">Thrown when no category is found to delete.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs while deleting categories.</exception>
        [HttpDelete("delete")]
        public async Task<ActionResult<CategoryDto?>> DeleteCategories(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            try
            {
                var categoriesDeleted = await _categoriaRepository.DeleteCategory(id, cancellationToken);
                if (categoriesDeleted == null)
                {
                    return NotFound(); // Status 404
                }

                return Ok(categoriesDeleted); // Status 200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }
        #endregion

        #region Gets data from a category through the id.
        /// <summary>
        /// Gets the specific category by ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>Category DTO.</returns>
        /// <response code="404">If the category is not found.</response>
        /// <response code="500">If an error occurred while getting the category.</response>
        [HttpGet("/CategoriesId")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById([FromQuery]IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            try
            {
                var categoria = await _categoriaRepository.GetCategoryById(id, cancellationToken);
                if (categoria == null)
                {
                    return NotFound("Produto não localizado"); // Status 404
                }
                return Ok(categoria); // Status 200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }
        #endregion

        #region Get all categories.
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>List of category DTOs.</returns>
        /// <response code="404">If no categories are found.</response>
        /// <response code="500">If an error occurred while getting the categories.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllProdutos(CancellationToken cancellationToken)
        {
            try
            {
                var produtos = await _categoriaRepository.GetAll(cancellationToken);
                if (produtos == null || !produtos.Any())
                {
                    return NotFound("Nenhum produto foi localizado"); // Status 404
                }

                return Ok(produtos); // Status 200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }
        #endregion
    }
    #endregion
}
