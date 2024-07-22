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

        public async Task<IEnumerable<CategoriasDtos?>> AddCategoria(List<CategoriasDtos> categoriasDtos)
        {
            if (categoriasDtos == null || categoriasDtos.Count == 0)
            {
                throw new ArgumentException("Lista de categoria está vazia.");
            }

            var categoria = categoriasDtos.Select(dto => _injectServicesApi._mapper.Map<Categoria>(dto)).ToList();

            try
            {
                await _injectServicesApi._dbContext.Categorias.AddRangeAsync(categoria);
                await _injectServicesApi._dbContext.SaveChangesAsync();

                return categoriasDtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possivel cria categorias", ex);
            }
        }

        public async Task<IEnumerable<CategoriasDtos?>> GetItensPorCategorias(int id)
        {
            var categorias = await _injectServicesApi._dbContext.Categorias
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
                           .Where(c => c.Id == id).ToListAsync();

            var categoriasDtos =  _injectServicesApi._mapper.Map<IEnumerable<CategoriasDtos>>(categorias);
            return categoriasDtos;

        }
    }
}
