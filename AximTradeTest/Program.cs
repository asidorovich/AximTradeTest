using Database;
using Microsoft.EntityFrameworkCore;
using AximTradeTest.Services.Repositories;
using AximTradeTest.Services.Repositories.Interfaces;
using AximTradeTest.Services.Services;
using AximTradeTest.Services.Services.Interfaces;
using AximTradeTest.Middlewares;
using Serilog;
using Serilog.Sinks.MariaDB.Extensions;
using System.Diagnostics;
using System.Text.Json;
using AximTradeTest.Services.Mappers.Interfaces;
using AximTradeTest.Services.Mappers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureDb(builder);
ConfigureServices(builder.Services, builder.Configuration);
ConfigureSerilog(builder);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds((type) => type.FullName);
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "AximTradeTest.xml");
    x.IncludeXmlComments(filePath);
    x.EnableAnnotations();

    //x.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));

    //x.TagActionsBy(api => new[] { api.GroupName });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

ApplyDbMigrations(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
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
    ConfigureMapperRegistrations(services);
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
    services.AddTransient<IJournalService, JournalService>();
}

static void ConfigureRepositoryRegistrations(IServiceCollection services)
{
    services.AddTransient<ITreeNodeReadRepository, TreeNodeReadRepository>();
    services.AddTransient<ITreeNodeWriteRepository, TreeNodeWriteRepository>();
    services.AddTransient<ILogReadRepository, LogReadRepository>();
}

static void ConfigureMapperRegistrations(IServiceCollection services)
{
    services.AddTransient<IJournalMapper, JournalMapper>();
}

static void ConfigureSerilog(WebApplicationBuilder builder)
{
    Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
    Serilog.Debugging.SelfLog.Enable(Console.Error);

    builder.Host.UseSerilog((ctx, services, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.MariaDB(
            connectionString: builder.Configuration.GetConnectionString("AximTradeTestConnectionString"),
            tableName: "log",
            autoCreateTable: false,
            options: new Serilog.Sinks.MariaDB.MariaDBSinkOptions
            {
                PropertiesToColumnsMapping = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
                {
                    ["ExeptionEventId"] = "event_id",
                    ["Level"] = "log_level",
                    ["Message"] = "message",
                    ["Exception"] = "exception",
                    ["MessageTemplate"] = null,
                    ["Properties"] = null,
                    ["Path"] = "path",
                    ["Data"] = "data",
                    ["Timestamp"] = "created_at"
                },
                TimestampInUtc = true,
            }),
            writeToProviders: true)
        ;
}

static void ApplyDbMigrations(WebApplication app)
{
    // Migrate latest database changes during startup
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider
        .GetRequiredService<AximTradeTestDbContext>();

    dbContext.Database.Migrate();
}