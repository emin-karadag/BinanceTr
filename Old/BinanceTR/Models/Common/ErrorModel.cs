using System.Text.Json.Serialization;

namespace BinanceTR.Models.Common
{
    public class ErrorModel
    {
        [JsonPropertyName("code")]
        public long Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
