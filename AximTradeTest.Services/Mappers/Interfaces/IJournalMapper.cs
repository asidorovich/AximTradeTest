using AximTradeTest.Models.Models.Journal;
using Database.Entities;

namespace AximTradeTest.Services.Mappers.Interfaces;

public interface IJournalMapper
{
    JournalInfo MapInfo(Log log);

    Journal Map(Log log);
}
