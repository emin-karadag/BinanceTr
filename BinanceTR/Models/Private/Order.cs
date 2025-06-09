using BinanceTR.Enums;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Private;

public class Order
{
    public long OrderId { get; set; }
    public string ClientId { get; set; } = "";
    public string Symbol { get; set; } = "";
    public long SymbolType { get; set; }
    public OrderSideEnum Side { get; set; }
    public OrderTypesEnum Type { get; set; }
    public decimal Price { get; set; }
    public decimal OrigQty { get; set; }
    public decimal OrigQuoteQty { get; set; }
    public decimal ExecutedQty { get; set; }
    public decimal ExecutedPrice { get; set; }
    public decimal ExecutedQuoteQty { get; set; }
    public int TimeInForce { get; set; }
    public decimal StopPrice { get; set; }
    public decimal IcebergQty { get; set; }
    public OrderStatusEnum Status { get; set; }
    public int IsWorking { get; set; }
    public long CreateTime { get; set; }
    public long BorderListId { get; set; }
    public long BorderId { get; set; }
    public object? EngineHeaders { get; set; }

    [JsonIgnore]
    public DateTime CreateTimeUTC => DateTimeOffset.FromUnixTimeMilliseconds(CreateTime).DateTime;

    [JsonIgnore]
    public decimal RemainingQty => OrigQty - ExecutedQty;

    [JsonIgnore]
    public decimal FillPercentage => OrigQty > 0 ? ExecutedQty / OrigQty * 100 : 0;

    [JsonIgnore]
    public decimal AverageExecutionPrice => ExecutedQty > 0 ? ExecutedQuoteQty / ExecutedQty : 0;

    [JsonIgnore]
    public bool HasExecution => ExecutedQty > 0;

    [JsonIgnore]
    public bool IsPartiallyExecuted => ExecutedQty > 0 && ExecutedQty < OrigQty;

    [JsonIgnore]
    public decimal ExecutionValue => ExecutedQuoteQty;

    [JsonIgnore]
    public decimal RemainingValue => Type == OrderTypesEnum.MARKET ?
        (OrigQuoteQty - ExecutedQuoteQty) :
        (RemainingQty * Price);

    [JsonIgnore]
    public string BaseAsset => Symbol.Contains('_') ? Symbol.Split('_')[0] : "";

    [JsonIgnore]
    public string QuoteAsset => Symbol.Contains('_') ? Symbol.Split('_')[1] : "";

    [JsonIgnore]
    public string OrderTypeDisplayName => Type switch
    {
        OrderTypesEnum.LIMIT => "Limit",
        OrderTypesEnum.MARKET => "Market",
        OrderTypesEnum.STOP_LOSS => "Stop Loss",
        OrderTypesEnum.STOP_LOSS_LIMIT => "Stop Loss Limit",
        OrderTypesEnum.TAKE_PROFIT => "Take Profit",
        OrderTypesEnum.TAKE_PROFIT_LIMIT => "Take Profit Limit",
        _ => Type.ToString()
    };

    [JsonIgnore]
    public string StatusDisplayName => Status switch
    {
        OrderStatusEnum.NEW => "Aktif",
        OrderStatusEnum.PARTIALLY_FILLED => "Kýsmen Dolmuþ",
        OrderStatusEnum.FILLED => "Tamamen Dolmuþ",
        OrderStatusEnum.CANCELED => "Ýptal Edilmiþ",
        OrderStatusEnum.REJECTED => "Reddedilmiþ",
        OrderStatusEnum.EXPIRED => "Süresi Dolmuþ",
        _ => Status.ToString()
    };

    [JsonIgnore]
    public string FormattedExecutionSummary => HasExecution ?
        $"{ExecutedQty:N8} {BaseAsset} @ {AverageExecutionPrice:N2} {QuoteAsset}" :
        "Gerçekleþmemiþ";

    [JsonIgnore]
    public string FormattedOrderInfo => $"{Side} - {OrderTypeDisplayName} - {Symbol}";
}
