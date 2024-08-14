using AutoMapper;
using Blazing.Domain.Interfaces.Repository;
using Blazing.infrastructure.Data;

namespace Blazing.infrastructure.Dependency
{
    public class DependencyInjection(BlazingDbContext AppDbContext, IMapper mapper)
    {
        public readonly BlazingDbContext _appContext = AppDbContext;

        public readonly IMapper _mapper = mapper;
    }
}
