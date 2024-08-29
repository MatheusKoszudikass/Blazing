using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Domain.Interfaces.Services.User
{
    public interface IUserDomainService : ICrudDomainService<Entities.User>
    {
        bool AreUseEqual(Entities.User user1, Entities.User user2);
        string NormalizeString(string? input);
    }
}
