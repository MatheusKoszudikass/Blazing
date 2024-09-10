using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Domain.Interfaces.Services.User
{
    public interface IUserDomainService : ICrudDomainService<Entities.User>
    {
        Task<bool> UserExistsAsync(bool id, bool userName, bool userEmail, IEnumerable<Entities.User> users, CancellationToken cancellationToken);
    }
}
