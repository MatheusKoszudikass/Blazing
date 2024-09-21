using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Domain.Entities
{
    /// <summary>
    /// Represents a user role with associated properties including status, name, description, permissions, and related user information.
    /// </summary>
    public sealed class Role : BaseEntity
    {
        public bool Status { get; init; }

        [Required(ErrorMessage = "Nome obrigatório.")]
        [StringLength(50, ErrorMessage = "Nome da função deve ter no máximo 50 caracteres.")]
        public string Name { get; init; } = string.Empty;

        [StringLength(255, ErrorMessage = "Descrição deve ter no máximo 255 caracteres.")]
        public string Description { get; init; } = string.Empty;

        [Required]
        public Guid UserCreatedId { get; init; } = new ();

        [Required]
        public Guid UserUpdatedId { get; init; } = new();

        [Required] 
        public ICollection<User> User { get; init; } = [];

        [Required] 
        public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
    }
}
