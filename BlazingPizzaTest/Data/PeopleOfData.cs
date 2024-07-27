using BlazingPizzaria.Models.DTOs;
using System;
using System.Collections.Generic;

namespace BlazingPizzaTest.Data
{
    public class PeopleOfData
    {
        // Chaves geradas para o primeiro produto.
        public  Guid ProdutoId { get; set; } = Guid.NewGuid();
        public Guid DimensoesId { get; set; } = Guid.NewGuid();
        public Guid AvaliacaoId { get; set; } = Guid.NewGuid();
        public Guid RevisaoId { get; set; } = Guid.NewGuid();
        public Guid CategoriaId { get; set; } = Guid.NewGuid();
        public Guid AtributosId { get; set; } = Guid.NewGuid();
        public Guid DisponibilidadeId { get; set; } = Guid.NewGuid();
        public Guid ImagemId { get; set; } = Guid.NewGuid();

        // Chaves geradas para o segundo produto.
        public Guid ProdutoId1 { get; set; } = Guid.NewGuid();
        public Guid DimensoesId1 { get; set; } = Guid.NewGuid();
        public Guid AvaliacaoId1 { get; set; } = Guid.NewGuid();
        public Guid RevisaoId1 { get; set; } = Guid.NewGuid();
        public Guid CategoriaId1 { get; set; } = Guid.NewGuid();
        public Guid AtributosId1 { get; set; } = Guid.NewGuid();
        public Guid DisponibilidadeId1 { get; set; } = Guid.NewGuid();
        public Guid ImagemId1 { get; set; } = Guid.NewGuid();

        #region Categorias.
        //Metodo para adicionar Categorias.
        public List<CategoriasDto> AddCategoria()
        {
            return new List<CategoriasDto>
            {
                new CategoriasDto
                {
                    Id = CategoriaId,
                    Nome = "Categoria",
                },
                new ()
                {
                    Id = CategoriaId1,
                    Nome = "Categoria 1",

                }

            };

        }

        //Metodo para puxa categorias.
        public List<CategoriasDto> GetItemCategoria()
        {
            return new List<CategoriasDto>
            {
                new CategoriasDto
                {
                    Id = CategoriaId,
                    Nome = "Categoria",
                },
                new ()
                {
                    Id = CategoriaId1,
                    Nome = "Categoria 1",
                }

            };

        }
        //Metodo para editar categoria.
        public CategoriasDto UpdateCategoria()
        {
            return new CategoriasDto
            {

                Id = CategoriaId,
                Nome = "Categoria edit",

            };
        }
        public List<Guid> GetIdsCategoria()
        {
            return new List<Guid>
            {
                CategoriaId,

            };
        }
        #endregion


