using System.Text.Json.Serialization;

namespace BinanceTR.Models.Public
{
    public class RecentTrade
    {
        public long Id { get; set; }
        public decimal? Price { get; set; }
        public decimal? Qty { get; set; }
        public decimal? QuoteQty { get; set; }
        public long Time { get; set; }
        public bool IsBuyerMaker { get; set; }
        public bool IsBestMatch { get; set; }

        [JsonIgnore]
        public DateTime DateTimeUTC => DateTimeOffset.FromUnixTimeMilliseconds(Time).DateTime;
    }
}
