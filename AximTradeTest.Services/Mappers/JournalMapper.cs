using AximTradeTest.Models.Models.Journal;
using AximTradeTest.Services.Mappers.Interfaces;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AximTradeTest.Services.Mappers;

public class JournalMapper : IJournalMapper
{
    public Journal Map(Log log)
    {
        var journal = new Journal
        {
            Id = log.Id,
            EventId = log.EventId,
            CreatedAt = log.CreatedAt,
            Text = CombineText(log)
        };

        return journal;
    }

    public JournalInfo MapInfo(Log log)
    {
        var journalInfo = new JournalInfo
        {
            Id = log.Id,
            EventId = log.EventId,
            CreatedAt = log.CreatedAt
        };

        return journalInfo;
    }

    private string CombineText(Log log)
    {
        var result = string.Empty;
        return result;
    }
}
