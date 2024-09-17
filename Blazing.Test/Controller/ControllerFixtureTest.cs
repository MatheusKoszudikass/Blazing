using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blazing.Api.Controllers.Category;
using Blazing.Api.Controllers.Product;
using Blazing.Api.Controllers.User;
using Blazing.Application.Dto;
using Blazing.Application.Interface.Product;
using Blazing.Application.Mappings;
using Blazing.Application.Services;
using Blazing.Domain.Services;
using Blazing.Ecommerce.Data;
using Blazing.Ecommerce.Dependency;
using Blazing.Ecommerce.Interface;
using Blazing.Ecommerce.Repository;
using Blazing.Test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blazing.Test.Controller
{
    public class ControllerFixtureTest : IDisposable
    {
        private bool _disposed;

        //Dados
        public PeopleOfData PeopleOfData { get; }
        public BlazingDbContext DbContext { get; }
        public IMapper Mapper { get; }
        public ProductDtoMapping ProductMapping { get; }
        public CategoryDtoMapping CategoryMapping { get; }
        public DependencyInjection DependencyInjection { get; }
        public IMemoryCache MemoryCache;

        //ProductController
        private readonly Mock<ILogger<ProductController>> _loggerController = new ();
        public readonly Mock<IProductInfrastructureRepository> ProductInfrastructureRepository = new ();
        public ProductDomainService ProductDomainService { get; }
        public ProductAppService ProductAppService { get; }
        public ProductInfrastructureRepository ProductInfrastructure { get; }
        public ProductController ProductController { get; }

        //CategoryController
        private readonly Mock<ILogger<CategoryController>> _loggerCategoryController = new ();
        public readonly Mock<ICategoryInfrastructureRepository> CategoryInfrastructureRepository = new ();
        public CategoryDomainService CategoryDomainService { get; }
        public CategoryAppService CategoryAppService { get; }
        public CategoryInfrastructureRepository CategoryInfrastructure { get; }
        public CategoryController CategoryController { get; }

        //UserController
        private readonly Mock<ILogger<UserController>> _loggerUserController = new();
        public readonly Mock<Identity.Interface.IUserInfrastructureRepository> _userInfrastructureRepository = new();
        public UserController UserController => new(_loggerUserController.Object, _userInfrastructureRepository.Object);

        public ControllerFixtureTest()
        {
            PeopleOfData = new PeopleOfData();
            DbContext = new MockDb().CreateDbContext();
            var config = new MapperConfiguration(cfg =>
            {
                // Add the BlazingProfile to the MapperConfiguration
                cfg.AddProfile<BlazingProfile>();
            }).CreateMapper();

            Mapper = config;
            ProductMapping = new ProductDtoMapping();
            CategoryMapping = new CategoryDtoMapping();
            MemoryCache = new MemoryCache(new MemoryCacheOptions());
            DependencyInjection = new DependencyInjection(DbContext, Mapper);

            //Products
            ProductDomainService = new ProductDomainService();
            ProductAppService = new ProductAppService(Mapper, ProductDomainService);
            ProductInfrastructure = new ProductInfrastructureRepository(ProductMapping, MemoryCache, DependencyInjection, ProductAppService);
            ProductController = new ProductController(_loggerController.Object, ProductInfrastructure);

            //Categories
            CategoryDomainService = new CategoryDomainService();
            CategoryAppService = new CategoryAppService(CategoryDomainService, Mapper);
            CategoryInfrastructure = new CategoryInfrastructureRepository(CategoryMapping, MemoryCache, CategoryAppService, DependencyInjection);
            CategoryController = new CategoryController(_loggerCategoryController.Object, CategoryInfrastructure);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
                DbContext?.Dispose();

            _disposed = true;
        }
    }
}
