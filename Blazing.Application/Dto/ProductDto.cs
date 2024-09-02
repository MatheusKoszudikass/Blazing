using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Blazing.Application.Dto
{
    #region DTO Product.
    /// <summary>
    /// DTO responsible for general product information.
    /// </summary>
    public sealed class ProductDto : BaseEntityDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? Currency { get; set; }

        public Guid CategoryId { get; set; }

        public string? Brand { get; set; }

        public string? SKU { get; set; }

        public int StockQuantity { get; set; }

        public string? StockLocation { get; set; }

        public Guid DimensionsId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DimensionsDto? Dimensions { get; set; }

        public Guid AssessmentId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AssessmentDto? Assessment { get; set; }

        public Guid AttributesId { get; set; } 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AttributeDto? Attributes { get; set; }

        public Guid AvailabilityId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AvailabilityDto? Availability { get; set; }

        public Guid ImageId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ImageDto? Image { get; set; }
    }
    #endregion
}
