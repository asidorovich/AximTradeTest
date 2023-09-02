using AximTradeTest.Models.Models.Journal;
using Database.Entities;

namespace AximTradeTest.Services.Mappers.Interfaces;

public interface IJournalMapper
{
    JournalInfoData MapInfoData(Log log);

    JournalData MapData(Log log);
}
