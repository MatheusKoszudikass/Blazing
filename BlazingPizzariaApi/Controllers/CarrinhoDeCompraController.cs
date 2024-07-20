using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Api.Repositories.Services;
using BlazingPizza.Models.DTOs;
using BlazingPizzaria.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazingPizza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoDeCompraController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICarrinhoCompraRepository _itemRepository;
        private readonly InjectServicesApi _injectServicesApi;
        private readonly ILogger<CarrinhoDeCompraController> _logger;

        public CarrinhoDeCompraController(IProdutoRepository produtoRepository, ICarrinhoCompraRepository itemRepository,
            InjectServicesApi injectServicesApi, ILogger<CarrinhoDeCompraController> logger)
        {
            _produtoRepository = produtoRepository;
            _itemRepository = itemRepository;
            _injectServicesApi = injectServicesApi;
            this._logger = logger;
        }

        //Controlr vincular item do carrrinho para o usuário.
        [HttpGet]
        [Route("{usuarioId}/GetItens")]
        public async Task<ActionResult<IEnumerable<CarrinhoDeCompraDtos>>> GetItens(int usuarioId)
        {
            try
            {
                var carrinhoDeCompra = await _itemRepository.GetItens(usuarioId);
                if (carrinhoDeCompra == null)
                {
                    return NoContent();
                }
                var carrinhoDeItensDtos = _injectServicesApi._mapper.Map<List<CarrinhoDeCompraDtos>>(carrinhoDeCompra);
                return Ok(carrinhoDeItensDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarrinhoDeItemsDtos>> GetItem(int id)
        {

            try
            {
                var carrinhoDeItem = await _itemRepository.GetItem(id);
                if (carrinhoDeItem == null)
                {
                    return NotFound("Item não encontrado"); //404 status code
                }

                if (carrinhoDeItem.Produto == null)
                {
                    return NotFound("Item não foi encontrado no produto");
                }
                var carrinhoDeItemDtos = _injectServicesApi._mapper.Map<CarrinhoDeItemsDtos>(carrinhoDeItem);
                return Ok(carrinhoDeItemDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter o item  ={id} do carrinho. Line(54)");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CarrinhoDeItemsDtos>> PostItem([FromBody] CarrinhoItemAddDtos carrinhoItemAddDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                // Adiciona o novo item no carrinho
                var novoCarrinhoDeItem = await _itemRepository.AddItem(carrinhoItemAddDtos);
                if (novoCarrinhoDeItem == null)
                {
                    return NoContent(); // Status 204
                }

                // Recupera o produto associado ao item adicionado
                var produto = await _produtoRepository.GetItem(novoCarrinhoDeItem.ProdutoId);
                if (produto == null)
                {
                    throw new Exception($"Produto não localizado (id: {carrinhoItemAddDtos.ProdutoId})");
                }

                // Mapeia o produto para o DTO do carrinho de itens
                var novoCarrinhoDeItemDtos = _injectServicesApi._mapper.Map<CarrinhoDeItemsDtos>(novoCarrinhoDeItem);

                // Retorna a resposta de criação com o item adicionado
                return CreatedAtAction(nameof(GetItem), new { id = novoCarrinhoDeItemDtos.Id }, novoCarrinhoDeItemDtos);
            }
            catch (Exception ex)
            {
                // Registra o erro e retorna um status 500
                _logger.LogError(ex, "Erro ao criar um novo item no carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CarrinhoDeItemsDtos>> DeleteItem(int id)
        {

            try
            {
                var carrinhoDeItem = await _itemRepository.DeletItem(id);
                if (carrinhoDeItem == null)
                {
                    return NotFound();
                }
                var produto = await _produtoRepository.GetItem(carrinhoDeItem.Id);

                if (produto == null)
                {
                    return NotFound();
                }
                var carrinhoDeItemDtos = _injectServicesApi._mapper.Map<CarrinhoDeItemsDtos>(produto);
                return Ok(carrinhoDeItemDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CarrinhoDeItemsDtos>> AtualizarQuantidade(int id,
            CarrinhoDeItemAtualizarQuantidadeDto carrinhoDeItemAtualizarQuantidadeDto)
        {
            try
            {
                var carrinhoDeItem = await _itemRepository.AddItensQuantidade(id, carrinhoDeItemAtualizarQuantidadeDto);
                if (carrinhoDeItem == null)
                {
                    return NotFound();
                }
                var produto = await _produtoRepository.GetItem(carrinhoDeItem.ProdutoId);
                var carrinhoItemDto = _injectServicesApi._mapper.Map<CarrinhoDeItemsDtos>(produto);
                return Ok(carrinhoItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
