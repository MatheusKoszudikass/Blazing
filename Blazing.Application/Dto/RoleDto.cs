using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Dto
{

    /// <summary>
    /// Data Transfer Object (DTO) for the Role entity, including properties for status, name, description, 
    /// creation and update user IDs, and collections of associated users and permissions.
    /// </summary>
    public class RoleDto : BaseEntityDto
    {
        public bool Status { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid UserCreatedId { get; set; } = new();

        public Guid UserUpdatedId { get; set; } = new();

        public ICollection<UserDto> User { get; set; } = new List<UserDto>();

        public ICollection<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}
