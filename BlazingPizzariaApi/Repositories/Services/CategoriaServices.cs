using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizzaria.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Api.Repositories.Services
{
    public class CategoriaServices(InjectServicesApi injectServicesApi) : ICategoriaRepository
    {
        private readonly InjectServicesApi _injectServicesApi = injectServicesApi;


        /// <summary>
        /// Adiciona uma lista de novas categorias ao banco de dados.
        /// </summary>
        /// <param name="novasCategoriasDto">Lista de DTOs de categorias a serem adicionados.</param>
        /// <returns>Lista de DTOs das categorias adicionados.</returns>
        /// <exception cref="ArgumentException">Lançado quando a lista de categoria está vazia.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao adicionar as categorias.</exception>
        public async Task<IEnumerable<CategoriasDto?>> AddCategoria(List<CategoriasDto> novasCategoriasDto)
        {
            if (novasCategoriasDto == null || novasCategoriasDto.Count == 0)
            {
                throw new ArgumentException("Lista de categoria está vazia.");
            }

            var categoria = novasCategoriasDto.Select(dto => _injectServicesApi._mapper.Map<Categoria>(dto)).ToList();

            try
            {
                await _injectServicesApi._dbContext.Categoria.AddRangeAsync(categoria);
                await _injectServicesApi._dbContext.SaveChangesAsync();

                return novasCategoriasDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possivel cria categorias", ex);
            }
        }

        /// <summary>
        /// Atualiza uma categoria existente no banco de dados.
        /// </summary>
        /// <param name="id">ID da categoria a ser atualizado.</param>
        /// <param name="categoriaDto">DTO contendo as novas informações da categoria.</param>
        /// <returns>DTO da categoria atualizado.</returns>
        /// <exception cref="ArgumentException">Lançado quando o ID da categoria é inválido ou a categoria não é encontrado.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao atualizar o produto.</exception>
        public async Task<CategoriasDto?> UpdateCategoria(Guid id, CategoriasDto updateCategoriasDto)
        {
            if (updateCategoriasDto == null || updateCategoriasDto.Id == Guid.Empty)
            {
                throw new ArgumentException("A categoria não existe.");
            }

            try
            {
                var categoria = await _injectServicesApi._dbContext.Categoria
                                      .Include(p => p.Produtos)
                                      .SingleOrDefaultAsync(c => c.Id == id);

                if (categoria == null || categoria.Id == Guid.Empty)
                {
                    throw new ArgumentException("A consulta retornou uma categoria inexistente.");
                }
                categoria.Nome = updateCategoriasDto?.Nome;

                await _injectServicesApi._dbContext.SaveChangesAsync();

                return _injectServicesApi._mapper.Map<CategoriasDto>(categoria);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Não foi possível atualizar a categoria.", ex);
            }

        }

        /// <summary>
        /// Exclui uma lista de categoria pelo ID.
        /// </summary>
        /// <param name="id">Lista de IDs das categorias a serem excluídos.</param>
        /// <returns>Lista de DTOs dos produtos excluídos.</returns>
        /// <exception cref="ArgumentException">Lançado quando nenhum produto é encontrado para exclusão.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao excluir os produtos.</exception>
        public async Task<IEnumerable<CategoriasDto?>> DeleteCategoria(List<Guid> id)
        {
            if (id.Count == 0)
            {
                throw new ArgumentException("Lista de id inválido");
            }

            try
            {
                var categorias = await _injectServicesApi._dbContext.Categoria
                                       .Where(p => id.Contains(p.Id))
                                       .ToListAsync() ?? throw new ArgumentException("Cetegorias inexistentes.");

                _injectServicesApi._dbContext.Categoria.RemoveRange(categorias);
                await _injectServicesApi._dbContext.SaveChangesAsync();

                return _injectServicesApi._mapper.Map<List<CategoriasDto>>(categorias);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível localizar a lista de categorias.", ex);
            }
        }

        /// <summary>
        /// Obtém  categoria específica.
        /// </summary>
        /// <param name="id">ID da categoria.</param>
        /// <returns>Lista de DTOs da categoria.</returns>
        /// <exception cref="ArgumentException">Lançado quando o ID da categoria é inválido.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao obter os produtos.</exception>
        public async Task<CategoriasDto?> GetItemCategoria(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Código de categoria inexistente.");
            }
            try
            {
                var categorias = await _injectServicesApi._dbContext.Categoria
                                        .Include(p => p.Produtos)
                                              .ThenInclude(d => d.Dimensoes)
                                        .Include(p => p.Produtos)
                                                  .ThenInclude(a => a.Avaliacao)
                                                                .ThenInclude(r => r.Revisao)
                                        .Include(p => p.Produtos)
                                               .ThenInclude(a => a.Atributos)
                                        .Include(p => p.Produtos)
                                               .ThenInclude(d => d.Disponibilidades)
                                        .Include(p => p.Produtos)
                                              .ThenInclude(p => p.Imagem)
                                        .Where(c => c.Id == id).SingleOrDefaultAsync();
                if (categorias == null || categorias.Id == Guid.Empty)
                {
                    throw new ArgumentException("Lista de categoria inexistente.");
                }
 
                return _injectServicesApi._mapper.Map<CategoriasDto>(categorias);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Não foi possível localizar produtos da categoria.", ex);
            }
        }


        /// <summary>
        /// Obtém todos os catergorias.
        /// </summary>
        /// <returns>Lista de DTOs de todas categorias.</returns>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao obter as categorias.</exception>
        public async Task<IEnumerable<CategoriasDto?>> GetIAllCategoria()
        {
            try
            {
                var produtos = await _injectServicesApi._dbContext.Categoria
                                .Include(p => p.Produtos)
                                         .ThenInclude(d => d.Dimensoes)
                                .Include(p => p.Produtos)
                                         .ThenInclude(a => a.Avaliacao)
                                                      .ThenInclude(r => r.Revisao)
                                .Include(p => p.Produtos)
                                       .ThenInclude(a => a.Atributos)
                                .Include(p => p.Produtos)
                                       .ThenInclude(d => d.Disponibilidades)
                                .Include(p => p.Produtos)
                                      .ThenInclude(p => p.Imagem)
                                .ToListAsync();


                if (produtos == null || produtos.Count == 0)
                {
                    throw new ArgumentException("Categoria inexistentes.");
                }

                return _injectServicesApi._mapper.Map<List<CategoriasDto>>(produtos);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível localizar a lista de categorias.", ex);
            }

        }
    }
}
