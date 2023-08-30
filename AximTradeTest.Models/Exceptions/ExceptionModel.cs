using System.Text.Json.Serialization;

namespace AximTradeTest.Models.Exceptions;

public class ExceptionModel
{
    public long Id { get; set; }

    public string Type { get; set; }

    [JsonIgnore]
    public object? DataObject { get; set; }

    public Dictionary<string, string> Data { get; set; } = new ();
}
