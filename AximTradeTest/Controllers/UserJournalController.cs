using AximTradeTest.Models.Models;
using AximTradeTest.Models.Models.Journal;
using AximTradeTest.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Controllers;

[Tags("user.journal")]
//[SwaggerTag("This is a test")]
//[ApiExplorerSettings(GroupName = "Group")]
[ApiController]
[Produces("application/json")]
public class UserJournalController : ControllerBase
{
    private readonly IJournalService _journalService;

    public UserJournalController(IJournalService journalService)
        => (_journalService) = (journalService);

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Provides the pagination API. Skip means the number of items should be skipped by server. Take means the maximum number items should be returned by server. All fields of the filter are optional.
    /// </remarks>
    /// <param name="filter"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    [HttpPost("/api.user.journal.getRange")]
    public async Task<Range<JournalInfo>> GetJournalRangeAsync([Required][FromBody] JournalFilter filter, [Required] int skip, [Required] int take)
    {
        var result = await _journalService.SearchJournalEntriesAsync(filter, skip, take);
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Returns the information about an particular event by ID.
    /// </remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("/api.user.journal.getSingle")]
    public async Task<Journal> GetSingleJournalRecordAsync([Required] long id)
    {
        var result = await _journalService.GetJournalEntryAsync(id);
        return result;
    }
}
