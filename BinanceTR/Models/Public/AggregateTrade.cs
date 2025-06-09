using System.Text.Json.Serialization;

namespace BinanceTR.Models.Public
{
    public class AggregateTrade
    {
        [JsonPropertyName("a")]
        public long TradeId { get; set; }

        [JsonPropertyName("p")]
        public decimal Price { get; set; }

        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }

        [JsonPropertyName("f")]
        public long FirstTradeId { get; set; }

        [JsonPropertyName("l")]
        public long LastTradeId { get; set; }

        [JsonPropertyName("T")]
        public long Timestamp { get; set; }

        [JsonPropertyName("m")]
        public bool IsBuyerMaker { get; set; }

        [JsonPropertyName("M")]
        public bool IsBestPriceMatch { get; set; }

        [JsonIgnore]
        public DateTime DateTimeUTC => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).DateTime;
    }
}
