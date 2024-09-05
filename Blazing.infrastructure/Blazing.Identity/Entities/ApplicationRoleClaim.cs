using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Entities
{
    public partial class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        //public DateTime DataCreated { get; set; } = DateTime.Now;
        //public DateTime DataUpdated { get; set; }
        //public DateTime DataDeleted { get; set; }
    }
}
