using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Dto
{
    /// <summary>
    /// Abstract base class for Data Transfer Objects (DTOs), providing common properties for tracking entity 
    /// identifiers, creation, update, and deletion timestamps.
    /// </summary>
    public abstract class BaseEntityDto
    {
        public Guid Id { get; set; }

        public DateTime DataCreated { get; init; }

        public DateTime? DataUpdated { get; init; }

        public DateTime? DataDeleted { get; init; }
    }
}
