using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Dto
{
    public sealed class CreateCategory : BaseEntity
    {
        public string? Name { get; set; }
    }
}
