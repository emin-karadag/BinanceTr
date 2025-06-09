using System.Text.Json.Serialization;

namespace BinanceTR.Models.Private;

public class AssetInfo
{
    public string Asset { get; set; } = "";
    public decimal Free { get; set; }
    public decimal Locked { get; set; }

    [JsonIgnore]
    public decimal TotalBalance => Free + Locked;

    [JsonIgnore]
    public bool HasBalance => TotalBalance > 0;

    [JsonIgnore]
    public bool HasFreeBalance => Free > 0;

    [JsonIgnore]
    public bool HasLockedBalance => Locked > 0;

    [JsonIgnore]
    public decimal FreePercentage => TotalBalance > 0 ? Free / TotalBalance * 100 : 0;

    [JsonIgnore]
    public decimal LockedPercentage => TotalBalance > 0 ? Locked / TotalBalance * 100 : 0;
}
