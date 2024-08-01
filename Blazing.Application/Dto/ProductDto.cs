namespace Blazing.Application.Dto
{
    #region DTO Product.
    /// <summary>
    /// DTO responsible for general product information.
    /// </summary>
    public class ProductDto
    {
        public Guid Id { get; set; }

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
        public DimensionsDto? Dimensions { get; set; }

        public Guid AssessmentId { get; set; }
        public AssessmentDto Assessment { get; set; } = new();

        public Guid AttributesId { get; set; }
        public Attribute? Attributes { get; set; }

        public Guid AvailabilityId { get; set; }
        public AvailabilityDto? Availability { get; set; }

        public Guid ImageId { get; set; }
        public ImageDto? Image { get; set; }
    }
    #endregion
}
