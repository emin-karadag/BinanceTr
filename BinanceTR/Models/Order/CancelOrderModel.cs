using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Order
{
    public class CancelOrderModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public CancelOrderResult Data { get; set; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long Timestamp { get; set; }
    }

    public class CancelOrderResult
    {
        [JsonPropertyName("orderId")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long OrderId { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("side")]
        public int Side { get; set; }

        [JsonPropertyName("price")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal Price { get; set; }

        [JsonPropertyName("origQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal OrigQty { get; set; }
    }
}
