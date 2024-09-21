using AutoMapper;
using Blazing.Ecommerce.Data;

namespace Blazing.Ecommerce.Dependencies
{
    public class DependencyInjection(BlazingDbContext AppDbContext, IMapper mapper)
    {
        public readonly BlazingDbContext _appContext = AppDbContext;

        public readonly IMapper _mapper = mapper;
    }
}
