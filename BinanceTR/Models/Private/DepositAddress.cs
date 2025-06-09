using System.Globalization;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Private;

public class DepositAddress
{
    public long Uid { get; set; }
    public string Asset { get; set; } = "";
    public string Network { get; set; } = "";
    public string Address { get; set; } = "";
    public string AddressTag { get; set; } = "";
    public long Status { get; set; }
    public string UpdateTime { get; set; } = "";
    public decimal MinimumDepositAmount { get; set; }

    [JsonIgnore]
    public DateTime? UpdateTimeUTC
    {
        get
        {
            if (DateTime.TryParseExact(UpdateTime, "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result))
            {
                return result.ToUniversalTime();
            }
            return null;
        }
    }

    [JsonIgnore]
    public bool HasAddressTag => !string.IsNullOrWhiteSpace(AddressTag);

    [JsonIgnore]
    public string DisplayAddress => HasAddressTag ? $"{Address} (Tag: {AddressTag})" : Address;

    [JsonIgnore]
    public bool HasMinimumDeposit => MinimumDepositAmount > 0;

    [JsonIgnore]
    public string FormattedMinimumDeposit => $"{MinimumDepositAmount:N8} {Asset}";

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
