using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Api.Repositories.Security;
using BlazingPizzaria.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BlazingPizza.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProdutosController(ILogger<ProdutosController> logger, IProdutoRepository produtoRepository) : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger = logger;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        [HttpPost("add-produtos")]
        public async Task<ActionResult<IEnumerable<ProdutoDtos>>> AddProduto([FromBody] List<ProdutoDtos> novosProdutosDto)
        {
            if (novosProdutosDto == null || novosProdutosDto.Count == 0)
            {
                return BadRequest("A lista de produtos não pode estar vazia.");
            }
            try
            {
                var produtosAdicionados = await _produtoRepository.AddProduto(novosProdutosDto);
                return Ok(novosProdutosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Erro ao adicionar novos produtos.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDtos>> EditProduto(int id, [FromBody] ProdutoDtos produtoDtos)
        {
            if (produtoDtos == null || id != produtoDtos.Id)
            {
                return BadRequest();
            }

            var editProduto = await _produtoRepository.EditProduto(id, produtoDtos);

            if (editProduto == null)
            {
                return NotFound();
            }
            return Ok(editProduto);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDtos>>> GetItens()
        {
            try
            {
                var produtos = await _produtoRepository.GetItens();
                if (produtos == null || !produtos.Any())
                {
                    return NotFound("Nenhum produto foi localizado");
                }
             
                return Ok(produtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoDtos>> GetItem(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetItem(id);
                if (produto == null)
                {
                    return NotFound("Produto não localizado");
                }

                return Ok(produto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
            }

        }
        [HttpGet]
        [Route("api/[controller]/GetItensPorCategorias/{categoriaId}")]
        public async Task<ActionResult<ProdutoDtos?>> GetCategorias(int categoriaId)
        {
            try
            {
                var categorias = await _produtoRepository.GetItensProdutoCategoria(categoriaId);

                if (categorias == null || !categorias.Any())
                {
                    return NotFound("Categorias não foi encontrato");
                }

                return Ok(categorias);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
            }
        }
        [HttpDelete("batch")]
        public async Task<ActionResult<ProdutoDtos>> DeleteProduto([FromBody] List<int> id)
        {
            try
            {
                var produtoDeletados = await _produtoRepository.DeleteProduto(id);
                if (produtoDeletados == null)
                {
                    return NotFound();
                }
                return Ok(produtoDeletados);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
            }

        }
    }
}
