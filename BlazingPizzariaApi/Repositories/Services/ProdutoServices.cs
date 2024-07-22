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

        public async Task<IEnumerable<ProdutoDtos?>> AddProduto(List<ProdutoDtos> novosProdutosDto)
        {
            if (novosProdutosDto == null || novosProdutosDto.Count == 0)
            {
                throw new ArgumentException("A lista de produtos não pode estar vazia.");
            }

            var produtos = novosProdutosDto.Select(dto => _injectServicesApi._mapper.Map<Produto>(dto)).ToList();

            try
            {
                await _injectServicesApi._dbContext.Produtos.AddRangeAsync(produtos);
                await _injectServicesApi._dbContext.SaveChangesAsync();

                return novosProdutosDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possivel adicionar os produtos.", ex);
            }

        }
        public async Task<ProdutoDtos?> EditProduto(int id, ProdutoDtos produtoDtos)
        {
            if (id == 0)
            {
                throw new ArgumentException("Código do produto inexistente.");
            }

            try
            {
                var editProduto = await _injectServicesApi._dbContext.Produtos
                                    .Include(d => d.Dimensoes)
                                    .Include(a => a.Avaliacao)
                                             .ThenInclude(r => r.Revisao)
                                    .Include(a => a.Atributos)
                                    .Include(d => d.Disponibilidades)
                                    .Include(a => a.Imagem)
                                    .SingleOrDefaultAsync(c => c.Id == id);
                var editProdutoDto = _injectServicesApi._mapper.Map<ProdutoDtos>(editProduto);
                if (editProduto == null || editProduto.Id == 0)
                {
                    throw new ArgumentException("Produtos inexistente.");
                }
               

                _injectServicesApi._mapper.Map(produtoDtos, editProduto);
                await _injectServicesApi._dbContext.SaveChangesAsync();

                return _injectServicesApi._mapper.Map<ProdutoDtos>(editProduto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<ProdutoDtos?>> GetItensProdutoCategoria(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Código de categoria inexistente.");
            }

            try
            {
                var categoria = await _injectServicesApi._dbContext.Produtos
                                      .Include(d => d.Dimensoes)
                                      .Include(a => a.Avaliacao)
                                               .ThenInclude(r => r.Revisao)
                                      .Include(a => a.Atributos)
                                      .Include(d => d.Disponibilidades)
                                      .Include(a => a.Imagem)
                                      .Where(c => c.CategoriaId == id).ToListAsync();
                if (categoria == null || categoria.Count == 0)
                {
                    throw new ArgumentException("Lista de produto por categoria inexistente.");
                }

                var categoriaDtos = _injectServicesApi._mapper.Map<List<ProdutoDtos>>(categoria);

                return categoriaDtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possivel localizar categoria.", ex);
            }
        }

        public async Task<IEnumerable<ProdutoDtos?>> GetItens()
        {
            try
            {
                var produtos = await _injectServicesApi._dbContext.Produtos
                                    .Include(d => d.Dimensoes)
                                    .Include(a => a.Avaliacao)
                                             .ThenInclude(r => r.Revisao)
                                    .Include(a => a.Atributos)
                                    .Include(d => d.Disponibilidades)
                                    .Include(a => a.Imagem).ToListAsync();
                if (produtos == null || produtos.Count == 0)
                {
                    throw new ArgumentException("Produtos inexistente.");
                }
                var produtosDtos = _injectServicesApi._mapper.Map<List<ProdutoDtos>>(produtos);
                return produtosDtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possivel localizar lista de produtos.", ex);
            }
        }
        public async Task<ProdutoDtos?> GetItem(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Codigo do produto inexistente.");
            }

            try
            {
                var produto = await _injectServicesApi._dbContext.Produtos
                          .Include(d => d.Dimensoes)
                          .Include(a => a.Avaliacao)
                                   .ThenInclude(r => r.Revisao)
                          .Include(a => a.Atributos)
                          .Include(d => d.Disponibilidades)
                          .Include(a => a.Imagem)
                          .SingleOrDefaultAsync(c => c.Id == id);
                if (produto == null || produto.Id == 0)
                {
                    throw new ArgumentException("Produtos inexistente.");
                }
                var produtosDtos = _injectServicesApi._mapper.Map<ProdutoDtos>(produto);
                return produtosDtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possivel localizar o produto através do código.", ex);
            }
        }

        public async Task<IEnumerable<ProdutoDtos?>> DeleteProduto(List<int> id)
        {
            var produtos = await _injectServicesApi._dbContext.Produtos
                          .Where(p => id.Contains(p.Id))
                          .ToListAsync();

            if (produtos.Count == 0)
            {
                throw new ArgumentException("Não foi possível excluir produtos.");
            }

            _injectServicesApi._dbContext.Produtos.RemoveRange(produtos);
            await _injectServicesApi._dbContext.SaveChangesAsync();

            var produtosDtos = _injectServicesApi._mapper.Map<IEnumerable<ProdutoDtos?>>(produtos);

            return produtosDtos;
        }
    }
}

