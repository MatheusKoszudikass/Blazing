using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Dto
{
    public sealed class CreateCategoryDto : BaseEntityDto
    {
        public string? Name { get; set; }
    }
}
