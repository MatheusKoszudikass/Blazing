using AutoMapper;
using Blazing.Application.Services;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Repository;
using Blazing.Domain.Services;
using Blazing.infrastructure.Data;
using Blazing.infrastructure.Dependency;
using Blazing.Test.Data;
using BlazingPizzaTest.Data;
using Moq;
using Blazing.Application.Mappings;
using Blazing.infrastructure.Service;
using Blazing.Domain.Interfaces.Services;
using Blazing.Application.Dto;

namespace Blazing.Test.Infrastructure
{
    /// <summary>
    /// Represents a fixture for testing the ProductRepository.
    /// </summary>
    public class RepositoryFixtureTest : IDisposable
    {
        /// <summary>
        /// Gets the AutoMapper instance.
        /// </summary>
        public IMapper Mapper { get; }

        /// <summary>
        /// Gets the PeopleOfData instance.
        /// </summary>
        public PeopleOfData PeopleOfData { get; }

        /// <summary>
        /// Gets the AppDbContext instance.
        /// </summary>
        public BlazingDbContext DbContext { get; }

        /// <summary>
        /// Gets the DependencyInjection instance.
        /// </summary>
        public DependencyInjection InjectServiceDbContext { get; }

        /// <summary>
        /// Gets the ProductInfrastructureRepository instance.
        /// </summary>
        public ProductInfrastructureRepository ProductInfrastructureRepository { get; }

        /// <summary>
        /// Gets the ProductAppService instance.
        /// </summary>
        public ProductAppService ProductAppService { get; }


        //Category

        /// <summary>
        /// Gets the CategoryInfrastructureRepository instance.
        /// </summary>
        public CategoryInfrastructureRepository CategoryInfrastructureRepository { get; }

        /// <summary>
        /// Gets the CategoryAppService instance.
        /// </summary>
        public CategoryAppService CategoryAppService { get; }

        /// <summary>
        /// Initializes a new instance of the ProductRepositoryFixture class.
        /// </summary>
        public RepositoryFixtureTest()
        {
            // Create a new instance of the PeopleOfData class
            PeopleOfData = new PeopleOfData();

            // Create a new instance of the MapperConfiguration class
            var config = new MapperConfiguration(cfg =>
            {
                // Add the BlazingProfile to the MapperConfiguration
                cfg.AddProfile<BlazingProfile>();
            });

            // Create a new instance of the Mapper class
            Mapper = config.CreateMapper();

            // Create a new instance of the MockDb class and create a new instance of the AppDbContext class
            DbContext = new MockDb().CreateDbContext();

            // Create a new instance of the DependencyInjection class
            InjectServiceDbContext = new DependencyInjection(DbContext, Mapper);

            // Create a new instance of the Mock<IProductDomainRepository<Product>> class
            var productRepositoryMock = new Mock<ICrudDomainService<Product>>();

            // Create a new instance of the ProductDomainService class
            var IcrudDomainRepository = new Mock<ICrudDomainService<Product>>();

            // Create a new instance of the ProductAppService class
            ProductAppService = new ProductAppService(Mapper, productRepositoryMock.Object);

            // Create a new instance of the ProductInfrastructureRepository class
            ProductInfrastructureRepository = new ProductInfrastructureRepository(InjectServiceDbContext, ProductAppService);


            //Category


            // Create a new instance of the Mock<ICrudDomainRepository<Category>> class
            var categoryRepositoryMock = new Mock<ICrudDomainService<Category>>();

            // Create a new instance of the CategoryDomainService class
            var categoryDomainServices = new CategoryDomainService();

            // Create a new instance of the CategoryAppService class
            CategoryAppService = new CategoryAppService(categoryDomainServices, Mapper);

            // Create a new instance of the CategoryInfrastructureRepository class
            CategoryInfrastructureRepository = new CategoryInfrastructureRepository(CategoryAppService, InjectServiceDbContext);
        }

        /// <summary>
        /// Disposes the resources used by the ProductRepositoryFixture.
        /// </summary>
        public void Dispose()
        {
            // Dispose the AppDbContext instance
            DbContext.Dispose();
        }
    }
}