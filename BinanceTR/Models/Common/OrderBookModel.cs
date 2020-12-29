using BinanceTR.Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Common
{
    public class OrderBookModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public OrderBookData Data { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class OrderBookData
    {
        [JsonPropertyName("lastUpdateId")]
        public int LastUpdateId { get; set; }

        [JsonPropertyName("bids")]
        public List<List<string>> Bids { get; set; }

        [JsonPropertyName("asks")]
        public List<List<string>> Asks { get; set; }
    }
}
