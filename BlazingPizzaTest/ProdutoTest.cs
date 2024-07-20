using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Services;
using BlazingPizzaTest.Helps;

namespace BlazingPizzaTest
{
    public class ProdutoTest
    {
        [Fact]
        public async void CriarProdutoDb()
        {
            var produtos = new List<Produto>
            {
                new Produto
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
                    Dimensoes = new Dimensoes
                    {
                        Id = 1,
                        Peso = 1.5,
                        Altura = 10.0,
                        Largura = 15.0,
                        Profundidade = 20.0,
                        Unidade = "cm"
                    },
                    Avaliacao = new Avaliacao
                    {
                        Id = 1,
                        Media = 4.5,
                        NumeroDeAvaliacoes = 10,
                        Revisao = new Revisao
                        {
                            Id = 1,
                            Usuario = "Usuario 1",
                            Comentario = "Muito bom!",
                            Data = DateTime.Now
                        }
                    },
                    Atributos = new Atributos
                    {
                        Id = 1,
                        Cor = "Azul",
                        Material = "Plástico",
                        Modelo = "Modelo A"
                    },
                    Disponibilidades = new Disponibilidade
                    {
                        Id = 1,
                        EstaDisponivel = true,
                        DataEstimadaDeEntrega = DateTime.Now.AddDays(5)
                    },
                    Imagem = new Imagem
                    {
                        Id = 1,
                        Url = "https://exemplo.com/imagem1.jpg",
                        TextoAlternativo = "Imagem do Produto 1"
                    }
                },

                new Produto
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
                       Dimensoes = new Dimensoes
                       {
                          Id = 2,
                          Peso = 2.5,
                          Altura = 20.0,
                          Largura = 25.0,
                          Profundidade = 30.0,
                          Unidade = "cm"
                       },
                       Avaliacao = new Avaliacao
                       {
                          Id = 2,
                          Media = 3.5,
                          NumeroDeAvaliacoes = 5,

                           Revisao = new Revisao
                           {
                              Id = 2,
                              Usuario = "Usuario 2",
                              Comentario = "Bom, mas poderia ser melhor.",
                              Data = DateTime.Now
                           }
                       },
                       Atributos = new Atributos
                       {
                          Id = 2,
                          Cor = "Vermelho",
                          Material = "Metal",
                          Modelo = "Modelo B"
                       },
                       Disponibilidades = new Disponibilidade
                       {
                          Id = 2,
                          EstaDisponivel = false,
                          DataEstimadaDeEntrega = DateTime.Now.AddDays(10)
                       },
                       Imagem = new Imagem
                       {
                          Id = 2,
                          Url = "https://exemplo.com/imagem2.jpg",
                          TextoAlternativo = "Imagem do Produto 2"
                       }
                }
            };
            await using var DbContext = new MockDb().CreateDbContext();

        }
    }
}
