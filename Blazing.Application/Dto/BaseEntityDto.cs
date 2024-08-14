using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Dto
{
    public abstract class BaseEntityDto
    {
        public Guid Id { get; set; }

        public DateTime DataCreated { get; set; }

        public DateTime? DataUpdated { get; set; }

        public DateTime? DataDeleted { get; set; }
    }
}
