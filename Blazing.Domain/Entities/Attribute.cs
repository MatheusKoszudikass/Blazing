using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity Attribute.
    /// <summary>
    /// Entity responsible for the product attributes.
    /// </summary>
    public class Attributes
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Color { get; set; }
        public string? Material { get; set; }
        public string? Model { get; set; }
    }
    #endregion
}
