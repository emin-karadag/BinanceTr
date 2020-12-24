using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Order
{
    public class AllOrdersModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public OpenOrdersResult Data { get; set; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long Timestamp { get; set; }
    }

    public class OpenOrdersResult
    {
        [JsonPropertyName("list")]
        public List<OpenOrderList> List { get; set; }
    }

    public class OpenOrderList
    {
        [JsonPropertyName("orderId")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long OrderId { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("symbolType")]
        public int SymbolType { get; set; }

        [JsonPropertyName("side")]
        public int Side { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("price")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal Price { get; set; }

        [JsonPropertyName("origQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal OrigQty { get; set; }

        [JsonPropertyName("origQuoteQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal OrigQuoteQty { get; set; }

        [JsonPropertyName("executedQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal ExecutedQty { get; set; }

        [JsonPropertyName("executedPrice")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal ExecutedPrice { get; set; }

        [JsonPropertyName("executedQuoteQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal ExecutedQuoteQty { get; set; }

        [JsonPropertyName("timeInForce")]
        public int TimeInForce { get; set; }

        [JsonPropertyName("stopPrice")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal StopPrice { get; set; }

        [JsonPropertyName("icebergQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal IcebergQty { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("createTime")]
        [JsonConverter(typeof(StringToLongConvertor))]
        public long CreateTime { get; set; }

        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }
    }
}
