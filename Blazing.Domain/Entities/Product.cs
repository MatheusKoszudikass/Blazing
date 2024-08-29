using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Blazing.Domain.Entities
{
    #region Entity Product.
    /// <summary>
    /// Entity responsible for general product information.
    /// </summary>
    public sealed class Product : BaseEntity
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto não pode ter mais de 100 caracteres.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "A descrição do produto não pode ter mais de 500 caracteres.")]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "A moeda é obrigatória.")]
        [StringLength(3, ErrorMessage = "O código da moeda deve ter exatamente 3 caracteres.")]
        public string? Currency { get; set; } 

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        [StringLength(50, ErrorMessage = "A marca não pode ter mais de 50 caracteres.")]
        public string? Brand { get; set; }

        [StringLength(50, ErrorMessage = "O código SKU não pode ter mais de 50 caracteres.")]
        public string? SKU { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser um valor positivo ou zero.")]
        public int StockQuantity { get; set; }

        [StringLength(100, ErrorMessage = "A localização do estoque não pode ter mais de 100 caracteres.")]
        public string? StockLocation { get; set; }


        /// <summary>
        /// Product dimensions identifier.
        /// </summary>
        [ForeignKey("Dimensions")]
        public Guid DimensionsId { get; set; }
        public Dimensions? Dimensions { get; set; }

        /// <summary>
        /// Product review identifier.
        /// </summary>
        [ForeignKey("AssessmentId")]
        public Guid AssessmentId { get; set; }

        /// <summary>
        /// Product review details.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Assessment? Assessment { get; set; }

        /// <summary>
        /// Product attribute identifier.
        /// </summary>
        [ForeignKey("Attributes")]
        public Guid AttributesId { get; set; }

        /// <summary>
        /// Product attribute details.
        /// </summary>
        public Attributes? Attributes { get; set; }


        /// <summary>
        /// Product availability identifier.
        /// </summary>
        [ForeignKey("Availability")]
        public Guid AvailabilityId { get; set; }

        /// <summary>
        /// Product availability details.
        /// </summary>
        public Availability? Availability { get; set; }

        /// <summary>
        /// Product image identifier.
        /// </summary>
        [ForeignKey("Image")]
        public Guid ImageId { get; set; }

        /// <summary>
        /// Product image.
        /// </summary>
        public Image? Image { get; set; }
    }
    #endregion
}
