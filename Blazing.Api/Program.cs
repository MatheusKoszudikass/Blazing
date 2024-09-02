using Blazing.Api.Dependencies;
using Blazing.Api.Middleware;
using Blazing.Ecommerce.Dependency;
using Blazing.Identity.Dependencies;
using Blazing.Identity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddConfigInfraEcommerce(builder.Configuration);
builder.Services.AddConfigInfraIdentity(builder.Configuration);
builder.Services.AddConfigApi(builder.Configuration);

//Add Serilog.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();


builder.Host.UseSerilog();

builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    opt.JsonSerializerOptions.AllowTrailingCommas = true;
    opt.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString |
        JsonNumberHandling.WriteAsString;
    opt.JsonSerializerOptions.IncludeFields = true;
});

// Registrar outras dependências
builder.Services.AddScoped<DependencyInjection>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseStaticFiles();

//app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>(); // <--- Add this line -->

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapIdentityApi<ApplicationUser>();

app.Run();

Log.Fatal(new Exception(), "Blazing API Failed to start");
