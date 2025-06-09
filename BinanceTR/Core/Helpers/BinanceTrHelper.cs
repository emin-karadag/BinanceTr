using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Helpers;

public static class BinanceTrHelper
{
    public static JsonSerializerOptions DefaultJsonSerializerOptions { get; } = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        //PropertyNameCaseInsensitive = true,
    };

    public static string NormalizeSymbol(string symbol)
        => symbol.Replace("_", "").ToUpperInvariant();
}
