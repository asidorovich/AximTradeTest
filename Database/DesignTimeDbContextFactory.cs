using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AximTradeTestDbContext>
{
    public AximTradeTestDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                                           .SetBasePath(Directory.GetCurrentDirectory())
                                           .AddJsonFile("appsettings.json")
                                           .Build();

        var builder = new DbContextOptionsBuilder<AximTradeTestDbContext>();

        var connectionString = configuration.GetConnectionString("AximTradeTestConnectionString");

        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new AximTradeTestDbContext(builder.Options);
    }
}
