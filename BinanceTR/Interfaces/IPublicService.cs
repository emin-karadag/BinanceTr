using BinanceTR.Core.Constants;
using BinanceTR.Models.Common;
using BinanceTR.Models.Public;

namespace BinanceTR.Interfaces;

public interface IPublicService
{
    Task<BinanceTrResponse<object>?> GetServerTimeAsync(CancellationToken ct = default);
    Task<BinanceTrResponse<SymbolInfo>?> GetSymbolsAsync(CancellationToken ct = default);
    Task<BinanceTrResponse<OrderBook>?> GetOrderBookAsync(string symbol, int limit = BinanceTRConstants.DefaultOrderBookLimit, CancellationToken ct = default);
    Task<BinanceTrResponse<List<RecentTrade>>?> GetRecentTradesAsync(string symbol, int limit = BinanceTRConstants.DefaultTradeLimit, int? fromId = null, CancellationToken ct = default);
    Task<BinanceTrResponse<List<AggregateTrade>>?> GetAggregateTradesAsync(string symbol, int limit = BinanceTRConstants.DefaultTradeLimit, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, int? fromId = null, CancellationToken ct = default);
    Task<BinanceTrResponse<List<Kline>>?> GetKlinesAsync(string symbol, string interval, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, int? limit = null, CancellationToken ct = default);
}
