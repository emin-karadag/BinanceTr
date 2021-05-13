using BinanceTR.Core.Models;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Order
{
    public class OcoOrderModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public OcoOrderData OcoOrderData { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class OcoOrderData
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("createTime")]
        public long CreateTime { get; set; }
    }
}
