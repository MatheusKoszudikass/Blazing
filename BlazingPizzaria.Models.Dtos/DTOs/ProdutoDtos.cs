using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazingPizzaria.Models.DTOs
{
    public class ProdutoDtos
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public string? Moeda { get; set; }

        public int CategoriaId { get; set; }

        public string? Marca { get; set; }

        public string? SKU { get; set; }

        public int QuantidadeEmEstoque { get; set; }
        public string? LocalizacaoEstoque { get; set; }

        public int DimensoesId { get; set; }
        public DimensoesDtos? Dimensoes { get; set; }

        public int AvaliacaoId { get; set; }
        public AvaliacaoDtos? Avaliacao { get; set; }

        public int AtributosId { get; set; }
        public AtributosDtos? Atributos { get; set; }


        public int DisponibilidadeId { get; set; }
        public DisponibilidadeDtos? Disponibilidades { get; set; }


        public int ImagemId { get; set; }
        public ImagemDtos? Imagem { get; set; }

    }
}
