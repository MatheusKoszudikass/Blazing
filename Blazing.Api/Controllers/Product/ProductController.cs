using Blazing.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Blazing.Ecommerce.Interface;

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
    /// <parameter name="logger">The type of the logger.</parameter>
    /// <parameter name="produtoRepository">The type of the product infrastructure service.</parameter>
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
        /// <param name="cancellationToken"></param>
        /// <returns>List of added productsDto.</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductDto>>> AddProducts([FromBody] IEnumerable<ProductDto> newProductsDto,
            CancellationToken cancellationToken)
        {
            var productAdded = await _productInfraRepository.AddProducts(newProductsDto, cancellationToken);

            var productDto = productAdded.ToList();
            var productName = productDto.Select(x => x.Name).ToList();

            _logger.LogInformation("Produtos adicionados com sucesso. nomes dos produtos: {names}. Total de produtos: {TotalProducts}.",
                     productName, productDto.Count());

            return Ok(productAdded); // Status 200
        }

        /// <summary>
        /// Edit an existing productDto.
        /// </summary>
        /// <param name="id">The identifier of the productDto to be edited.</param>
        /// <param name="updateProductDto">DTO with updated productDto data.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>DTO of the edited product.</returns>
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> UpdateProduto([FromQuery]IEnumerable<Guid> id, 
            [FromBody] IEnumerable<ProductDto> updateProductDto, CancellationToken cancellationToken)
        {
            var productUpdated = await _productInfraRepository.UpdateProduct(id, updateProductDto, cancellationToken);

            _logger.LogInformation("Produtos foram atualizados com sucesso utilizando os identificadores: {id}. Total de produtos editados: {TotalProducts}.",
                id, productUpdated.Count());

            return Ok(productUpdated); // Status 200
        }

        /// <summary>
        /// Get productsDto from a specific categoryDto.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="id">the identifier of categoryDto.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of products in categoryDto.</returns>
        [Authorize]
        [HttpGet("categoryId")]
        public async Task<ActionResult<IEnumerable<ProductDto?>>> GetProductsByCategoryId(int page, int pageSize, [FromQuery] IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            if(pageSize > 50)
                pageSize = 50;

            var productsCategories = await _productInfraRepository.GetProductsByCategoryId(page, pageSize, id, cancellationToken);

            _logger.LogInformation("Produtos recuperados com sucesso utilizando os identificadores de categoria: {id}. Total de produtos: {TotalProducts}.",
                id, productsCategories.Count());

            return Ok(productsCategories); // Status 200
        }

        /// <summary>
        /// Deletes a productDto list.
        /// </summary>
        /// <param name="id">List of productDto and the identifier to delete.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of deleted productDto.</returns>
        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> DeleteProducts([FromQuery] IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var productsDeleted = await _productInfraRepository.DeleteProducts(id, cancellationToken);

            _logger.LogInformation("Produtos excluídos com sucesso dos identificadores: {id}. Total de produtos excluídos: {TotalProducts}.",
                id, productsDeleted.Count());

            return Ok(productsDeleted); // Status 200
        }

        /// <summary>
        /// Gets a specific productDto by its identifier.
        /// </summary>
        /// <param name="id">the identifier of the productDto.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>productDto.</returns>
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<ProductDto?>>> GetProductById([FromQuery] IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var productsById = await _productInfraRepository.GetProductById(id, cancellationToken);

            _logger.LogInformation("Produtos recuperados com sucesso utilizando o identificador: {id}. Total de produtos: {TotalProducts}.",
                id, productsById.Count());

            return Ok(productsById); // Status 200
        }

        /// <summary>
        /// Gets all productsDto.
        /// </summary>
        /// <returns>List of product DTOs.</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] int page, int pageSize, CancellationToken cancellationToken)
        {
            if (pageSize > 50)
                pageSize = 50;

            var products = await _productInfraRepository.GetAll(page, pageSize, cancellationToken);

            _logger.LogInformation("Produtos recuperados com sucesso. Total de produtos: {TotalProducts}.",
                products.Count());

            return Ok(products); // Status 200
        }
    }
    #endregion
}