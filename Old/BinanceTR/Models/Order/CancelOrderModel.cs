using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using BinanceTR.Models.Enums;
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
        public CancelOrderData Data { get; set; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long Timestamp { get; set; }
    }

    public class CancelOrderData
    {
        [JsonPropertyName("orderId")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long OrderId { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(OrderTypeConvertor))]
        public OrderTypeEnum Type { get; set; }

        [JsonPropertyName("side")]
        [JsonConverter(typeof(OrderSideConvertor))]
        public OrderSideEnum Side { get; set; }

        [JsonPropertyName("price")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal Price { get; set; }

        [JsonPropertyName("origQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal OrigQty { get; set; }
    }
}
