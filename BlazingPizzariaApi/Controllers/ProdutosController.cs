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
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly IProdutoRepository _produtoRepository;

        private readonly InjectServicesApi _injectServices;

        public ProdutosController(ILogger<ProdutosController> logger,IProdutoRepository produtoRepository, 
            InjectServicesApi injectServices)
        {
            _logger = logger;
            _injectServices = injectServices;
            _produtoRepository = produtoRepository;
            _injectServices = injectServices;
 
        }
        [HttpPost("add-produtos")]
        public async Task<ActionResult<IEnumerable<Produto>>> AddProduto([FromBody] List<ProdutoDtos> novosProdutosDto)
        {
            if (novosProdutosDto == null || novosProdutosDto.Any())
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
                var produtoDtos = _injectServices._mapper.Map<List<ProdutoDtos>>(produtos);
                return Ok(produtoDtos);
            }
            catch (Exception)
            {
                return StatusCode(500);
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

                var produtosDtos = _injectServices._mapper.Map<ProdutoDtos>(produto);

                return Ok(produtosDtos);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }
        [HttpGet]
        [Route("api/[controller]/GetItensPorCategorias/{categoriaId}")]
        public async Task<ActionResult<CategoriasDtos?>> GetCategorias(int categoriaId)
        {
            try
            {
                var categorias = await _produtoRepository.GetItensPorCategorias(categoriaId);

                if (categorias.Count() == 0)
                {
                    return NotFound("Categorias não foi encontrato");
                }

                var categoriasDtos = _injectServices._mapper.Map<List<CategoriasDtos>>(categorias);

                return Ok(categoriasDtos);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
