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

        [StringLength(50, ErrorMessage ="A cor não pode ter mais de 50 caracteres")]
        public string? Color { get; set; }

        [StringLength(100, ErrorMessage = "O material não pode ter mais de 100 caracteres")]
        public string? Material { get; set; }

        [StringLength(100, ErrorMessage =   "O modelo não pode ter mais de 100 caracteres")]
        public string? Model { get; set; }
    }
    #endregion
}
