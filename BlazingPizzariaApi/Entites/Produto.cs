using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável pela informações em geral do produto.
    /// </summary>
    public class Produto
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(3)]
        public string? Moeda { get; set; }

        [ForeignKey("Categoria")]
        public Guid CategoriaId { get; set; }

        [StringLength(50)]
        public string? Marca { get; set; }

        [StringLength(50)]
        public string? SKU { get; set; }

        public int QuantidadeEmEstoque { get; set; }
        public string? LocalizacaoEstoque { get; set; }


        [ForeignKey("Dimensoes")]
        public Guid DimensoesId { get; set; }
        public Dimensoes? Dimensoes { get; set; }


        [ForeignKey("Avaliacao")]
        public Guid AvaliacaoId { get; set; }
        public Avaliacao? Avaliacao { get; set; }


        [ForeignKey("Atributos")]
        public Guid AtributosId { get; set; }
        public Atributo? Atributos { get; set; }



        [ForeignKey("DisponibilidadeId")]
        public Guid DisponibilidadeId { get; set; }
        public Disponibilidade? Disponibilidades { get; set; }


        [ForeignKey("ImagemId")]
        public Guid ImagemId { get; set; }
        public Imagem? Imagem { get; set; }
    }
}
