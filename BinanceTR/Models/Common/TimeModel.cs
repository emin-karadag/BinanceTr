using BinanceTR.Core.Models;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Common
{
    public class TimeModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public long Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
