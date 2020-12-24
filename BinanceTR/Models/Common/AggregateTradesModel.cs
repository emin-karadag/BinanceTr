using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Common
{
    public class AggregateTradesModel : IBinanceTrModel
    {
        [JsonPropertyName("a")]
        public int TradeId { get; set; }

        [JsonPropertyName("p")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal Price { get; set; }

        [JsonPropertyName("q")]
        [JsonConverter(typeof(StringToDoubleConvertor))]
        public double Quantity { get; set; }

        [JsonPropertyName("f")]
        public int FirstTradeId { get; set; }

        [JsonPropertyName("l")]
        public int LastTradeId { get; set; }

        [JsonPropertyName("T")]
        public long Timestamp { get; set; }

        [JsonPropertyName("m")]
        public bool IsBuyerMaker { get; set; }

        [JsonPropertyName("M")]
        public bool IsBestPriceMatch { get; set; }
    }
}
