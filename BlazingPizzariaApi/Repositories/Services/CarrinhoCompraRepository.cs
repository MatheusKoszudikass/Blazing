using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Models.DTOs;
using BlazingPizzaria.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BlazingPizza.Api.Repositories.Services
{
    public class CarrinhoCompraRepository(InjectServicesApi injectServicesApi) : ICarrinhoCompraRepository
    {
        private readonly InjectServicesApi _injectServicesApi = injectServicesApi;

        /// <summary>
        /// Verifica se um item já existe no carrinho de compras.
        /// </summary>
        /// <param name="carrinhoId">ID do carrinho de compras.</param>
        /// <param name="produtoId">ID do produto.</param>
        /// <returns>True se o item já existir, caso contrário, False.</returns>
        private async Task<bool> CarrinhoItemJaExiste(Guid carrinhoId, Guid produtoId)
        {
            try
            {
                return await _injectServicesApi._dbContext.CarrinhoDeItem.AnyAsync(c => c.CarrinhoDeCompraId == carrinhoId && c.ProdutoId == produtoId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao verificar a existência do item no carrinho.", ex);
            }
        }

        /// <summary>
        /// Adiciona um novo item ao carrinho de compras.
        /// </summary>
        /// <param name="carrinhoDeCompraAddDtos">Dados do item a ser adicionado ao carrinho.</param>
        /// <returns>Item adicionado ao carrinho, ou null se o item já existir.</returns>
        public async Task<CarrinhoDeItem?> AddCarrinhoCompra(CarrinhoItemAddDto carrinhoDeCompraAddDtos)
        {
            try
            {
                if (await CarrinhoItemJaExiste(carrinhoDeCompraAddDtos.CarrinhoId, carrinhoDeCompraAddDtos.ProdutoId))
                {
                    return null; // Item já existe no carrinho.
                }

                var item = await (from produto in _injectServicesApi._dbContext.Produto
                                  where produto.Id == carrinhoDeCompraAddDtos.ProdutoId
                                  select new CarrinhoDeItem
                                  {
                                      CarrinhoDeCompraId = carrinhoDeCompraAddDtos.CarrinhoId,
                                      ProdutoId = produto.Id,
                                      Quantidade = carrinhoDeCompraAddDtos.Quantidade
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    try
                    {
                        var resultado = await _injectServicesApi._dbContext.CarrinhoDeItem.AddAsync(item);
                        await _injectServicesApi._dbContext.SaveChangesAsync();
                        return resultado.Entity;
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Erro ao adicionar o item ao carrinho.", ex);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao adicionar o item ao carrinho.", ex);
            }
        }

        /// <summary>
        /// Atualiza a quantidade de um item no carrinho de compras.
        /// </summary>
        /// <param name="id">ID do item no carrinho.</param>
        /// <param name="carrinhoDeItemAtualizarQuantidadeDto">Dados para atualizar a quantidade do item.</param>
        /// <returns>Item atualizado no carrinho, ou null se não encontrado.</returns>
        public async Task<CarrinhoDeItem?> UpdateItemQuantity(Guid id, CarrinhoDeItemAtualizarQuantidadeDto carrinhoDeItemAtualizarQuantidadeDto)
        {
            try
            {
                if (carrinhoDeItemAtualizarQuantidadeDto.Quantidade < 0)
                {
                    throw new ArgumentException("A quantidade não pode ser negativa.");
                }

                var carrinhoItem = await _injectServicesApi._dbContext.CarrinhoDeItem.FindAsync(id);

                if (carrinhoItem != null)
                {
                    carrinhoItem.Quantidade = carrinhoDeItemAtualizarQuantidadeDto.Quantidade;
                    await _injectServicesApi._dbContext.SaveChangesAsync();
                    return carrinhoItem;
                }
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException("Quantidade inválida para o item do carrinho.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao atualizar a quantidade do item no carrinho.", ex);
            }
        }

        /// <summary>
        /// Obtém um item específico do carrinho de compras.
        /// </summary>
        /// <param name="id">ID do item.</param>
        /// <returns>Item do carrinho, ou null se não encontrado.</returns>
        public async Task<CarrinhoDeItem?> GetItem(Guid id)
        {
            try
            {
                return await (from carrinhoDeCompra in _injectServicesApi._dbContext.CarrinhoDeCompra
                              join carrinhoDeItems in _injectServicesApi._dbContext.CarrinhoDeItem
                              on carrinhoDeCompra.Id equals carrinhoDeItems.CarrinhoDeCompraId
                              join produtos in _injectServicesApi._dbContext.Produto
                              on carrinhoDeItems.ProdutoId equals produtos.Id
                              where carrinhoDeItems.Id == id
                              select new CarrinhoDeItem
                              {
                                  Id = carrinhoDeItems.Id,
                                  ProdutoId = carrinhoDeItems.ProdutoId,
                                  Produto = produtos,
                                  Quantidade = carrinhoDeItems.Quantidade,
                                  CarrinhoDeCompraId = carrinhoDeItems.CarrinhoDeCompraId
                              }).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter o item do carrinho.", ex);
            }
        }

        /// <summary>
        /// Obtém todos os itens do carrinho de compras para um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de itens do carrinho de compras.</returns>
        public async Task<IEnumerable<CarrinhoDeCompra?>> GetItens(Guid usuarioId)
        {
            try
            {
                return await _injectServicesApi._dbContext.CarrinhoDeCompra
                              .Where(uid => uid.UsuarioId == usuarioId)
                              .Include(u => u.Usuario)
                              .Include(i => i.Items!)
                                  .ThenInclude(p => p.Produto)
                                      .ThenInclude(d => d.Dimensoes)
                              .Include(i => i.Items)
                                  .ThenInclude(p => p.Produto)
                                      .ThenInclude(a => a.Avaliacao)
                                          .ThenInclude(r => r.Revisao)
                              .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter os itens do carrinho.", ex);
            }
        }

        /// <summary>
        /// Remove um item do carrinho de compras.
        /// </summary>
        /// <param name="id">ID do item a ser removido.</param>
        /// <returns>Item removido do carrinho, ou null se não encontrado.</returns>
        public async Task<CarrinhoDeItem?> DeleteItem(Guid id)
        {
            try
            {
                var item = await _injectServicesApi._dbContext.CarrinhoDeItem.FindAsync(id);

                if (item != null)
                {
                    _injectServicesApi._dbContext.CarrinhoDeItem.Remove(item);
                    await _injectServicesApi._dbContext.SaveChangesAsync();
                    return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao remover o item do carrinho.", ex);
            }
        }
    }

}

