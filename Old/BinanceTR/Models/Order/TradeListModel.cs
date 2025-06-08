using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Order
{
    public class TradeListModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public TradeListModelData Data { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class TradeListModelData
    {
        [JsonPropertyName("tradeId")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long TradeId { get; set; }

        [JsonPropertyName("orderId")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long OrderId { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("price")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public string Price { get; set; }

        [JsonPropertyName("qty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public string Qty { get; set; }

        [JsonPropertyName("quoteQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public string QuoteQty { get; set; }

        [JsonPropertyName("commission")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public string Commission { get; set; }

        [JsonPropertyName("commissionAsset")]
        public string CommissionAsset { get; set; }

        [JsonPropertyName("isBuyer")]
        public bool IsBuyer { get; set; }

        [JsonPropertyName("isMaker")]
        public bool IsMaker { get; set; }

        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }

        [JsonPropertyName("time")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long Time { get; set; }
    }
}
