using AximTradeTest.Models.Models.Journal;
using AximTradeTest.Services.Helpers;
using AximTradeTest.Services.Mappers.Interfaces;
using Database.Entities;

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

    private static string CombineText(Log log)
    {
        var dataValues = JsonHelper.ConvertJsonToString(log.Data);

        var result = $"Request ID = {log.EventId} \r\n" +
            $"Path = {log.Path}\r\n" +
            $"{dataValues}" +
            $"{log.Exception}";

        return result;
    }
}
