using Blazing.Domain.Interfaces.Repository;
using Blazing.Identity.Dto;
using Blazing.Identity.Entities;
using Blazing.Identity.Interface;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Identity.Interface
{
    public interface IRoleInfrastructureRepository : ICrudInfrastructureIdentityRepository<IdentityResult, ApplicationRoleDto>
    {

    }
}
