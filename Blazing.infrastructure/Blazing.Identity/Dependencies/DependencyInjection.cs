using AutoMapper;
using Blazing.Identity.Data;

namespace Blazing.Identity.Dependencies
{
    public class DependencyInjection(BlazingIdentityDbContext? identityAppDbContext, IMapper mapper)
    {
        public readonly BlazingIdentityDbContext? AppContext = identityAppDbContext;

        public readonly IMapper Mapper = mapper;
    }
}
