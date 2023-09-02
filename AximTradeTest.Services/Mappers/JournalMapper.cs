using AximTradeTest.Models.Models.Journal;
using AximTradeTest.Services.Helpers;
using AximTradeTest.Services.Mappers.Interfaces;
using Database.Entities;

namespace AximTradeTest.Services.Mappers;

public class JournalMapper : IJournalMapper
{
    public JournalData MapData(Log log)
    {
        var journalData = new JournalData
        {
            Id = log.Id,
            EventId = log.EventId.ToString(),
            CreatedAt = log.CreatedAt,
            Text = CombineText(log)
        };

        return journalData;
    }

    public JournalInfoData MapInfoData(Log log)
    {
        var journalInfo = new JournalInfoData
        {
            Id = log.Id,
            EventId = log.EventId.ToString(),
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
