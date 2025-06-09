using System.Text.Json.Serialization;

namespace BinanceTR.Models.Private;

public class DepositHistoryList
{
    public DepositHistory[] List { get; set; } = [];

    [JsonIgnore]
    public DepositHistory? LatestDeposit => List.OrderByDescending(d => d.InsertTime).FirstOrDefault();

    [JsonIgnore]
    public DepositHistory? LargestDeposit => List.OrderByDescending(d => d.Amount).FirstOrDefault();

    public IEnumerable<DepositHistory> GetDepositsByAsset(string asset) => List.Where(d => d.Asset.Equals(asset, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<DepositHistory> GetDepositsByNetwork(string network) => List.Where(d => d.Network.Equals(network, StringComparison.OrdinalIgnoreCase));

    public decimal GetTotalAmountByAsset(string asset) => GetDepositsByAsset(asset).Where(d => d.Status == 1).Sum(d => d.Amount);

    public IEnumerable<DepositHistory> GetDepositsByDateRange(DateTime startDate, DateTime endDate) =>
        List.Where(d => d.InsertTimeUTC >= startDate && d.InsertTimeUTC <= endDate);

    public class DepositHistory
    {
        public long Id { get; set; }
        public string Asset { get; set; } = "";
        public string Network { get; set; } = "";
        public string Address { get; set; } = "";
        public string AddressTag { get; set; } = "";
        public string TxId { get; set; } = "";
        public decimal Amount { get; set; }
        public int TransferType { get; set; }
        public int Status { get; set; }
        public long InsertTime { get; set; }
        public int BStatus { get; set; }
        public int SelfReturnStatus { get; set; }
        public string BId { get; set; } = "";
        public int TravelRuleStatus { get; set; }

        [JsonIgnore]
        public DateTime InsertTimeUTC => DateTimeOffset.FromUnixTimeMilliseconds(InsertTime).DateTime;

        [JsonIgnore]
        public bool HasAddressTag => !string.IsNullOrWhiteSpace(AddressTag);

        [JsonIgnore]
        public string FormattedAmount => $"{Amount:N8} {Asset}";

        [JsonIgnore]
        public string NetworkDisplayName => Network switch
        {
            "ETH" => "Ethereum",
            "BSC" => "Binance Smart Chain",
            "AVAXC" => "Avalanche C-Chain",
            "MATIC" => "Polygon",
            "SOL" => "Solana",
            "TRX" => "Tron",
            "XRP" => "XRP Ledger",
            "APT" => "Aptos",
            "BNB" => "Binance Chain",
            _ => Network
        };
    }
}
