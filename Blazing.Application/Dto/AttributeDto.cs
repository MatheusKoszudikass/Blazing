
namespace Blazing.Application.Dto
{
    #region DTO Attribute.
    /// <summary>
    /// DTO responsible for the product attributes.
    /// </summary>
    public class AttributeDto
    {
        public Guid Id { get; set; }
        public string? Color { get; set; }
        public string? Material { get; set; }
        public string? Model { get; set; }
    }
    #endregion
}
