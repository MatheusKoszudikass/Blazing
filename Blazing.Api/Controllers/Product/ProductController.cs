using Blazing.Api.Erros.ControllerExceptions;
using Blazing.Application.Dto;
using Blazing.Application.Interfaces.Product;
using Blazing.infrastructure.Interface;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ILogger<ProductController> logger, IProductInfrastructureRepository produtoRepository) : ControllerBase
    {

        private readonly ILogger<ProductController> _logger = logger;
        private readonly IProductInfrastructureRepository _productInfraRepository = produtoRepository;

        /// <summary>
        /// Adds a list of new productsDto.
        /// </summary>
        /// <param name="newProductsDto">List of DTOs of productsDto to be added.</param>
        /// <returns>List of added productsDto.</returns>
        /// <response code="400">If the productsDto list is null or empty.</response>
        /// <response code="500">If an error occurs while adding productsDto.</response>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductDto>>> AddProducts([FromBody] IEnumerable<ProductDto> newProductsDto, CancellationToken cancellationToken)
        {
               var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

               var produtosAdicionados = await _productInfraRepository.AddProducts(newProductsDto, cancellationToken);
            _logger.LogInformation("Produtos adicionados com sucesso. Total de produtos: {TotalProducts}. IP: {IpAddress}",
                    produtosAdicionados.Count(), ipAddress);

            return Ok(produtosAdicionados); // Status 200
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
        [HttpPut("Update")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> UpdateProduto([FromBody]IEnumerable<ProductDto> productDto, CancellationToken cancellationToken)
        {
            var id = productDto.Select(x => x.Id).ToList();
            if (productDto == null || id.Count == 0)
            {
                return BadRequest(); // Status 400
            }

            var editProduto = await _productInfraRepository.UpdateProduct(id, productDto, cancellationToken);

            if (editProduto == null)
            {
                return NotFound(); // Status 404
            }

            return Ok(editProduto); // Status 200
        }

        /// <summary>
        /// Gets productsDto from a specific categoryDto.
        /// </summary>
        /// <param name="id">categoryDto ID.</param>
        /// <returns>List of products in the categoryDto.</returns>
        /// <response code="404">If no categoryDto is found.</response>
        /// <response code="500">If an error occurs while getting the categoryDto.</response>
        [HttpGet("categoryId")]
        public async Task<ActionResult<IEnumerable<ProductDto?>>> GetProductsByCategoryId([FromQuery]IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            try
            {
                var categorias = await _productInfraRepository.GetProductsByCategoryId(id, cancellationToken);

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
        [HttpDelete("Delete")]
        public async Task<ActionResult<ProductDto>> DeleteProducts([FromBody] IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
 
                var produtoDeletados = await _productInfraRepository.DeleteProducts(id, cancellationToken);
                _logger.LogInformation($"Produtos excluídos com sucesso. Total de produtos excluídos: {produtoDeletados.Count()}.");
                return Ok(produtoDeletados); // Status 200
        }

        /// <summary>
        /// Gets a specific productDto by ID.
        /// </summary>
        /// <param name="id">productDto ID.</param>
        /// <returns>productDto.</returns>
        /// <response code="404">If the productDto is not found.</response>
        /// <response code="500">If an error occurs while retrieving the productDto.</response>
        [HttpGet("ProductId")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromQuery]IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            try
            {
                var produto = await _productInfraRepository.GetProductById(id, cancellationToken);
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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken)
        {
           
                var produtos = await _productInfraRepository.GetAll(cancellationToken);
                if (!produtos.Any())
                {
                   throw new NotFoundException("Produtos não encontrados");
                }

                return Ok(produtos); // Status 200
            
        }
    }
    #endregion
}

