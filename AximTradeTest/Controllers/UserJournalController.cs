using AximTradeTest.Models.Models;
using AximTradeTest.Models.Models.Journal;
using AximTradeTest.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Controllers;

[ApiController]
[Produces("application/json")]
public class UserJournalController : Controller
{
    private readonly IJournalService _journalService;

    public UserJournalController(IJournalService journalService)
        => (_journalService) = (journalService);


    [HttpPost("/api.user.journal.getRange")]
    public async Task<Range<JournalInfo>> GetJournalRangeASync([FromBody] JournalFilter filter, int skip, int take)
    {
        var result = await _journalService.SearchJournalEntriesAsync(filter, skip, take);
        return result;
    }

    [HttpPost("/api.user.journal.getSingle")]
    public async Task<Journal> GetSingleJournalRecordAsync([Required] long id)
    {
        var result = await _journalService.GetJournalEntryAsync(id);
        return result;
    }
}
