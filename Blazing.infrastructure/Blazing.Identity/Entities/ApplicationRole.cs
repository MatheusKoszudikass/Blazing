using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Entities
{
    public partial class ApplicationRole() : IdentityRole<Guid>
    {
        [StringLength(256)]
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public int Page { get; set; }

        [NotMapped]
        public int PageSize { get; set; }
    }
}
