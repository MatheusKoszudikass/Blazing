using Blazing.Application.Dto;
using Blazing.Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    /// <typeparam name="_logger">The type of the logger.</typeparam>
    /// <typeparam name="_productInfraRepository">The type of the product infrastructure service.</typeparam>
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
       [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductDto>>> AddProducts([FromBody] IEnumerable<ProductDto> newProductsDto, CancellationToken cancellationToken)
        {
            
            var productAdded = await _productInfraRepository.AddProducts(newProductsDto, cancellationToken);
            var productName = productAdded.Select(x => x.Name).ToList();

            _logger.LogInformation("Produtos adicionados com sucesso. nomes dos produtos: {names}. Total de produtos: {TotalProducts}.",
                     productName, productAdded.Count());

            return Ok(productAdded); // Status 200
        }

        /// <summary>
        /// Edit an existing productDto.
        /// </summary>
        /// <param name="id">The identifier of the productDto to be edited.</param>
        /// <param name="productDto">DTO with updated productDto data.</param>
        /// <returns>DTO of the edited product.</returns>
        [HttpPut("update")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> UpdateProduto([FromBody] IEnumerable<ProductDto> updateProductDto, CancellationToken cancellationToken)
        {
            var id = updateProductDto.Select(x => x.Id).ToList();
            var productUpdated = await _productInfraRepository.UpdateProduct(id, updateProductDto, cancellationToken);

            _logger.LogInformation("Produtos foram atualizados com sucesso utilizando os identificadores: {id}. Total de produtos editados: {TotalProducts}.",
                id, productUpdated.Count());

            return Ok(productUpdated); // Status 200
        }

        /// <summary>
        /// Get productsDto from a specific categoryDto.
        /// </summary>
        /// <param name="id">the identifier of categoryDto.</param>
        /// <returns>List of products in categoryDto.</returns>
        [HttpGet("categoryId")]
        public async Task<ActionResult<IEnumerable<ProductDto?>>> GetProductsByCategoryId([FromQuery] IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var produtcsCategories = await _productInfraRepository.GetProductsByCategoryId(id, cancellationToken);

            _logger.LogInformation("Produtos recuperados com sucesso utilizando os identificadores de categoria: {id}. Total de produtos: {TotalProducts}.",
                id, produtcsCategories.Count());

            return Ok(produtcsCategories); // Status 200
        }


        /// <summary>
        /// Deletes a productDto list.
        /// </summary>
        /// <param name="id">List of productDto and the identifier to delete.</param>
        /// <returns>List of deleted productDto.</returns>
        [HttpDelete("delete")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> DeleteProducts([FromBody] IEnumerable<Guid> id, CancellationToken cancellationToken)
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
        /// <returns>productDto.</returns>
        [HttpGet("productId")]
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
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken)
        {
            var products = await _productInfraRepository.GetAll(cancellationToken);

            _logger.LogInformation("Produtos recuperados com sucesso. Total de produtos: {TotalProducts}.",
                products.Count());

            return Ok(products); // Status 200
        }
    }
    #endregion
}

