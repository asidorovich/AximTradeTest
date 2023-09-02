using AximTradeTest.Models.Models;
using AximTradeTest.Models.Models.Journal;

namespace AximTradeTest.Services.Services.Interfaces;

public interface IJournalService
{
    Task<Range<JournalInfoData>> SearchJournalEntriesAsync(JournalFilter filter, int skip, int take);

    Task<JournalData> GetJournalEntryAsync(long eventId);
}
