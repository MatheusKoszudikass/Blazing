using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Models.DTOs;
using BlazingPizzaria.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BlazingPizza.Api.Repositories.Services
{
    public class CarrinhoCompraRepository : ICarrinhoCompraRepository
    {
        private readonly InjectServicesApi _injectServicesApi;

        public CarrinhoCompraRepository(InjectServicesApi injectServicesApi)
        {
            _injectServicesApi = injectServicesApi;
        }

        private async Task<bool> CarrinhoItemJaExiste(int carrinhoId, int produtoId)
        {
            return await _injectServicesApi._dbContext.CarrinhoDeItems.AnyAsync(c => c.Id == carrinhoId
                                                                                     && c.CarrinhoDeCompraId == produtoId);
        }

        public async Task<CarrinhoDeItems?> AddItem(CarrinhoItemAddDtos carrinhoDeCompraAddDtos)
        {
            if (await CarrinhoItemJaExiste(carrinhoDeCompraAddDtos.CarrinhoId, carrinhoDeCompraAddDtos.ProdutoId) == false)
            {
                var item = await (from produto in _injectServicesApi._dbContext.Produtos
                                  where produto.Id == carrinhoDeCompraAddDtos.ProdutoId
                                  select new CarrinhoDeItems
                                  { 
                                      CarrinhoDeCompraId = carrinhoDeCompraAddDtos.CarrinhoId,
                                      ProdutoId = produto.Id,
                                      Quantidade = carrinhoDeCompraAddDtos.Quantidade
                                  }).SingleOrDefaultAsync();

                if (item is not null)
                {
                    var resultado = await _injectServicesApi._dbContext.CarrinhoDeItems.AddAsync(item);
                    await _injectServicesApi._dbContext.SaveChangesAsync();
                    return resultado.Entity;
                }

            }
            return null;
        }

        //Atualizar quantidade dos produtos no carrinho.
        public async Task<CarrinhoDeItems?> AddItensQuantidade(int id, CarrinhoDeItemAtualizarQuantidadeDto carrinhoDeItemAtualizarQuantidadeDto)
        {
            if (carrinhoDeItemAtualizarQuantidadeDto.Quantidade < 0)
            {
                throw new ArgumentException("A quantidade não pode ser negativa.");
            }

            try
            {
                // Encontra o item no carrinho pelo ID
                var carrinhoItem = await _injectServicesApi._dbContext.CarrinhoDeItems.FindAsync(id);

                if (carrinhoItem is not null)
                {
                    // Atualiza a quantidade do item
                    carrinhoItem.Quantidade = carrinhoDeItemAtualizarQuantidadeDto.Quantidade;
                    await _injectServicesApi._dbContext.SaveChangesAsync();
                    return carrinhoItem;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível atualizar o item no carrinho.", ex);
            }
        }

        public async Task<CarrinhoDeItems?> DeletItem(int id)
        {
            var item = await _injectServicesApi._dbContext.CarrinhoDeItems.FindAsync(id);

            if (item is not null)
            {
                _injectServicesApi._dbContext.CarrinhoDeItems.Remove(item);
                await _injectServicesApi._dbContext.SaveChangesAsync();
            }
            return item;
        }
        public async Task<CarrinhoDeItems?> GetItem(int id)
        {
            return await (from carrinhoDeCompra in _injectServicesApi._dbContext.CarrinhoDeCompra
                          join CarrinhoDeItems in _injectServicesApi._dbContext.CarrinhoDeItems
                          on carrinhoDeCompra.Id equals CarrinhoDeItems.CarrinhoDeCompraId
                          join produtos in _injectServicesApi._dbContext.Produtos
                          on CarrinhoDeItems.ProdutoId equals produtos.Id
                          where CarrinhoDeItems.Id == id
                          select new CarrinhoDeItems
                          {
                              Id = carrinhoDeCompra.Id,
                              ProdutoId = CarrinhoDeItems.ProdutoId,
                              Produto = produtos,
                              Quantidade = CarrinhoDeItems.Quantidade,
                              CarrinhoDeCompraId = CarrinhoDeItems.CarrinhoDeCompraId
                          }).SingleAsync();


        }
        public async Task<IEnumerable<CarrinhoDeCompra?>> GetItens(int usuarioId)
        {
            var carrinhoDeCompras = await _injectServicesApi._dbContext.CarrinhoDeCompra
                               .Where(uid => uid.UsuarioId == usuarioId)
                               .Include(u => u.Usuario)
                               .Include(i => i.Items!)
                                        .ThenInclude(p => p.Produto)
                                                    .ThenInclude(d => d.Dimensoes)
                               .Include(i1 => i1.Items!)
                                        .ThenInclude(p => p.Produto)
                                                    .ThenInclude(a => a.Avaliacao)
                                                                .ThenInclude(r => r.Revisao)
                               .ToListAsync();

            return carrinhoDeCompras;
        }
    }
}
