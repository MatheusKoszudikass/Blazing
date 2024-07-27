using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizzaria.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace BlazingPizza.Api.Repositories.Services
{
    public class ProdutoServices(InjectServicesApi services) : IProdutoRepository
    {
        private readonly InjectServicesApi _injectServicesApi = services;

        /// <summary>
        /// Adiciona uma lista de novos produtos ao banco de dados.
        /// </summary>
        /// <param name="novosProdutosDto">Lista de DTOs de produtos a serem adicionados.</param>
        /// <returns>Lista de DTOs dos produtos adicionados.</returns>
        /// <exception cref="ArgumentException">Lançado quando a lista de produtos está vazia.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao adicionar os produtos.</exception>
        public async Task<IEnumerable<ProdutoDto?>> AddProdutos(List<ProdutoDto> novosProdutosDto)
        {
            if (novosProdutosDto == null || novosProdutosDto.Count == 0)
            {
                throw new ArgumentException("A lista de produtos não pode estar vazia.");
            }

            var produtos = novosProdutosDto.Select(dto => _injectServicesApi._mapper.Map<Produto>(dto)).ToList();

            try
            {
                await _injectServicesApi._dbContext.Produto.AddRangeAsync(produtos);
                await _injectServicesApi._dbContext.SaveChangesAsync();
                return novosProdutosDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível adicionar os produtos.", ex);
            }
        }

        /// <summary>
        /// Atualiza um produto existente no banco de dados.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="produtoDtos">DTO contendo as novas informações do produto.</param>
        /// <returns>DTO do produto atualizado.</returns>
        /// <exception cref="ArgumentException">Lançado quando o ID do produto é inválido ou o produto não é encontrado.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao atualizar o produto.</exception>
        public async Task<ProdutoDto?> UpdateProduto(Guid id, ProdutoDto produtoDtos)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Código do produto inexistente.");
            }

            try
            {
                var editProduto = await _injectServicesApi._dbContext.Produto
                    .Include(d => d.Dimensoes)
                    .Include(a => a.Avaliacao)
                        .ThenInclude(r => r.Revisao)
                    .Include(a => a.Atributos)
                    .Include(d => d.Disponibilidades)
                    .Include(a => a.Imagem)
                    .SingleOrDefaultAsync(c => c.Id == id);

                if (editProduto == null || editProduto.Id == Guid.Empty)
                {
                    throw new ArgumentException("Produto inexistente.");
                }

                _injectServicesApi._mapper.Map(produtoDtos, editProduto);
                await _injectServicesApi._dbContext.SaveChangesAsync();

                return _injectServicesApi._mapper.Map<ProdutoDto>(editProduto);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível atualizar o produto.", ex);
            }
        }

        /// <summary>
        /// Obtém todos os produtos de uma categoria específica.
        /// </summary>
        /// <param name="id">ID da categoria.</param>
        /// <returns>Lista de DTOs dos produtos da categoria.</returns>
        /// <exception cref="ArgumentException">Lançado quando o ID da categoria é inválido ou não há produtos na categoria.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao obter os produtos.</exception>
        public async Task<IEnumerable<ProdutoDto?>> GetProdutosByCategoriaId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Código de categoria inexistente.");
            }

            try
            {
                var categoria = await _injectServicesApi._dbContext.Produto
                    .Include(d => d.Dimensoes)
                    .Include(a => a.Avaliacao)
                        .ThenInclude(r => r.Revisao)
                    .Include(a => a.Atributos)
                    .Include(d => d.Disponibilidades)
                    .Include(a => a.Imagem)
                    .Where(c => c.CategoriaId == id).ToListAsync();

                if (categoria == null || categoria.Count == 0)
                {
                    throw new ArgumentException("Lista de produtos por categoria inexistente.");
                }

                return _injectServicesApi._mapper.Map<List<ProdutoDto>>(categoria);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível localizar produtos da categoria.", ex);
            }
        }


        /// <summary>
        /// Exclui uma lista de produtos pelo ID.
        /// </summary>
        /// <param name="id">Lista de IDs dos produtos a serem excluídos.</param>
        /// <returns>Lista de DTOs dos produtos excluídos.</returns>
        /// <exception cref="ArgumentException">Lançado quando nenhum produto é encontrado para exclusão.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao excluir os produtos.</exception>
        public async Task<IEnumerable<ProdutoDto?>> DeleteProdutos(List<Guid> id)
        {
            if (id.Count == 0)
            {
                throw new ArgumentException("Lista de id inválido");
            }

            try
            {
                var produtos = await _injectServicesApi._dbContext.Produto
                .Where(p => id.Contains(p.Id))
                .ToListAsync() ?? throw new ArgumentException("Não foi possível encontrar produtos para exclusão.");


                _injectServicesApi._dbContext.Produto.RemoveRange(produtos);
                await _injectServicesApi._dbContext.SaveChangesAsync();

                return _injectServicesApi._mapper.Map<IEnumerable<ProdutoDto?>>(produtos);
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Obtém um produto específico pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>DTO do produto.</returns>
        /// <exception cref="ArgumentException">Lançado quando o ID do produto é inválido ou o produto não é encontrado.</exception>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao obter o produto.</exception>
        public async Task<ProdutoDto?> GetProdutoById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Código do produto inexistente.");
            }

            try
            {
                var produto = await _injectServicesApi._dbContext.Produto
                    .Include(d => d.Dimensoes)
                    .Include(a => a.Avaliacao)
                        .ThenInclude(r => r.Revisao)
                    .Include(a => a.Atributos)
                    .Include(d => d.Disponibilidades)
                    .Include(a => a.Imagem)
                    .SingleOrDefaultAsync(c => c.Id == id);

                if (produto == null || produto.Id == Guid.Empty)
                {
                    throw new ArgumentException("Produto inexistente.");
                }

                return _injectServicesApi._mapper.Map<ProdutoDto>(produto);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível localizar o produto pelo código.", ex);
            }
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        /// <returns>Lista de DTOs de todos os produtos.</returns>
        /// <exception cref="InvalidOperationException">Lançado quando ocorre um erro ao obter os produtos.</exception>
        public async Task<IEnumerable<ProdutoDto?>> GetAllProdutos()
        {
            try
            {
                var produtos = await _injectServicesApi._dbContext.Produto
                    .Include(d => d.Dimensoes)
                    .Include(a => a.Avaliacao)
                        .ThenInclude(r => r.Revisao)
                    .Include(a => a.Atributos)
                    .Include(d => d.Disponibilidades)
                    .Include(a => a.Imagem).ToListAsync();

                if (produtos == null || produtos.Count == 0)
                {
                    throw new ArgumentException("Produtos inexistentes.");
                }

                return _injectServicesApi._mapper.Map<List<ProdutoDto>>(produtos);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível localizar a lista de produtos.", ex);
            }
        }
    }
}

