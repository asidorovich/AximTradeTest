using AximTradeTest.Models.Models;
using AximTradeTest.Models.Models.Journal;
using AximTradeTest.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AximTradeTest.Controllers;

[ApiController]
[Produces("application/json")]
public class UserJournalController : Controller
{
    private readonly IJournalService _journalService;

    public UserJournalController(IJournalService journalService)
        => (_journalService) = (journalService);


    [HttpPost("/api.user.journal.getRange")]
    public async Task<Range<JournalInfo>> GetJournalRange([FromBody] JournalFilter filter, int skip, int take)
    {
        return new Range<JournalInfo> { };
    }

    [HttpPost("/api.user.journal.getSingle")]
    public async Task<Journal> GetSingleJournalRecord(long id)
    {
        return new Journal();
    }
}
