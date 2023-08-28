using Database;
using Microsoft.EntityFrameworkCore;
using AximTradeTest.Services.Repositories;
using AximTradeTest.Services.Repositories.Interfaces;
using AximTradeTest.Services.Services;
using AximTradeTest.Services.Services.Interfaces;
using AximTradeTest.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureDb(builder);
ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddHealthChecks();

    ConfigureServiceRegistrations(services);
    ConfigureRepositoryRegistrations(services);
}

static void ConfigureDb(WebApplicationBuilder builder)
{
    string mySqlConnectionStr = builder.Configuration.GetConnectionString("AximTradeTestConnectionString");
    builder.Services.AddDbContextPool<AximTradeTestDbContext>(options
        => options
            .UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr))
            .UseLazyLoadingProxies());
}


static void ConfigureServiceRegistrations(IServiceCollection services)
{
    services.AddTransient<ITreeNodeService, TreeNodeService>();
}

static void ConfigureRepositoryRegistrations(IServiceCollection services)
{
    services.AddTransient<ITreeNodeReadRepository, TreeNodeReadRepository>();
    services.AddTransient<ITreeNodeWriteRepository, TreeNodeWriteRepository>();
}
