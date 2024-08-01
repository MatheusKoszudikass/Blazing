using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity Product.
    /// <summary>
    /// Entity responsible for general product information.
    /// </summary>
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string? Name { get; set; } 

        [StringLength(500)]
        public string? Description { get; set; } 

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } 

        [Required]
        [StringLength(3)]
        public string? Currency { get; set; } 

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        [StringLength(50)]
        public string? Brand { get; set; }

        [StringLength(50)]
        public string? SKU { get; set; } 

        public int StockQuantity { get; set; } 

        [StringLength(100)]
        public string? StockLocation { get; set; } 

        [ForeignKey("Dimensions")]
        public Guid DimensionsId { get; set; }
        public Dimensions? Dimensions { get; set; }

        [ForeignKey("AssessmentId")]
        public Guid AssessmentId { get; set; }
        public Assessment Assessment { get; set; } = new();

        [ForeignKey("Attributes")]
        public Guid AttributesId { get; set; }
        public Attributes? Attributes { get; set; }

        [ForeignKey("Availability")]
        public Guid AvailabilityId { get; set; } 
        public Availability? Availability { get; set; }

        [ForeignKey("Image")]
        public Guid ImageId { get; set; } 
        public Image? Image { get; set; }
    }
    #endregion
}
