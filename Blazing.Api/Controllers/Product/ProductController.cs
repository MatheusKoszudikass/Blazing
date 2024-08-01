using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blazing.Api.Controllers.Product
{
    #region Controller product.
    /// <summary>
    /// This class represents the controller for the Product API.
    /// It contains methods for adding, updating, deleting, and retrieving products.
    /// </summary>
    /// <remarks>
    /// This class requires an instance of ILogger and IProductAppService to be passed in the constructor.
    /// </remarks>
    /// <typeparam name="TLogger">The type of the logger.</typeparam>
    /// <typeparam name="TProdutoAppService">The type of the product app service.</typeparam>
    public class ProductController(ILogger<ProductController> logger, IProductAppService produtoRepository) : ControllerBase
    {
        private readonly ILogger<ProductController> _logger = logger;
        private readonly IProductAppService _productAppService = produtoRepository;

        /// <summary>
        /// Adds a list of new productsDto.
        /// </summary>
        /// <param name="newProductsDto">List of DTOs of productsDto to be added.</param>
        /// <returns>List of added productsDto.</returns>
        /// <response code="400">If the productsDto list is null or empty.</response>
        /// <response code="500">If an error occurs while adding productsDto.</response>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductDto>>> AddProducts([FromBody] IEnumerable<ProductDto> newProductsDto)
        {
            if (newProductsDto == null || !newProductsDto.Any())
            {
                return BadRequest("A lista de produtos não pode estar vazia."); // Status 400
            }

            try
            {
                var produtosAdicionados = await _productAppService.AddProducts(newProductsDto);
                return Ok(newProductsDto); // Status 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar novos produtos."); // Log do erro
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }

        /// <summary>
        /// Edit an existing productDto.
        /// </summary>
        /// <param name="id">ID of the productDto to be edited.</param>
        /// <param name="productDto">DTO with updated productDto data.</param>
        /// <returns>DTO of the edited product.</returns>
        /// <response code="400">If the productDto  is null or the ID does not match.</response>F
        /// <response code="404">If the productDto is not found.</response>
        /// <response code="200">If the productDto is edited successfully.</response>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(Guid id, [FromBody] ProductDto productDto)
        {
            if (productDto == null || id != productDto.Id)
            {
                return BadRequest(); // Status 400
            }

            var editProduto = await _productAppService.UpdateProduct(id, productDto);

            if (editProduto == null)
            {
                return NotFound(); // Status 404
            }

            return Ok(editProduto); // Status 200
        }

        /// <summary>
        /// Gets productsDto from a specific categoryDto.
        /// </summary>
        /// <param name="categoryId">categoryDto ID.</param>
        /// <returns>List of products in the categoryDto.</returns>
        /// <response code="404">If no categoryDto is found.</response>
        /// <response code="500">If an error occurs while getting the categoryDto.</response>
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto?>>> GetProductsByCategoryId(Guid categoryId)
        {
            try
            {
                var categorias = await _productAppService.GetProductsByCategoryId(categoryId);

                if (categorias == null || !categorias.Any())
                {
                    return NotFound("Categorias não foi encontrado"); // Status 404
                }

                return Ok(categorias); // Status 200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }


        /// <summary>
        /// Deletes a productDto list.
        /// </summary>
        /// <param name="id">List of productDto IDs to be deleted.</param>
        /// <returns>List of excluded productDto.</returns>
        /// <response code="404">If no productDto are found to delete.</response>
        /// <response code="500">If an error occurs while deleting productDto.</response>
        [HttpDelete("delete")]
        public async Task<ActionResult<ProductDto>> DeleteProducts([FromBody] IEnumerable<Guid> id)
        {
            try
            {
                var produtoDeletados = await _productAppService.DeleteProducts(id);
                if (produtoDeletados == null)
                {
                    return NotFound(); // Status 404
                }

                return Ok(produtoDeletados); // Status 200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }

        /// <summary>
        /// Gets a specific productDto by ID.
        /// </summary>
        /// <param name="id">productDto ID.</param>
        /// <returns>productDto.</returns>
        /// <response code="404">If the productDto is not found.</response>
        /// <response code="500">If an error occurs while retrieving the productDto.</response>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            try
            {
                var produto = await _productAppService.GetProductById(id);
                if (produto == null)
                {
                    return NotFound("Produto não localizado"); // Status 404
                }

                return Ok(produto); // Status 200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }


        /// <summary>
        /// Gets all productsDto.
        /// </summary>
        /// <returns>List of product DTOs.</returns>
        /// <response code="404">If no productDto are found.</response>
        /// <response code="500">If an error occurs while retrieving productDto.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            try
            {
                var produtos = await _productAppService.GetAll();
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
    }
    #endregion
}

