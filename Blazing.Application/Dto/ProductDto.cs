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
    public sealed record ProductDto 
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;

        public string? Description { get; init; }

        public decimal Price { get; init; }

        public string? Currency { get; init; }

        public Guid CategoryId { get; init; }

        public string? Brand { get; init; }

        public string? Sku { get; init; }

        public int StockQuantity { get; init; }

        public string? StockLocation { get; init; }

        public Guid DimensionsId { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DimensionsDto? Dimensions { get; init; }

        public Guid AssessmentId { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AssessmentDto? Assessment { get; init; }

        public Guid AttributesId { get; init; } 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AttributeDto? Attributes { get; init; }

        public Guid AvailabilityId { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AvailabilityDto? Availability { get; init; }

        public Guid ImageId { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ImageDto? Image { get; init; }
    }
    #endregion
}
