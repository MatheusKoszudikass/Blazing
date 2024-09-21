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
        public string Name { get; init; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição do produto não pode ter mais de 500 caracteres.")]
        public string? Description { get; init; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
        public decimal Price { get; init; }

        [Required(ErrorMessage = "A moeda é obrigatória.")]
        [StringLength(3, ErrorMessage = "O código da moeda deve ter exatamente 3 caracteres.")]
        public string? Currency { get; init; } 

        [ForeignKey("Category")]
        public Guid CategoryId { get; init; }

        [StringLength(50, ErrorMessage = "A marca não pode ter mais de 50 caracteres.")]
        public string? Brand { get; init; }

        [StringLength(50, ErrorMessage = "O código SKU não pode ter mais de 50 caracteres.")]
        public string? Sku { get; init; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser um valor positivo ou zero.")]
        public int StockQuantity { get; init; }

        [StringLength(100, ErrorMessage = "A localização do estoque não pode ter mais de 100 caracteres.")]
        public string? StockLocation { get; init; }


        /// <summary>
        /// Product dimensions identifier.
        /// </summary>
        [ForeignKey("Dimensions")]
        public Guid DimensionsId { get; init; }
        public Dimensions? Dimensions { get; init; }

        /// <summary>
        /// Product review identifier.
        /// </summary>
        [ForeignKey("AssessmentId")]
        public Guid AssessmentId { get; init; }

        /// <summary>
        /// Product review details.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Assessment? Assessment { get; init; }

        /// <summary>
        /// Product attribute identifier.
        /// </summary>
        [ForeignKey("Attributes")]
        public Guid AttributesId { get; init; }

        /// <summary>
        /// Product attribute details.
        /// </summary>
        public Attributes? Attributes { get; init; }


        /// <summary>
        /// Product availability identifier.
        /// </summary>
        [ForeignKey("Availability")]
        public Guid AvailabilityId { get; init; }

        /// <summary>
        /// Product availability details.
        /// </summary>
        public Availability? Availability { get; init; }

        /// <summary>
        /// Product image identifier.
        /// </summary>
        [ForeignKey("Image")]
        public Guid ImageId { get; init; }

        /// <summary>
        /// Product image.
        /// </summary>
        public Image? Image { get; init; }
    }
    #endregion
}
