using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Account
{
    public class AccountInformationModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public Data AccountData { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("makerCommission")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal MakerCommission { get; set; }

        [JsonPropertyName("takerCommission")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal TakerCommission { get; set; }

        [JsonPropertyName("buyerCommission")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal BuyerCommission { get; set; }

        [JsonPropertyName("sellerCommission")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal SellerCommission { get; set; }

        [JsonPropertyName("canTrade")]
        public int CanTrade { get; set; }

        [JsonPropertyName("canWithdraw")]
        public int CanWithdraw { get; set; }

        [JsonPropertyName("canDeposit")]
        public int CanDeposit { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("accountAssets")]
        public List<AccountAsset> AccountAssets { get; set; }
    }

    public class AccountAsset
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
