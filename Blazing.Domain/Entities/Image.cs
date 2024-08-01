using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity Image. 
    /// <summary>
    /// Entity responsible for product images in general.
    /// </summary>
    public class Image
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Url { get; set; }
        [StringLength(200)]
        public string? AltText { get; set; }
    }
    #endregion
}
