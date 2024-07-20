using AutoMapper;
using BlazingPizza.Api.Controllers;
using BlazingPizza.Api.Data;
using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Profiles;
using BlazingPizza.Api.Repositories.Interface;
using BlazingPizza.Api.Repositories.Services;
using BlazingPizzaria.Models.DTOs;
using BlazingPizzaTest.Helps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using Moq;
using System.Collections;

namespace BlazingPizzaTest
{
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

        [Fact]
        public async Task ProdutoService()
        {
            //Arrange
            var editProdutosDtos = new ProdutoDtos
            {
                
                    Id = 1,
                    Nome = "Produto 1 Editado",
                    Descricao = "Descrição do Produto 1",
                    Preco = 100.00M,
                    Moeda = "BRL",
                    CategoriaId = 1,
                    Marca = "Marca A",
                    SKU = "SKU001",
                    QuantidadeEmEstoque = 10,
                    LocalizacaoEstoque = "A1",
                    DimensoesId = 1,
                    Dimensoes = new ()
                    {
                        Id = 1,
                        Peso = 1.5,
                        Altura = 10.0,
                        Largura = 15.0,
                        Profundidade = 20.0,
                        Unidade = "cm"
                    },
                    AvaliacaoId = 1,
                    Avaliacao = new ()
                    {
                        Id = 1,
                        Media = 4.5,
                        NumeroDeAvaliacoes = 10,
                        RevisaoId = 1,
                        Revisao = new ()
                        {
                            Id = 1,
                            Usuario = "Usuario 1",
                            Comentario = "Muito bom!",
                            Data = DateTime.Now
                        }
                    },
                    AtributosId = 1,
                    Atributos = new ()
                    {
                        Id = 1,
                        Cor = "Azul",
                        Material = "Plástico",
                        Modelo = "Modelo A"
                    },
                    DisponibilidadeId = 1,
                    Disponibilidades = new ()
                    {
                        Id = 1,
                        EstaDisponivel = true,
                        DataEstimadaDeEntrega = DateTime.Now.AddDays(5)
                    },
                    ImagemId = 1,
                    Imagem = new ()
                    {
                        Id = 1,
                        Url = "https://exemplo.com/imagem1.jpg",
                        TextoAlternativo = "Imagem do Produto 1"
                    }
            };

            var produtosDtos = new List<ProdutoDtos>
            {
                new ()
                {
                    Id = 1,
                    Nome = "Produto 1",
                    Descricao = "Descrição do Produto 1",
                    Preco = 100.00M,
                    Moeda = "BRL",
                    CategoriaId = 1,
                    Marca = "Marca A",
                    SKU = "SKU001",
                    QuantidadeEmEstoque = 10,
                    LocalizacaoEstoque = "A1",
                    DimensoesId = 1,
                    Dimensoes = new ()
                    {
                        Id = 1,
                        Peso = 1.5,
                        Altura = 10.0,
                        Largura = 15.0,
                        Profundidade = 20.0,
                        Unidade = "cm"
                    },
                    AvaliacaoId = 1,
                    Avaliacao = new ()
                    {
                        Id = 1,
                        Media = 4.5,
                        NumeroDeAvaliacoes = 10,
                        RevisaoId = 1,
                        Revisao = new ()
                        {
                            Id = 1,
                            Usuario = "Usuario 1",
                            Comentario = "Muito bom!",
                            Data = DateTime.Now
                        }
                    },
                    AtributosId = 1,
                    Atributos = new ()
                    {
                        Id = 1,
                        Cor = "Azul",
                        Material = "Plástico",
                        Modelo = "Modelo A"
                    },
                    DisponibilidadeId = 1,
                    Disponibilidades = new ()
                    {
                        Id = 1,
                        EstaDisponivel = true,
                        DataEstimadaDeEntrega = DateTime.Now.AddDays(5)
                    },
                    ImagemId = 1,
                    Imagem = new ()
                    {
                        Id = 1,
                        Url = "https://exemplo.com/imagem1.jpg",
                        TextoAlternativo = "Imagem do Produto 1"
                    }
                },

                new ()
                {
                       Id = 2,
                       Nome = "Produto 2",
                       Descricao = "Descrição do Produto 2",
                       Preco = 200.00M,
                       Moeda = "BRL",
                       CategoriaId = 2,
                       Marca = "Marca B",
                       SKU = "SKU002",
                       QuantidadeEmEstoque = 20,
                       LocalizacaoEstoque = "B1",
                       DimensoesId = 2,
                       Dimensoes = new ()
                       {
                          Id = 2,
                          Peso = 2.5,
                          Altura = 20.0,
                          Largura = 25.0,
                          Profundidade = 30.0,
                          Unidade = "cm"
                       },
                       AvaliacaoId = 2,
                       Avaliacao = new ()
                       {
                          Id = 2,
                          Media = 3.5,
                          NumeroDeAvaliacoes = 5,
                          RevisaoId = 2,
                           Revisao = new ()
                           {
                              Id = 2,
                              Usuario = "Usuario 2",
                              Comentario = "Bom, mas poderia ser melhor.",
                              Data = DateTime.Now
                           }
                       },
                       AtributosId = 2,
                       Atributos = new ()
                       {
                          Id = 2,
                          Cor = "Vermelho",
                          Material = "Metal",
                          Modelo = "Modelo B"
                       },
                       DisponibilidadeId = 2,
                       Disponibilidades = new ()
                       {
                          Id = 2,
                          EstaDisponivel = false,
                          DataEstimadaDeEntrega = DateTime.Now.AddDays(10)
                       },
                       ImagemId = 2,
                       Imagem = new ()
                       {
                          Id = 2,
                          Url = "https://exemplo.com/imagem2.jpg",
                          TextoAlternativo = "Imagem do Produto 2"
                       }
                }
            };

            List<int> ids =
            [
                2
            ];
            int produtoID = 1;

            await using var DbContext = new MockDb().CreateDbContext();

            var injectServiceApi = new InjectServicesApi(DbContext, _mapper);

            var produtoService = new ProdutoServices(injectServiceApi);

            //Act
            var resultAddProduto = await produtoService.AddProduto(produtosDtos);

            var resultEditProduto = await produtoService.EditProduto(produtoID, editProdutosDtos);

            var resultCategoriaProdutos = await produtoService.GetItensProdutoCategoria(produtoID);

            var resultGetItens = await produtoService.GetItens();

            var resultGetItem = await produtoService.GetItem(produtoID);

            var resultDeleteProduto = await produtoService.DeleteProduto(ids);
            //Assert
            Assert.True(resultAddProduto.Any());
            Assert.True(resultAddProduto.Any());
            Assert.True(resultGetItens.Any());
            Assert.NotNull(resultEditProduto);
            Assert.NotNull(resultGetItem);
            Assert.True(resultDeleteProduto.Any());
    
            Assert.Equal(produtosDtos.Count, resultAddProduto.Count());

            foreach (var dto in produtosDtos)
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

            var produtosDtos = new List<ProdutoDtos>();

            await using var DbContext = new MockDb().CreateDbContext();
            var injectServicesApi = new InjectServicesApi(DbContext, _mapper);
            var produtoService = new ProdutoServices(injectServicesApi);

            await Assert.ThrowsAsync<ArgumentException>(() => produtoService.AddProduto(produtosDtos));
        }

    }

}
