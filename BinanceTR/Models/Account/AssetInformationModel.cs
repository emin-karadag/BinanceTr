using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Account
{
    public class AssetInformationModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public AssetInformationData Data { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class AssetInformationData
    {
        [JsonPropertyName("asset")]
        public string Asset { get; set; }

        [JsonPropertyName("free")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal Free { get; set; }

        [JsonPropertyName("locked")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal Locked { get; set; }
    }
}
