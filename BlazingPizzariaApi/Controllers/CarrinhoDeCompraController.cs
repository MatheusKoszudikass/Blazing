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
    public class CarrinhoDeCompraController(IProdutoRepository produtoRepository, ICarrinhoCompraRepository itemRepository,
        InjectServicesApi injectServicesApi, ILogger<CarrinhoDeCompraController> logger) : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly ICarrinhoCompraRepository _itemRepository = itemRepository;
        private readonly InjectServicesApi _injectServicesApi = injectServicesApi;
        private readonly ILogger<CarrinhoDeCompraController> _logger = logger;

        /// <summary>
        /// Adiciona um novo item ao carrinho.
        /// </summary>
        /// <param name="carrinhoItemAddDtos">DTO contendo os dados do item a ser adicionado.</param>
        /// <returns>DTO do item adicionado.</returns>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        /// <response code="201">Se o item for adicionado com sucesso.</response>
        /// <response code="500">Se ocorrer um erro ao adicionar o item.</response>
        [HttpPost]
        public async Task<ActionResult<CarrinhoDeItemsDto>> AddCarrinhoCompra([FromBody] CarrinhoItemAddDto carrinhoItemAddDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos."); // Status 400
            }

            try
            {
                // Adiciona o novo item ao carrinho
                var novoCarrinhoDeItem = await _itemRepository.AddCarrinhoCompra(carrinhoItemAddDtos);
                if (novoCarrinhoDeItem == null)
                {
                    return NoContent(); // Status 204
                }

                // Recupera o produto associado ao item adicionado
                var produto = await _produtoRepository.GetProdutoById(novoCarrinhoDeItem.ProdutoId) ?? 
                    throw new Exception($"Produto não localizado (id: {carrinhoItemAddDtos.ProdutoId})");

                // Mapeia o item do carrinho para o DTO correspondente
                var novoCarrinhoDeItemDtos = _injectServicesApi._mapper.Map<CarrinhoDeItemsDto>(novoCarrinhoDeItem);

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

        /// <summary>
        /// Obtém todos os itens do carrinho de um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário cujo carrinho de compras será obtido.</param>
        /// <returns>Lista de DTOs dos itens do carrinho.</returns>
        /// <response code="204">Se não houver itens no carrinho.</response>
        /// <response code="500">Se ocorrer um erro ao obter os itens.</response>
        [HttpGet]
        [Route("{usuarioId}/GetItens")]
        public async Task<ActionResult<IEnumerable<CarrinhoDeCompraDto>>> GetItens(Guid usuarioId)
        {
            try
            {
                var carrinhoDeCompra = await _itemRepository.GetItens(usuarioId);
                if (carrinhoDeCompra == null)
                {
                    return NoContent(); // Status 204
                }

                var carrinhoDeItensDtos = _injectServicesApi._mapper.Map<List<CarrinhoDeCompraDto>>(carrinhoDeCompra);
                return Ok(carrinhoDeItensDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtém um item específico do carrinho pelo ID.
        /// </summary>
        /// <param name="id">ID do item do carrinho.</param>
        /// <returns>DTO do item do carrinho.</returns>
        /// <response code="404">Se o item não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro ao obter o item.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarrinhoDeItemsDto>> GetItem(Guid id)
        {
            try
            {
                var carrinhoDeItem = await _itemRepository.GetItem(id);
                if (carrinhoDeItem == null)
                {
                    return NotFound("Item não encontrado"); // Status 404
                }

                if (carrinhoDeItem.Produto == null)
                {
                    return NotFound("Item não foi encontrado no produto");
                }

                var carrinhoDeItemDtos = _injectServicesApi._mapper.Map<CarrinhoDeItemsDto>(carrinhoDeItem);
                return Ok(carrinhoDeItemDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter o item = {id} do carrinho. Line(54)");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Exclui um item do carrinho pelo ID.
        /// </summary>
        /// <param name="id">ID do item a ser excluído.</param>
        /// <returns>DTO do item excluído.</returns>
        /// <response code="404">Se o item não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro ao excluir o item.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CarrinhoDeItemsDto>> DeleteItem(Guid id)
        {
            try
            {
                var carrinhoDeItem = await _itemRepository.DeleteItem(id);
                if (carrinhoDeItem == null)
                {
                    return NotFound(); // Status 404
                }

                var produto = await _produtoRepository.GetProdutoById(carrinhoDeItem.Id);
                if (produto == null)
                {
                    return NotFound(); // Status 404
                }

                var carrinhoDeItemDtos = _injectServicesApi._mapper.Map<CarrinhoDeItemsDto>(produto);
                return Ok(carrinhoDeItemDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Atualiza a quantidade de um item no carrinho.
        /// </summary>
        /// <param name="id">ID do item do carrinho.</param>
        /// <param name="carrinhoDeItemAtualizarQuantidadeDto">DTO com a nova quantidade.</param>
        /// <returns>DTO do item do carrinho atualizado.</returns>
        /// <response code="404">Se o item não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro ao atualizar a quantidade.</response>
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CarrinhoDeItemsDto>> UpdateItemQuantity(Guid id,
            CarrinhoDeItemAtualizarQuantidadeDto carrinhoDeItemAtualizarQuantidadeDto)
        {
            try
            {
                var carrinhoDeItem = await _itemRepository.UpdateItemQuantity(id, carrinhoDeItemAtualizarQuantidadeDto);
                if (carrinhoDeItem == null)
                {
                    return NotFound(); // Status 404
                }

                var produto = await _produtoRepository.GetProdutoById(carrinhoDeItem.ProdutoId);
                var carrinhoItemDto = _injectServicesApi._mapper.Map<CarrinhoDeItemsDto>(produto);
                return Ok(carrinhoItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
