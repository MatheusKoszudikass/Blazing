using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizzaria.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazingPizza.Api.Controllers
{
    #region Controlador de categorias.
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController(ILogger<CategoriaController> logger, ICategoriaRepository categoriaRepository) : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger = logger;
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

        #region Adiciona uma nova categoria.
        /// <summary>
        /// Adiciona uma lista de novas categorias.
        /// </summary>
        /// <param name="novosProdutosDto">Lista de DTOs dass categorias a serem adicionados.</param>
        /// <returns>Lista das categorias adicionados.</returns>
        /// <response code="400">Se a lista de categorias for nula ou vazia.</response>
        /// <response code="500">Se ocorrer um erro ao adicionar as categorias.</response>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoriasDto>>> AddCategoria([FromBody] List<CategoriasDto> novasCategorias)
        {
            if (novasCategorias == null || novasCategorias.Count == 0)
            {
                return BadRequest("A lista de categorias não pode estar vazia."); // Status 400
            }

            try
            {
                var produtosAdicionados = await _categoriaRepository.AddCategoria(novasCategorias);
                return Ok(novasCategorias); // Status 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar novas categorias."); // Log do erro
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }
        #endregion

        #region Atualizar infomações na categoria
        /// <summary>
        /// Atualiza uma categoria existente no banco de dados.
        /// </summary>
        /// <param name="id">ID da categoria a ser atualizado.</param>
        /// <param name="produtoDtos">DTO contendo as novas informações da categoria.</param>
        /// <returns>DTO da categoria atualizado.</returns>
        /// <exception cref="ArgumentException">Lançado quando o ID da categoria é inválido ou a categoria não é encontrado.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao atualizar a categoria.</exception>
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriasDto?>> UpdateCategoria(Guid id, CategoriasDto categoriasDto)
        {
            if (categoriasDto == null || id != categoriasDto.Id)
            {
                return BadRequest(); // Status 400
            }

            var editProduto = await _categoriaRepository.UpdateCategoria(id, categoriasDto);

            if (editProduto == null)
            {
                return NotFound(); // Status 404
            }

            return Ok(editProduto); // Status 200
        }
        #endregion

        #region Exclui categorias.
        /// <summary>
        /// Exclui uma lista de categorias pelo ID.
        /// </summary>
        /// <param name="id">Lista de IDs das categorias a serem excluídos.</param>
        /// <returns>Lista de DTOs das categorias excluídos.</returns>
        /// <exception cref="ArgumentException">Lançado quando nenhuma categoria é encontrado para exclusão.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao excluir as categorias.</exception>
        [HttpGet("delete")]
        public async Task<ActionResult<CategoriasDto?>> DeleteCategoria(List<Guid> id)
        {
            try
            {
                var categoriasDeletado = await _categoriaRepository.DeleteCategoria(id);
                if (categoriasDeletado == null)
                {
                    return NotFound(); // Status 404
                }

                return Ok(categoriasDeletado); // Status 200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor"); // Status 500
            }
        }
        #endregion

        #region Obtem dados de uma categoria atraves do id.
        /// <summary>
        /// Obtém a categoria específica pelo ID.
        /// </summary>
        /// <param name="id">ID da categoria.</param>
        /// <returns>DTO da categoria.</returns>
        /// <response code="404">Se a categoria não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro ao obter a categoria.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriasDto>> GetItemCategoria(Guid id)
        {
            try
            {
                var categoria = await _categoriaRepository.GetItemCategoria(id);
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

        #region Obtem todas as categorias.
        /// <summary>
        /// Obtém todas as categorias.
        /// </summary>
        /// <returns>Lista de DTOs das categorias.</returns>
        /// <response code="404">Se nenhuma categoria for encontrado.</response>
        /// <response code="500">Se ocorrer um erro ao obter as categorias.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriasDto>>> GetAllProdutos()
        {
            try
            {
                var produtos = await _categoriaRepository.GetIAllCategoria();
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
