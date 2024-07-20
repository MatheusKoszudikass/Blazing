using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazingPizza.Api.Entites
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

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
        public int CategoriaId { get; set; }

        [StringLength(50)]
        public string? Marca { get; set; }

        [StringLength(50)]
        public string? SKU { get; set; }

        public int QuantidadeEmEstoque { get; set; }
        public string? LocalizacaoEstoque { get; set; }


        [ForeignKey("Dimensoes")]
        public int DimensoesId { get; set; }
        public Dimensoes? Dimensoes { get; set; }


        [ForeignKey("Avaliacao")]
        public int AvaliacaoId { get; set; }
        public Avaliacao? Avaliacao { get; set; }


        [ForeignKey("Atributos")]
        public int AtributosId { get; set; }
        public Atributos? Atributos { get; set; }



        [ForeignKey("DisponibilidadeId")]
        public int DisponibilidadeId { get; set; }
        public Disponibilidade? Disponibilidades { get; set; }


        [ForeignKey("ImagemId")]
        public int ImagemId { get; set; }
        public Imagem? Imagem { get; set; }
    }
}
