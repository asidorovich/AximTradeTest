using System.Text.Json;
using System.Text.Json.Serialization;

namespace AximTradeTest.Services.Helpers;

public static class JsonHelper
{
    public static string ConvertJsonToString(string? json)
    {
        var result = string.Empty;

        if (!string.IsNullOrEmpty(json))
        {
            var options = new JsonSerializerOptions { UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode };
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);

            if (data != null)
            {
                foreach (var kvp in data)
                {
                    var key = kvp.Key;
                    var value = kvp.Value;

                    result += $"{key} = {value}\r\n";
                }
            }
        }

        return result;
    }
}
