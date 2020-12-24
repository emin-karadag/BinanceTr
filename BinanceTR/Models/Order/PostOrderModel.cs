using BinanceTR.Core.Models;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Order
{
    public class PostOrderModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public PostOrderModelResult Data { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class PostOrderModelResult
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("createTime")]
        public long CreateTime { get; set; }
    }
}
