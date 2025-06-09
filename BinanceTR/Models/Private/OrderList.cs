using BinanceTR.Enums;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Private;

public class OrderList
{
    public Order[] List { get; set; } = [];

    [JsonIgnore]
    public IEnumerable<Order> ActiveOrders => List.Where(o => o.Status == OrderStatusEnum.NEW);

    [JsonIgnore]
    public IEnumerable<Order> FilledOrders => List.Where(o => o.Status == OrderStatusEnum.FILLED);

    [JsonIgnore]
    public IEnumerable<Order> CancelledOrders => List.Where(o => o.Status == OrderStatusEnum.CANCELED);

    [JsonIgnore]
    public IEnumerable<Order> PartiallyFilledOrders => List.Where(o => o.Status == OrderStatusEnum.PARTIALLY_FILLED);

    [JsonIgnore]
    public IEnumerable<Order> BuyOrders => List.Where(o => o.Side == OrderSideEnum.BUY);

    [JsonIgnore]
    public IEnumerable<Order> SellOrders => List.Where(o => o.Side == OrderSideEnum.SELL);

    [JsonIgnore]
    public IEnumerable<Order> LimitOrders => List.Where(o => o.Type == OrderTypesEnum.LIMIT);

    [JsonIgnore]
    public IEnumerable<Order> MarketOrders => List.Where(o => o.Type == OrderTypesEnum.MARKET);

    [JsonIgnore]
    public int TotalOrdersCount => List.Length;

    [JsonIgnore]
    public int FilledOrdersCount => FilledOrders.Count();

    [JsonIgnore]
    public int CancelledOrdersCount => CancelledOrders.Count();

    [JsonIgnore]
    public decimal SuccessRate => TotalOrdersCount > 0 ? FilledOrdersCount / (decimal)TotalOrdersCount * 100 : 0;

    [JsonIgnore]
    public decimal TotalExecutedValue => List.Sum(o => o.ExecutionValue);

    [JsonIgnore]
    public decimal TotalBuyValue => BuyOrders.Where(o => o.HasExecution).Sum(o => o.ExecutionValue);

    [JsonIgnore]
    public decimal TotalSellValue => SellOrders.Where(o => o.HasExecution).Sum(o => o.ExecutionValue);

    [JsonIgnore]
    public Order? LatestOrder => List.OrderByDescending(o => o.CreateTime).FirstOrDefault();

    public IEnumerable<Order> GetOrdersBySymbol(string symbol) => List.Where(o => o.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate) =>
        List.Where(o => o.CreateTimeUTC >= startDate && o.CreateTimeUTC <= endDate);

    public IEnumerable<Order> GetOrdersByPriceRange(decimal minPrice, decimal maxPrice) =>
        List.Where(o => o.Price >= minPrice && o.Price <= maxPrice && o.Price > 0);

    public IEnumerable<Order> GetOrdersByStatus(OrderStatusEnum status) => List.Where(o => o.Status == status);

    public IEnumerable<Order> GetOrdersBySide(OrderSideEnum side) => List.Where(o => o.Side == side);

    public IEnumerable<Order> GetOrdersByType(OrderTypesEnum type) => List.Where(o => o.Type == type);

    public decimal GetTotalVolumeBySymbol(string symbol)
    {
        return GetOrdersBySymbol(symbol).Where(o => o.HasExecution).Sum(o => o.ExecutedQty);
    }

    public IEnumerable<Order> GetRecentOrders(int days) =>
        GetOrdersByDateRange(DateTime.UtcNow.AddDays(-days), DateTime.UtcNow);

    public decimal GetRealizedPnL(string symbol)
    {
        var symbolOrders = GetOrdersBySymbol(symbol).Where(o => o.HasExecution).OrderBy(o => o.CreateTime);

        decimal totalCost = 0;
        decimal totalQuantity = 0;
        decimal realizedPnL = 0;

        foreach (var order in symbolOrders)
        {
            if (order.Side == OrderSideEnum.BUY)
            {
                totalCost += order.ExecutionValue;
                totalQuantity += order.ExecutedQty;
            }
            else if (order.Side == OrderSideEnum.SELL && totalQuantity > 0)
            {
                var soldQuantity = Math.Min(order.ExecutedQty, totalQuantity);
                var avgCostPrice = totalQuantity > 0 ? totalCost / totalQuantity : 0;

                realizedPnL += (order.AverageExecutionPrice - avgCostPrice) * soldQuantity;

                totalQuantity -= soldQuantity;
                totalCost -= avgCostPrice * soldQuantity;
            }
        }

        return realizedPnL;
    }
}
