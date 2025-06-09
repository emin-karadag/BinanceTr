using System.Text.Json.Serialization;

namespace BinanceTR.Models.Private;

public class OcoOrder
{
    public long ListClientId { get; set; }
    public string Symbol { get; set; } = "";
    public long SymbolType { get; set; }
    public string ContingencyType { get; set; } = "";
    public string ListStatusType { get; set; } = "";
    public string ListOrderStatus { get; set; } = "";
    public long CreateTime { get; set; }
    public Order[] Orders { get; set; } = [];
    public long BorderListId { get; set; }

    [JsonIgnore]
    public DateTime CreateTimeUTC => DateTimeOffset.FromUnixTimeMilliseconds(CreateTime).DateTime;
}
