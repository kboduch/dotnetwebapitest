using System.Text.Json;
using System.Text.Json.Serialization;

namespace core_web_api_fundamentals.api;

public static class WebSerializer
{
    /**
     * Solves the "Cache and reuse 'JsonSerializerOptions' instances"
     * https://learn.microsoft.com/en-gb/dotnet/fundamentals/code-analysis/quality-rules/ca1869
     */
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true,
        Converters =
        {
            new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.CamelCase, allowIntegerValues: true)
        }
    };

    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, Options);
    }

    public static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, Options);
    }
}
