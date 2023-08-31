using AximTradeTest.Models.Models;
using AximTradeTest.Models.Models.Journal;

namespace AximTradeTest.Services.Services.Interfaces;

public interface IJournalService
{
    Task<Range<JournalInfo>> SearchJournalEntriesAsync(JournalFilter filter, int skip, int take);

    Task<Journal> GetJournalEntryAsync(long eventId);
}
