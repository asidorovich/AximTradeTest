using AximTradeTest.Models.Models;
using AximTradeTest.Services.Repositories.Interfaces;
using Dapper;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace AximTradeTest.Services.Repositories;

public class LogReadRepository : ReadRepository, ILogReadRepository
{
    private readonly DbSet<Log> _dbSet;

    public LogReadRepository(AximTradeTestDbContext dbContext, IConfiguration configuration)
        : base(configuration)
    {
        _dbSet = dbContext.Set<Log>();
    }

    public async Task<Range<Log>> SearchAsync(string? search, DateTime? dateFrom, DateTime? dateTo, int skip, int take)
    {
        var result = new Range<Log>
        {
            Skip = skip
        };

        using var connection = new MySqlConnection(ConnectionString);
        var sqlQuery = @"
SELECT l.id,
	l.event_id AS EventId,
	l.created_at AS CreatedAt
FROM log l
WHERE ((@search IS NULL OR TRIM(@search) = '') OR l.event_id LIKE CONCAT('%', @search, '%'))
	AND (@dateFrom IS NULL OR l.created_at >= @dateFrom)
	AND (@dateTo IS NULL OR l.created_at <= @dateTo)
LIMIT @skip, @take;

SELECT COUNT(id) AS Total 
FROM log;
            ";

        var parameters = new
        {
            search,
            dateFrom,
            dateTo,
            skip,
            take
        };

        using var logEntriesResult = await connection.QueryMultipleAsync(sqlQuery, parameters);
        var logEntries = await logEntriesResult.ReadAsync<Log>();
        var total = await logEntriesResult.ReadSingleAsync<int>();

        result.Items = logEntries;
        result.Count = total;

        return result;
    }
    
    public async Task<Log> GetByEventIdAsync(long eventId)
    {
        var result = await _dbSet.SingleAsync(x => x.EventId == eventId);

        return result;
    }
}
