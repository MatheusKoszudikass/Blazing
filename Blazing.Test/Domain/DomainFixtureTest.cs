using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blazing.Application.Mappings;
using Blazing.Domain.Services;
using Blazing.Test.Data;

namespace Blazing.Test.Domain
{
    public class DomainFixtureTest
    {
        //Dados
        public PeopleOfData PeopleOfData { get; } = new();

        public IMapper Mapper { get; } 

        //Product
        public ProductDomainService ProductDomainService {get;} = new();

        //Categories
        public CategoryDomainService CategoryDomainService { get; } = new();

        //Users
        public UserDomainService UserDomainService { get; } = new();

        public DomainFixtureTest()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BlazingProfile>();
            }).CreateMapper();
        }

    }
}