        #region Produtos.
        // Método para inicializar a lista de produtos
        public List<ProdutoDto> AddProdutos()
        {
            return new List<ProdutoDto>
            {
                new ProdutoDto
                {
                    Id = ProdutoId,
                    Nome = "Produto 1",
                    Descricao = "Descrição do Produto 1",
                    Preco = 100.00M,
                    Moeda = "BRL",
                    CategoriaId = CategoriaId,
                    Marca = "Marca A",
                    SKU = "SKU001",
                    QuantidadeEmEstoque = 10,
                    LocalizacaoEstoque = "A1",
                    DimensoesId = DimensoesId,
                    Dimensoes = new ()
                    {
                        Id = DimensoesId,
                        Peso = 1.5,
                        Altura = 10.0,
                        Largura = 15.0,
                        Profundidade = 20.0,
                        Unidade = "cm"
                    },
                    AvaliacaoId = AvaliacaoId,
                    Avaliacao = new ()
                    {
                        Id = AvaliacaoId,
                        Media = 4.5,
                        NumeroDeAvaliacoes = 10,
                        RevisaoId = RevisaoId,
                        Revisao = new ()
                        {
                            Id = RevisaoId,
                            Usuario = "Usuario 1",
                            Comentario = "Muito bom!",
                            Data = DateTime.Now
                        }
                    },
                    AtributosId = AtributosId,
                    Atributos = new ()
                    {
                        Id = AtributosId,
                        Cor = "Azul",
                        Material = "Plástico",
                        Modelo = "Modelo A"
                    },
                    DisponibilidadeId = DisponibilidadeId,
                    Disponibilidades = new ()
                    {
                        Id = DisponibilidadeId,
                        EstaDisponivel = true,
                        DataEstimadaDeEntrega = DateTime.Now.AddDays(5)
                    },
                    ImagemId = ImagemId,
                    Imagem = new ()
                    {
                        Id = ImagemId,
                        Url = "https://exemplo.com/imagem1.jpg",
                        TextoAlternativo = "Imagem do Produto 1"
                    }
                },
                new ()
                {
                    Id = ProdutoId1,
                    Nome = "Produto 2",
                    Descricao = "Descrição do Produto 2",
                    Preco = 200.00M,
                    Moeda = "BRL",
                    CategoriaId = CategoriaId1,
                    Marca = "Marca B",
                    SKU = "SKU002",
                    QuantidadeEmEstoque = 20,
                    LocalizacaoEstoque = "B1",
                    DimensoesId = DimensoesId1,
                    Dimensoes = new ()
                    {
                        Id = DimensoesId1,
                        Peso = 2.5,
                        Altura = 20.0,
                        Largura = 25.0,
                        Profundidade = 30.0,
                        Unidade = "cm"
                    },
                    AvaliacaoId = AvaliacaoId1,
                    Avaliacao = new ()
                    {
                        Id = AvaliacaoId1,
                        Media = 3.5,
                        NumeroDeAvaliacoes = 5,
                        RevisaoId = RevisaoId1,
                        Revisao = new ()
                        {
                            Id = RevisaoId1,
                            Usuario = "Usuario 2",
                            Comentario = "Bom, mas poderia ser melhor.",
                            Data = DateTime.Now
                        }
                    },
                    AtributosId = AtributosId1,
                    Atributos = new ()
                    {
                        Id = AtributosId1,
                        Cor = "Vermelho",
                        Material = "Metal",
                        Modelo = "Modelo B"
                    },
                    DisponibilidadeId = DisponibilidadeId1,
                    Disponibilidades = new ()
                    {
                        Id = DisponibilidadeId1,
                        EstaDisponivel = false,
                        DataEstimadaDeEntrega = DateTime.Now.AddDays(10)
                    },
                    ImagemId = ImagemId1,
                    Imagem = new ()
                    {
                        Id = ImagemId1,
                        Url = "https://exemplo.com/imagem2.jpg",
                        TextoAlternativo = "Imagem do Produto 2"
                    }
                }
            };
        }

        public ProdutoDto UpdateProdutos()
        {
            return new ProdutoDto
            {
                Id = ProdutoId,
                Nome = "Produto 1 editado",
                Descricao = "Descrição do Produto 1",
                Preco = 100.00M,
                Moeda = "BRL",
                CategoriaId = CategoriaId,
                Marca = "Marca A",
                SKU = "SKU001",
                QuantidadeEmEstoque = 10,
                LocalizacaoEstoque = "A1",
                DimensoesId = DimensoesId,
                Dimensoes = new ()
                {
                    Id = DimensoesId,
                    Peso = 1.5,
                    Altura = 10.0,
                    Largura = 15.0,
                    Profundidade = 20.0,
                    Unidade = "cm"
                },
                AvaliacaoId = AvaliacaoId,
                Avaliacao = new ()
                {
                    Id = AvaliacaoId,
                    Media = 4.5,
                    NumeroDeAvaliacoes = 10,
                    RevisaoId = RevisaoId,
                    Revisao = new RevisaoDto
                    {
                        Id = RevisaoId,
                        Usuario = "Usuario 1",
                        Comentario = "Muito bom!",
                        Data = DateTime.Now
                    }
                },
                AtributosId = AtributosId,
                Atributos = new ()
                {
                    Id = AtributosId,
                    Cor = "Azul",
                    Material = "Plástico",
                    Modelo = "Modelo A"
                },
                DisponibilidadeId = DisponibilidadeId,
                Disponibilidades = new ()
                {
                    Id = DisponibilidadeId,
                    EstaDisponivel = true,
                    DataEstimadaDeEntrega = DateTime.Now.AddDays(5)
                },
                ImagemId = ImagemId,
                Imagem = new ()
                {
                    Id = ImagemId,
                    Url = "https://exemplo.com/imagem1.jpg",
                    TextoAlternativo = "Imagem do Produto 1"
                }
            };
        }

        public List<Guid> GetIds()
        {
            return new List<Guid>
            {
                ProdutoId,

            };
        }
#endregion

    }
}
