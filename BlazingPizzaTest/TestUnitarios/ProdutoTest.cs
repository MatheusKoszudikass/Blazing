using AutoMapper;
using BlazingPizza.Api.Controllers;
using BlazingPizza.Api.Data;
using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Profiles;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Api.Repositories.Services;
using BlazingPizzaria.Models.DTOs;
using BlazingPizzaTest.Data;
using BlazingPizzaTest.Helps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using Moq;
using System.Collections;
using static BlazingPizzaTest.Depedencia.PriorityOrderer;

namespace BlazingPizzaTest.TestUnitarios
{
    [TestCaseOrderer("Namespace.PriorityOrderer", "AssemblyName")]
    public class ProdutoTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ProdutoServices>> _loggerMock;


        public ProdutoTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProdutosProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<ProdutoServices>>();
        }

        [Fact, TestPriority(1)]
        public async Task ProdutoService()
        {

            //Classe de povoamento  de dados 
            PeopleOfData peopleOfData = new PeopleOfData();

            var produtos = peopleOfData.AddProdutos();

            var updateProdutos = peopleOfData.UpdateProdutos();

            var ids = peopleOfData.GetIds();




            await using var DbContext = new MockDb().CreateDbContext();

            var injectServiceApi = new InjectServicesApi(DbContext, _mapper);

            var produtoService = new ProdutoServices(injectServiceApi);



            //Act
            var resultAddProduto = await produtoService.AddProdutos(produtos);

            var resultEditProduto = await produtoService.UpdateProduto(peopleOfData.ProdutoId, updateProdutos);

            var resultGetItem = await produtoService.GetProdutoById(peopleOfData.ProdutoId);

            var resultGetItens = await produtoService.GetAllProdutos();

            var resultCategoriaProdutos = await produtoService.GetProdutosByCategoriaId(peopleOfData.CategoriaId);

            var resultDeleteProduto = await produtoService.DeleteProdutos(ids);

            //Assert
            Assert.True(resultAddProduto.Any());
            Assert.True(resultAddProduto.Any());
            Assert.True(resultGetItens.Any());
            Assert.NotNull(resultEditProduto);
            Assert.NotNull(resultGetItem);
            Assert.True(resultDeleteProduto.Any());

            Assert.Equal(produtos.Count, resultAddProduto.Count());

            foreach (var dto in produtos)
            {
                var produto = resultAddProduto.FirstOrDefault(p => p.Id == dto.Id);
                Assert.NotNull(produto);
                Assert.Equal(dto.Nome, produto.Nome);
                Assert.Equal(dto.Descricao, produto.Descricao);
                Assert.Equal(dto.Preco, produto.Preco);
                Assert.Equal(dto.Moeda, produto.Moeda);
                Assert.Equal(dto.CategoriaId, produto.CategoriaId);
                Assert.Equal(dto.Marca, produto.Marca);
                Assert.Equal(dto.SKU, produto.SKU);
                Assert.Equal(dto.QuantidadeEmEstoque, produto.QuantidadeEmEstoque);
                Assert.Equal(dto.LocalizacaoEstoque, produto.LocalizacaoEstoque);
                Assert.Equal(dto.DimensoesId, produto.DimensoesId);

                Assert.Equal(dto.Dimensoes.Id, produto.Dimensoes.Id);
                Assert.Equal(dto.Dimensoes.Peso, produto.Dimensoes.Peso);
                Assert.Equal(dto.Dimensoes.Altura, produto.Dimensoes.Altura);
                Assert.Equal(dto.Dimensoes.Largura, produto.Dimensoes.Largura);
                Assert.Equal(dto.Dimensoes.Profundidade, produto.Dimensoes.Profundidade);
                Assert.Equal(dto.Dimensoes.Unidade, produto.Dimensoes.Unidade);

                Assert.Equal(dto.AvaliacaoId, produto.AvaliacaoId);

                Assert.Equal(dto.Avaliacao.Id, produto.Avaliacao.Id);
                Assert.Equal(dto.Avaliacao.Media, produto.Avaliacao.Media);
                Assert.Equal(dto.Avaliacao.NumeroDeAvaliacoes, produto.Avaliacao.NumeroDeAvaliacoes);

                Assert.Equal(dto.Avaliacao.RevisaoId, produto.Avaliacao.RevisaoId);

                Assert.Equal(dto.Avaliacao.Revisao.Id, produto.Avaliacao.Revisao.Id);
                Assert.Equal(dto.Avaliacao.Revisao.Usuario, produto.Avaliacao.Revisao.Usuario);
                Assert.Equal(dto.Avaliacao.Revisao.Comentario, produto.Avaliacao.Revisao.Comentario);
                Assert.Equal(dto.Avaliacao.Revisao.Data, produto.Avaliacao.Revisao.Data);

                Assert.Equal(dto.AtributosId, produto.AtributosId);

                Assert.Equal(dto.Atributos.Id, produto.Atributos.Id);
                Assert.Equal(dto.Atributos.Cor, produto.Atributos.Cor);
                Assert.Equal(dto.Atributos.Material, produto.Atributos.Material);
                Assert.Equal(dto.Atributos.Modelo, produto.Atributos.Modelo);

                Assert.Equal(dto.DisponibilidadeId, produto.DisponibilidadeId);

                Assert.Equal(dto.Disponibilidades.Id, produto.Disponibilidades.Id);
                Assert.Equal(dto.Disponibilidades.EstaDisponivel, produto.Disponibilidades.EstaDisponivel);
                Assert.Equal(dto.Disponibilidades.DataEstimadaDeEntrega, produto.Disponibilidades.DataEstimadaDeEntrega);


                Assert.Equal(dto.ImagemId, produto.ImagemId);

                Assert.Equal(dto.Imagem.Id, produto.Imagem.Id);
                Assert.Equal(dto.Imagem.Url, produto.Imagem.Url);
                Assert.Equal(dto.Imagem.TextoAlternativo, produto.Imagem.TextoAlternativo);
            }
        }
        [Fact]
        public async Task AddProduto_WhenProductListEmpty()
        {

            var produtosDtos = new List<ProdutoDto>();

            await using var DbContext = new MockDb().CreateDbContext();
            var injectServicesApi = new InjectServicesApi(DbContext, _mapper);
            var produtoService = new ProdutoServices(injectServicesApi);

            await Assert.ThrowsAsync<ArgumentException>(() => produtoService.AddProdutos(produtosDtos));
        }

    }

}
