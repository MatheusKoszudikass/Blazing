﻿using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria não pode ter mais de 100 caracteres.")]
        public string? Name { get; set; }

        public ICollection<Product?> Products { get; set; } = [];
    }
    #endregion
}
