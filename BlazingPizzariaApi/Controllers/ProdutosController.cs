using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Api.Repositories.Security;
using BlazingPizzaria.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BlazingPizza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController(ILogger<ProdutosController> logger, IProdutoRepository produtoRepository) : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger = logger;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;


        /// <summary>
        /// Adiciona uma lista de novos produtos.
        /// </summary>
        /// <param name="novosProdutosDto">Lista de DTOs dos produtos a serem adicionados.</param>
        /// <returns>Lista dos produtos adicionados.</returns>
        /// <response code="400">Se a lista de produtos for nula ou vazia.</response>
        /// <response code="500">Se ocorrer um erro ao adicionar os produtos.</response>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> AddProdutos([FromBody] List<ProdutoDto> novosProdutosDto)
        {
            if (novosProdutosDto == null || novosProdutosDto.Count == 0)
            {
                return BadRequest("A lista de produtos não pode estar vazia."); // Status 400
            }

            try
            {
                var produtosAdicionados = await _produtoRepository.AddProdutos(novosProdutosDto);
                return Ok(novosProdutosDto); // Status 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar novos produtos."); // Log do erro
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }

        /// <summary>
        /// Edita um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser editado.</param>
        /// <param name="produtoDtos">DTO com os dados atualizados do produto.</param>
        /// <returns>DTO do produto editado.</returns>
        /// <response code="400">Se o DTO do produto for nulo ou o ID não corresponder.</response>
        /// <response code="404">Se o produto não for encontrado.</response>
        /// <response code="200">Se o produto for editado com sucesso.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDto>> UpdateProduto(Guid id, [FromBody] ProdutoDto produtoDtos)
        {
            if (produtoDtos == null || id != produtoDtos.Id)
            {
                return BadRequest(); // Status 400
            }

            var editProduto = await _produtoRepository.UpdateProduto(id, produtoDtos);

            if (editProduto == null)
            {
                return NotFound(); // Status 404
            }

            return Ok(editProduto); // Status 200
        }

        /// <summary>
        /// Obtém produtos de uma categoria específica.
        /// </summary>
        /// <param name="categoriaId">ID da categoria.</param>
        /// <returns>Lista de produtos na categoria.</returns>
        /// <response code="404">Se nenhuma categoria for encontrada.</response>
        /// <response code="500">Se ocorrer um erro ao obter as categorias.</response>
        [HttpGet("{categoriaId}")]
        public async Task<ActionResult<ProdutoDto?>> GetProdutosByCategoriaId(Guid categoriaId)
        {
            try
            {
                var categorias = await _produtoRepository.GetProdutosByCategoriaId(categoriaId);

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
        /// Exclui uma lista de produtos.
        /// </summary>
        /// <param name="id">Lista de IDs dos produtos a serem excluídos.</param>
        /// <returns>Lista de produtos excluídos.</returns>
        /// <response code="404">Se nenhum produto for encontrado para exclusão.</response>
        /// <response code="500">Se ocorrer um erro ao excluir os produtos.</response>
        [HttpDelete("delete")]
        public async Task<ActionResult<ProdutoDto>> DeleteProdutos([FromBody] List<Guid> id)
        {
            try
            {
                var produtoDeletados = await _produtoRepository.DeleteProdutos(id);
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
        /// Obtém um produto específico pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>DTO do produto.</returns>
        /// <response code="404">Se o produto não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro ao obter o produto.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoDto>> GetProdutoById(Guid id)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoById(id);
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
        /// Obtém todos os produtos.
        /// </summary>
        /// <returns>Lista de DTOs dos produtos.</returns>
        /// <response code="404">Se nenhum produto for encontrado.</response>
        /// <response code="500">Se ocorrer um erro ao obter os produtos.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAllProdutos()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllProdutos();
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
}
