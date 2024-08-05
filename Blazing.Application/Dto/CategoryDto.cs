using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO category.
    /// <summary>
    /// DTO responsible for grouping products by categories.
    /// </summary>
    public class CategoryDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria não pode ter mais de 100 caracteres.")]
        public string? Name { get; set; }
        public ICollection<ProductDto?> Products { get; set; } = [];
    }
    #endregion
}
