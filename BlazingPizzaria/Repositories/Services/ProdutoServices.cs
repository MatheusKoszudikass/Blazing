using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Dependencias;
using BlazingPizza.Repositories.Interface;
using BlazingPizzaria.Models.DTOs;
using System.Net;

namespace BlazingPizza.Repositories.Services
{
    public class ProdutoServices : IProdutoServices
    {
        private readonly InjectServicesWeb _injectServicesWeb;
        public ProdutoServices(InjectServicesWeb InjectServicesWeb)
        {
            _injectServicesWeb = InjectServicesWeb;
        }

        public async Task<IEnumerable<ProdutoDtos>> GetItens()
        {
            try
            {
                var produtosDto = await _injectServicesWeb._httpClient.
                    GetFromJsonAsync<IEnumerable<ProdutoDtos>>("api/Produtos/GetItens");

                return produtosDto;
            }
            catch (Exception)
            {
                _injectServicesWeb._logger.LogError("Erro ao acessar os produtos : api/Produtos");
                throw;
            }
        }

        public async Task<ProdutoDtos> GetItem(int Id)
        {
            try
            {
                var produtoDtos = await _injectServicesWeb._httpClient.GetAsync($"api/Produtos/GetItem/{Id}");

                if (produtoDtos.IsSuccessStatusCode)
                {
                    if (produtoDtos.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(ProdutoDtos);
                    }
                    return await produtoDtos.Content.ReadFromJsonAsync<ProdutoDtos>();
                }
                else
                {
                    var message = await produtoDtos.Content.ReadAsStringAsync();
                    _injectServicesWeb._logger.LogError($"Erro ao obter o produto pelo código {Id} - {message}");

                    throw new Exception($"Status code : {produtoDtos.StatusCode} - {message}");
                }
            }
            catch (Exception)
            {
                _injectServicesWeb._logger.LogError($"Erro a obter produto pelo código {Id}");
                throw;
            }
        }

        public async Task<IEnumerable<ProdutoDtos>> GetItensPorCategoria(int CategoriaId)
        {
            try
            {
                var ProdutoCategoriasDtos = await _injectServicesWeb._httpClient.
                     GetAsync($"api/Produto/GetItensPorCategorias/{CategoriaId}");
                if (ProdutoCategoriasDtos.IsSuccessStatusCode)
                {
                    if (ProdutoCategoriasDtos.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProdutoDtos>();
                    }

                    return await ProdutoCategoriasDtos.Content.ReadFromJsonAsync<IEnumerable<ProdutoDtos>>();
                }
                else
                {
                    var message = await ProdutoCategoriasDtos.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {ProdutoCategoriasDtos.StatusCode} Message - {message}");
                }

            }
            catch (Exception)
            {
                _injectServicesWeb._logger.LogError("Não foi encontrado a categoria");
                throw;
            }
        }

    }
}
