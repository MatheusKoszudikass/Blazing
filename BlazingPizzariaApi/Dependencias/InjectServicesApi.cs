using AutoMapper;
using BlazingPizza.Api.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

namespace BlazingPizza.Api.Dependencias
{
    public partial class InjectServicesApi
    {
        public readonly IMapper _mapper;

        public readonly AppDbContext _dbContext;

        public InjectServicesApi(AppDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }   
    }
}
