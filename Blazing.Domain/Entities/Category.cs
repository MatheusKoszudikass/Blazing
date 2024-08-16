using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blazing.Domain.Entities
{
    #region Entity category.
    /// <summary>
    /// Entity responsible for grouping products by categories.
    /// </summary>
    public sealed class Category : BaseEntity
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria não pode ter mais de 100 caracteres.")]
        public string? Name { get; set; }

        public IEnumerable<Product?> Products { get; set; } = new List<Product?>();
    }
    #endregion
}
