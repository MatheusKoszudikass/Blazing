using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Entities
{
    public partial class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public DateTime logado { get; set; } = DateTime.Now;
    }
}
