using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Common
{
    public class RecentTradesModel : IBinanceTrModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("price")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal Price { get; set; }

        [JsonPropertyName("qty")]
        [JsonConverter(typeof(StringToDoubleConvertor))]
        public double Qty { get; set; }

        [JsonPropertyName("quoteQty")]
        [JsonConverter(typeof(StringToDoubleConvertor))]
        public double QuoteQty { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("isBuyerMaker")]
        public bool IsBuyerMaker { get; set; }

        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }
    }
}
