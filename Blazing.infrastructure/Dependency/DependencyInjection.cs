using AutoMapper;
using Blazing.Domain.Interfaces.Repository;
using Blazing.infrastructure.Data;

namespace Blazing.infrastructure.Dependency
{
    public class DependencyInjection(AppDbContext AppDbContext, IMapper mapper)
    {
        public readonly AppDbContext _appContext = AppDbContext;

        public readonly IMapper _mapper = mapper;
    }
}
