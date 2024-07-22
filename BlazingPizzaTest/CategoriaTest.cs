using AutoMapper;
using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Profiles;
using BlazingPizza.Api.Repositories.Services;
using BlazingPizzaria.Models.DTOs;
using BlazingPizzaTest.Helps;
using Microsoft.Extensions.Logging;
using Moq;


namespace BlazingPizzaTest
{

    public class CategoriaTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CategoriaServices>> _loggerMock;

        public CategoriaTest()
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProdutosProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<CategoriaServices>>();
        }
     
        public async Task Categoria()
        {
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

            var categoriaProduto = new List<CategoriasDtos>
            {
              new ()
              {
                  Id = 1,
                  Nome = "Produto",
                  Produtos = produtosDtos
              }
            };

            await using var DbContext = new MockDb().CreateDbContext();

            var injectServiceApi = new InjectServicesApi(DbContext, _mapper);

            var categoriaService = new CategoriaServices(injectServiceApi);

            var resultAddCategoria = await categoriaService.AddCategoria(categoriaProduto);

            int categoriaId = 1;

            var resultGetCategoria = await categoriaService.GetItensPorCategorias(categoriaId);

            Assert.True(resultAddCategoria.Any());
            Assert.True(resultGetCategoria.Any());
        }
    }
}
