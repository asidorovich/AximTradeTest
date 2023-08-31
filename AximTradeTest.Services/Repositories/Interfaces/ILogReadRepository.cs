using AximTradeTest.Models.Models;
using Database.Entities;

namespace AximTradeTest.Services.Repositories.Interfaces;

public interface ILogReadRepository
{
    Task<Range<Log>> SearchAsync(string? search, DateTime? dateFrom, DateTime? dateTo, int skip, int take);

    Task<Log> GetByEventIdAsync(long eventId);
}
