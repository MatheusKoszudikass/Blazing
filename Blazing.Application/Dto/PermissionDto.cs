using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazing.Application.Dto
{
    public record PermissionDto 
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string? Description { get; init; }

        public Guid UserCreated { get; init; } = Guid.Empty;

        public Guid UserUpdated { get; init; } = Guid.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Role> Roles { get; set; } = [];

        public DateTime DataCreated { get; init; }

        public DateTime? DataUpdated { get; init; }

        public DateTime? DataDeleted { get; init; }
    }
}
