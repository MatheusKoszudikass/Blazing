using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Domain.Entities
{
    #region Entity category.
    /// <summary>
    /// Entity responsible for grouping products by categories.
    /// </summary>
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public ICollection<Product?> Products { get; set; } = [];
    }
    #endregion
}
