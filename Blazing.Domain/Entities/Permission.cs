using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Domain.Entities
{
    /// <summary>
    /// The PermissionRole class represents a role with specific permissions in the system. 
    /// It inherits from the BaseEntity class and contains two primary properties: 
    /// Name and Description. 
    /// - The Name property stores the name of the permission, which is required and has a maximum length of 50 characters. 
    /// - The Description property holds a brief explanation of the permission, which is also required and has a maximum length of 100 characters. 
    /// Both properties include validation attributes to ensure proper input is provided.
    /// </summary>
    public sealed class Permission : BaseEntity
    {
        [Required(ErrorMessage = "O nome da permissão é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome da permissão deve ter no máximo 50 caracteres")]
        public string Name { get; init; } = string.Empty;

        [Required(ErrorMessage = "A descrição da permissão é obrigatória")]
        [StringLength(100, ErrorMessage = "A descrição da permissão deve ter no máximo 100 caracteres")]
        public string Description { get; init; } = string.Empty;

        [Required]
        public Guid UserCreated { get; set; } = Guid.Empty;

        [Required]
        public Guid UserUpdated { get; set; } = Guid.Empty;

        [Required] public ICollection<Role> Roles { get; set; } = [];
    }
}
