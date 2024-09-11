﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blazing.Application.Mappings;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;
using Blazing.Domain.Services;
using BlazingPizzaTest.Data;
using Moq;

namespace Blazing.Test.Application
{
    public class ApplicationFixtureTest
    {
        public PeopleOfData PeopleOfData { get; } = new();

        public  IMapper Mapper { get; }

        //Products
        public ProductDomainService ProductDomainService { get; }
        public ProductAppService ProductAppService { get; }


        //Categories
        public CategoryDomainService CategoryDomainService { get; }
        public CategoryAppService CategoryAppService { get; }

        //Users
        public UserDomainService UserDomainService { get; }
        public UserAppService UserAppService { get; }


        public ApplicationFixtureTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BlazingProfile>();
            }).CreateMapper();

            Mapper = config;

            //Products
            ProductDomainService = new ProductDomainService();

            ProductAppService = new ProductAppService(Mapper,ProductDomainService);


            //Categories
            CategoryDomainService = new CategoryDomainService();

            CategoryAppService = new CategoryAppService(CategoryDomainService, Mapper);

            //Users
            UserDomainService = new UserDomainService();

            UserAppService = new UserAppService(Mapper, UserDomainService);
        }

    }
}