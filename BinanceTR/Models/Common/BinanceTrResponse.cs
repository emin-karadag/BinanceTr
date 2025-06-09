using System.Text.Json.Serialization;

namespace BinanceTR.Models.Common;

public class BinanceTrResponse<T> where T : class
{
    public int Code { get; set; }
    public long Timestamp { get; set; }
    public T? Data { get; set; }

    [JsonPropertyName("msg")]
    public string Message { get; set; } = "";

    [JsonIgnore]
    public string RawResponse { get; set; } = "";

    [JsonIgnore]
    public DateTime DateTimeUTC => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).DateTime;
}
