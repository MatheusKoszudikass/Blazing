using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazingPizzaria.Models.DTOs
{
    public class ProdutoDto
    {
        public Guid Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public string? Moeda { get; set; }

        public Guid CategoriaId { get; set; }

        public string? Marca { get; set; }

        public string? SKU { get; set; }

        public int QuantidadeEmEstoque { get; set; }
        public string? LocalizacaoEstoque { get; set; }

        public Guid DimensoesId { get; set; }
        public DimensoesDto? Dimensoes { get; set; }

        public Guid AvaliacaoId { get; set; }
        public AvaliacaoDto? Avaliacao { get; set; }

        public Guid AtributosId { get; set; }
        public AtributosDto? Atributos { get; set; }


        public Guid DisponibilidadeId { get; set; }
        public DisponibilidadeDto? Disponibilidades { get; set; }


        public Guid ImagemId { get; set; }
        public ImagemDto? Imagem { get; set; }

    }
}
