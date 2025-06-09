using BinanceTR.Core.Converters;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Public
{
    public class OrderBook
    {
        public long LastUpdateId { get; set; }

        [JsonPropertyName("bids")]
        public List<OrderBookEntry> Bids { get; set; } = [];

        public List<OrderBookEntry> Asks { get; set; } = [];

        [JsonIgnore]
        public decimal? BestBid => Bids.FirstOrDefault()?.Price;

        [JsonIgnore]
        public decimal? BestAsk => Asks.FirstOrDefault()?.Price;

        [JsonIgnore]
        public decimal? Spread => BestAsk.HasValue && BestBid.HasValue ? BestAsk - BestBid : null;

        [JsonIgnore]
        public decimal? SpreadPercentage => BestBid.HasValue && Spread.HasValue ? (Spread / BestBid) * 100 : null;

        [JsonIgnore]
        public decimal TotalBidVolume => Bids.Sum(b => b.Quantity);

        [JsonIgnore]
        public decimal TotalAskVolume => Asks.Sum(a => a.Quantity);

        public decimal GetBidDepth(int levels)
        {
            return Bids.Take(levels).Sum(b => b.Quantity);
        }

        public decimal GetAskDepth(int levels)
        {
            return Asks.Take(levels).Sum(a => a.Quantity);
        }

        [JsonConverter(typeof(OrderBookEntryArrayConverter))]
        public class OrderBookEntry
        {
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }

            [JsonIgnore]
            public decimal Total => Price * Quantity;
        }
    }
}
