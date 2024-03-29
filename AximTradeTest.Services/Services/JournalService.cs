﻿using AximTradeTest.Models.Exceptions;
using AximTradeTest.Models.Models;
using AximTradeTest.Models.Models.Journal;
using AximTradeTest.Services.Mappers.Interfaces;
using AximTradeTest.Services.Repositories.Interfaces;
using AximTradeTest.Services.Services.Interfaces;

namespace AximTradeTest.Services.Services;

public class JournalService : IJournalService
{
    private readonly ILogReadRepository _logRepository;
    private readonly IJournalMapper _journalMapper;

    public JournalService(ILogReadRepository logRepository, IJournalMapper journalMapper)
        => (_logRepository, _journalMapper) = (logRepository, journalMapper);

    public async Task<Range<JournalInfoData>> SearchJournalEntriesAsync(JournalFilter filter, int skip, int take)
    {
        var result = new Range<JournalInfoData>();

        var logEntriesRange = await _logRepository.SearchAsync(filter.Search, filter.From, filter.To, skip, take);

        if(logEntriesRange.Items?.Any() == true)
        {
            var journalInfoEntries = new List<JournalInfoData>();

            foreach(var logEntry in logEntriesRange.Items)
            {
                var journalInfo = _journalMapper.MapInfoData(logEntry);
                journalInfoEntries.Add(journalInfo);
            }

            result.Items = journalInfoEntries;
        }

        result.Skip = logEntriesRange.Skip;
        result.Count = logEntriesRange.Count;

        return result;
    }

    public async Task<JournalData> GetJournalEntryAsync(long eventId)
    {
        var log = await _logRepository.GetByEventIdAsync(eventId);

        var result = _journalMapper.MapData(log);

        return result;
    }
}
