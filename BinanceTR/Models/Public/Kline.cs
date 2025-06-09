using BinanceTR.Core.Converters;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Public
{
    [JsonConverter(typeof(KlineArrayConverter))]
    public class Kline
    {
        public long OpenTime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public long CloseTime { get; set; }
        public decimal QuoteAssetVolume { get; set; }
        public int TradeCount { get; set; }
        public decimal TakerBuyBaseAssetVolume { get; set; }
        public decimal TakerBuyQuoteAssetVolume { get; set; }

        [JsonIgnore]
        public DateTime OpenTimeUtc => DateTimeOffset.FromUnixTimeMilliseconds(OpenTime).DateTime;

        [JsonIgnore]
        public DateTime CloseTimeUtc => DateTimeOffset.FromUnixTimeMilliseconds(CloseTime).DateTime;
    }
}
