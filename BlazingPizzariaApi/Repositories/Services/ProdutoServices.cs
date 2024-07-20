using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizzaria.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BlazingPizza.Api.Repositories.Services
{
    public class ProdutoServices : IProdutoRepository
    {
        private readonly InjectServicesApi _injectServicesApi;
        public ProdutoServices(InjectServicesApi services)
        {
            _injectServicesApi = services;
        }

        public async Task<IEnumerable<Produto?>> AddProduto(List<ProdutoDtos> novosProdutosDto)
        {
            if (novosProdutosDto == null || !novosProdutosDto.Any())
            {
                throw new ArgumentException("A lista de produtos não pode estar vazia.");
            }

            var produtos = novosProdutosDto.Select(dto => _injectServicesApi._mapper.Map<Produto>(novosProdutosDto));

            try
            {
                await _injectServicesApi._dbContext.Produtos.AddRangeAsync(produtos);
                await _injectServicesApi._dbContext.SaveChangesAsync();

                return produtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possivel adicionar os produtos.", ex);
            }

        }

        public async Task<IEnumerable<Categoria?>> GetItensPorCategorias(int id)
        {
            var produto = await _injectServicesApi._dbContext.Categorias
                            .Include(p => p.Produtos)
                                  .ThenInclude(p => p.Dimensoes)
                            .Include(p => p.Produtos)
                                      .ThenInclude(p => p.Avaliacao)
                                                    .ThenInclude(a => a.Revisao)
                            .Include(p => p.Produtos)
                                   .ThenInclude(p => p.Atributos)
                            .Include(p => p.Produtos)
                                  .ThenInclude(p => p.Imagem)
                            .Where(c => c.Id == id).ToListAsync();
            return produto;
        }

        public async Task<IEnumerable<Produto?>> GetItens()
        {
            var produtosComDetalhes = await _injectServicesApi._dbContext.Produtos
                            .Include(p => p.Dimensoes)
                            .Include(p => p.Avaliacao)
                                      .ThenInclude(p => p.Revisao)
                            .Include(p => p.Atributos)
                            .Include(a => a.Imagem).ToListAsync();

            return produtosComDetalhes;
        }
        public async Task<Produto?> GetItem(int id)
        {
            var produto = await _injectServicesApi._dbContext.Produtos
                            .Include(p => p.Dimensoes)
                            .Include(p => p.Avaliacao)
                                      .ThenInclude(p => p.Revisao)
                            .Include(p => p.Atributos)
                            .Include(a => a.Imagem).SingleOrDefaultAsync(c => c.Id == id);

            return produto;

        }
    }
}

