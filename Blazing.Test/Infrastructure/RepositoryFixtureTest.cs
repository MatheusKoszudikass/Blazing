using AutoMapper;
using Blazing.Application.Mappings;
using Blazing.Application.Services;
using Blazing.Domain.Services;
using Blazing.Ecommerce.Data;
using Blazing.Ecommerce.Dependency;
using Blazing.Ecommerce.Repository;
using Blazing.Ecommerce.Service;
using Blazing.Identity.Data;
using Blazing.Identity.Entities;
using Blazing.Identity.Mappings;
using Blazing.Test.Data;
using BlazingPizzaTest.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using UserInfrastructureRepository = Blazing.Identity.Service.UserInfrastructureRepository;

namespace Blazing.Test.Infrastructure;

/// <summary>
///     Represents a fixture for testing the ProductRepository.
/// </summary>
public class RepositoryFixtureTest : IDisposable
{
    private bool _disposed;


    private readonly IMemoryCache _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
    /// <summary>
    ///     Gets the AutoMapper instance.
    /// </summary>
    public IMapper Mapper { get; }

    /// <summary>
    ///     Gets the PeopleOfData instance.
    /// </summary>
    public PeopleOfData PeopleOfData { get; }

    /// <summary>
    ///     Gets the AppDbContext instance.
    /// </summary>
    public BlazingDbContext DbContext { get; }

    /// <summary>
    ///     Gets the DependencyInjection instance.
    /// </summary>
    public DependencyInjection InjectServiceDbContext { get; }

    /// <summary>
    ///     Gets the ProductInfrastructureRepository instance.
    /// </summary>
    public ProductInfrastructureRepository ProductInfrastructureRepository { get; }

    /// <summary>
    ///     Gets the ProductAppService instance.
    /// </summary>
    public ProductAppService ProductAppService { get; }

    public ProductDomainService ProductDomainService { get; }


    //Category

    /// <summary>
    ///     Gets the CategoryInfrastructureRepository instance.
    /// </summary>
    public CategoryInfrastructureRepository CategoryInfrastructureRepository { get; }

    /// <summary>
    ///     Gets the CategoryAppService instance.
    /// </summary>
    public CategoryAppService CategoryAppService { get; }

    public CategoryDomainService CategoryDomainService { get; }


    //User Identity

    public BlazingIdentityDbContext BlazingIdentityDbContext { get; }
    public UserInfrastructureRepository UserInfrastructureRepository { get; }

    public Identity.Dependency.DependencyInjection InjectServiceIdentityDbContext { get; }

    public UserManager<ApplicationUser> UserManagerIdentity { get; }

    public SignInManager<ApplicationUser> SignInManagerIdentity { get; }

    public BlazingIdentityMapper IdentityMapper { get; }


    //User Ecommerce

    public Ecommerce.Service.UserInfrastructureRepository UserEcommerceRepository { get; }

    public UserDomainService UserDomainService { get; }

    public UserAppService UserAppService { get; }


    /// <summary>
    ///     Initializes a new instance of the ProductRepositoryFixture class.
    /// </summary>
    public RepositoryFixtureTest()
    {
        //Product

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

        // Create a new instance of the MockDb class and create a new instance of the BlazingDbContext class
        DbContext = new MockDb().CreateDbContext();

        // Create a new instance of the DependencyInjection class
        InjectServiceDbContext = new DependencyInjection(DbContext, Mapper);


        //Product 

        // Create a new instance of the ProductDomainService class
        ProductDomainService = new ProductDomainService();

        // Create a new instance of the ProductAppService class
        ProductAppService = new ProductAppService(Mapper, ProductDomainService);

        // Create a new instance of the ProductInfrastructureRepository class
        ProductInfrastructureRepository =
            new ProductInfrastructureRepository(_memoryCache, InjectServiceDbContext, ProductAppService);

        //Category

        // Create a new instance of the CategoryDomainService class
        CategoryDomainService = new CategoryDomainService();

        // Create a new instance of the CategoryAppService class
        CategoryAppService = new CategoryAppService(CategoryDomainService, Mapper);

        // Create a new instance of the CategoryInfrastructureRepository class
        CategoryInfrastructureRepository =
            new CategoryInfrastructureRepository(CategoryAppService, InjectServiceDbContext);

        //User Ecommerce

        // Cria um mock de ICrudDomainService<User>
        UserDomainService = new UserDomainService();

        // Cria a instância do UserAppService com os mocks injetados
        UserAppService = new UserAppService(Mapper, UserDomainService);

        UserEcommerceRepository =
            new Ecommerce.Service.UserInfrastructureRepository(UserAppService, InjectServiceDbContext);

        //User Identity

        // Create a new instance of the DependencyInjection class
        InjectServiceIdentityDbContext = new Identity.Dependency.DependencyInjection(BlazingIdentityDbContext, Mapper);

        //Create a new instance of the Mock<IUserInfrastructureRepository> class
        var userRepositoryMock = new Mock<IUserInfrastructureRepository>();

        UserManagerIdentity = GetUserManager();

        SignInManagerIdentity = GetSignInManager();

        IdentityMapper = new BlazingIdentityMapper();

        UserInfrastructureRepository = new UserInfrastructureRepository(InjectServiceIdentityDbContext,
            UserManagerIdentity, SignInManagerIdentity, IdentityMapper, userRepositoryMock.Object);
    }

    /// <summary>
    ///     Disposes the resources used by the ProductRepositoryFixture.
    /// </summary>
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


    public static UserManager<ApplicationUser> GetUserManager()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        var identityOptions = new Mock<IOptions<IdentityOptions>>();
        var passwordHasher = new Mock<IPasswordHasher<ApplicationUser>>();
        var userValidators = new List<IUserValidator<ApplicationUser>>
            { new Mock<IUserValidator<ApplicationUser>>().Object };
        var passwordValidators = new List<IPasswordValidator<ApplicationUser>>
            { new Mock<IPasswordValidator<ApplicationUser>>().Object };
        var keyNormalizer = new Mock<ILookupNormalizer>();
        var errors = new Mock<IdentityErrorDescriber>();
        var services = new Mock<IServiceProvider>();
        var logger = new Mock<ILogger<UserManager<ApplicationUser>>>();

        return new UserManager<ApplicationUser>(
            store.Object,
            identityOptions.Object,
            passwordHasher.Object,
            userValidators,
            passwordValidators,
            keyNormalizer.Object,
            errors.Object,
            services.Object,
            logger.Object);
    }

    public static SignInManager<ApplicationUser> GetSignInManager()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();

        var claimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

        var option = new Mock<IOptions<IdentityOptions>>();

        var logger = new Mock<ILogger<SignInManager<ApplicationUser>>>();

        var authentication = new Mock<IAuthenticationSchemeProvider>();

        var userConfirmation = new Mock<IUserConfirmation<ApplicationUser>>();

        return new SignInManager<ApplicationUser>(
            GetUserManager(),
            httpContextAccessor.Object,
            claimsPrincipalFactory.Object,
            option.Object,
            logger.Object,
            authentication.Object,
            userConfirmation.Object);
    }
}