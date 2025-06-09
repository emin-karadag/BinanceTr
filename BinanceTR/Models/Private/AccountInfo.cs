using System.Text.Json.Serialization;

namespace BinanceTR.Models.Private;

public class AccountInfo
{
    public decimal MakerCommission { get; set; }
    public decimal TakerCommission { get; set; }
    public decimal BuyerCommission { get; set; }
    public decimal SellerCommission { get; set; }
    public int CanTrade { get; set; }
    public int CanWithdraw { get; set; }
    public int CanDeposit { get; set; }
    public int Status { get; set; }
    public AssetInfo[] AccountAssets { get; set; } = [];

    [JsonIgnore]
    public bool IsTradingEnabled => CanTrade == 1;

    [JsonIgnore]
    public bool IsWithdrawEnabled => CanWithdraw == 1;

    [JsonIgnore]
    public bool IsDepositEnabled => CanDeposit == 1;

    [JsonIgnore]
    public bool IsAccountActive => Status == 1;

    [JsonIgnore]
    public IEnumerable<AssetInfo> NonZeroAssets => AccountAssets.Where(a => a.HasBalance);

    [JsonIgnore]
    public IEnumerable<AssetInfo> AssetsWithFreeBalance => AccountAssets.Where(a => a.HasFreeBalance);

    [JsonIgnore]
    public IEnumerable<AssetInfo> AssetsWithLockedBalance => AccountAssets.Where(a => a.HasLockedBalance);

    public AssetInfo? GetAsset(string assetSymbol) => AccountAssets.FirstOrDefault(a => a.Asset.Equals(assetSymbol, StringComparison.OrdinalIgnoreCase));

    public decimal GetTotalBalance(string assetSymbol)
    {
        var asset = GetAsset(assetSymbol);
        return asset?.TotalBalance ?? 0;
    }

    public decimal GetFreeBalance(string assetSymbol)
    {
        var asset = GetAsset(assetSymbol);
        return asset?.Free ?? 0;
    }

    public decimal GetLockedBalance(string assetSymbol)
    {
        var asset = GetAsset(assetSymbol);
        return asset?.Locked ?? 0;
    }

    public bool HasAsset(string assetSymbol) => GetAsset(assetSymbol) != null;

    public bool HasBalanceForAsset(string assetSymbol) => GetAsset(assetSymbol)?.HasBalance ?? false;

    public IEnumerable<AssetInfo> GetAssetsByMinimumBalance(decimal minimumBalance) => AccountAssets.Where(a => a.TotalBalance >= minimumBalance);
}
