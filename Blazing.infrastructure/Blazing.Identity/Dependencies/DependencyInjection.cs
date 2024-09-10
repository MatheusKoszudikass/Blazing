using AutoMapper;
using Blazing.Ecommerce.Data;
using Blazing.Identity.Data;


namespace Blazing.Identity.Dependency
{
    public class DependencyInjection(BlazingIdentityDbContext? IdentityAppDbContext, IMapper mapper)
    {
        public readonly BlazingIdentityDbContext? _appContext = IdentityAppDbContext;

        public readonly IMapper _mapper = mapper;
    }
}
