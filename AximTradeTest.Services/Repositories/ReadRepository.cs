using Microsoft.Extensions.Configuration;

namespace AximTradeTest.Services.Repositories;

public class ReadRepository
{
    protected string ConnectionString { get; }

    public ReadRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("AximTradeTestConnectionString") ?? string.Empty;
    }
}
