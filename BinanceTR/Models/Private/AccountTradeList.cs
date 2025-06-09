using System.Text.Json.Serialization;

namespace BinanceTR.Models.Private;

public class AccountTradeList
{
    public TradeList[] List { get; set; } = [];
    public IEnumerable<TradeList> GetOrdersBySymbol(string symbol) => List.Where(t => t.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));

    public class TradeList
    {
        public string OrderId { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string BOrderId { get; set; } = "";
        public string BOrderListId { get; set; } = "";
        public string Symbol { get; set; } = "";
        public long SymbolType { get; set; }
        public long Side { get; set; }
        public long Type { get; set; }
        public decimal Price { get; set; }
        public decimal OrigQty { get; set; }
        public decimal OrigQuoteQty { get; set; }
        public decimal ExecutedQty { get; set; }
        public decimal ExecutedPrice { get; set; }
        public decimal ExecutedQuoteQty { get; set; }
        public long TimeInForce { get; set; }
        public decimal StopPrice { get; set; }
        public decimal IcebergQty { get; set; }
        public long Status { get; set; }
        public string Time { get; set; } = "";
        public long CreateTime { get; set; }

        [JsonIgnore]
        public DateTime CreateTimeUTC => DateTimeOffset.FromUnixTimeMilliseconds(CreateTime).DateTime;

        [JsonIgnore]
        public DateTime TimeUTC => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(Time)).DateTime;

        [JsonIgnore]
        public decimal RemainingQty => OrigQty - ExecutedQty;

        [JsonIgnore]
        public decimal FillPercentage => OrigQty > 0 ? (ExecutedQty / OrigQty) * 100 : 0;

        [JsonIgnore]
        public decimal AverageExecutionPrice => ExecutedQty > 0 ? ExecutedQuoteQty / ExecutedQty : 0;

        [JsonIgnore]
        public bool HasExecution => ExecutedQty > 0;

        [JsonIgnore]
        public bool IsPartiallyExecuted => ExecutedQty > 0 && ExecutedQty < OrigQty;

        [JsonIgnore]
        public decimal ExecutionValue => ExecutedQuoteQty;

        [JsonIgnore]
        public string BaseAsset => Symbol.Contains('_') ? Symbol.Split('_')[0] : "";

        [JsonIgnore]
        public string QuoteAsset => Symbol.Contains('_') ? Symbol.Split('_')[1] : "";
    }
}
