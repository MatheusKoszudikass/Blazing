using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Entities
{
    public partial class ApplicationUserToken : IdentityUserToken<Guid>
    {
        public DateTime DateToken { get; set; } = DateTime.Now;
    }
}
