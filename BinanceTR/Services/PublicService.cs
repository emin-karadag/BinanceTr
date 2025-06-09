using BinanceTR.Core.Builders;
using BinanceTR.Core.Constants;
using BinanceTR.Core.Helpers;
using BinanceTR.Interfaces;
using BinanceTR.Models.Common;
using BinanceTR.Models.Public;

namespace BinanceTR.Services;

public class PublicService(BinanceTrHttpClient httpClient) : IPublicService
{
    private readonly BinanceTrHttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<BinanceTrResponse<object>?> GetServerTimeAsync(CancellationToken ct = default)
    {
        return await _httpClient.GetAsync<BinanceTrResponse<object>>(BinanceTRConstants.PublicEndpoints.ServerTime, null, ct: ct);
    }

    public async Task<BinanceTrResponse<SymbolInfo>?> GetSymbolsAsync(CancellationToken ct = default)
    {
        return await _httpClient.GetAsync<BinanceTrResponse<SymbolInfo>>(BinanceTRConstants.PublicEndpoints.Symbols, null, ct: ct);
    }

    public async Task<BinanceTrResponse<OrderBook>?> GetOrderBookAsync(string symbol, int limit = BinanceTRConstants.DefaultOrderBookLimit, CancellationToken ct = default)
    {
        symbol = BinanceTrHelper.NormalizeSymbol(symbol);
        var endpoint = EndpointBuilder.Create(BinanceTRConstants.PublicEndpoints.OrderBook)
            .AddParameter("symbol", symbol)
            .AddParameter("limit", limit)
            .Build();
        var response = await _httpClient.GetAsync<OrderBook>(endpoint, BinanceTRConstants.ApiBaseUrl, ct: ct);
        return new BinanceTrResponse<OrderBook> { Data = response };
    }

    public async Task<BinanceTrResponse<List<RecentTrade>>?> GetRecentTradesAsync(string symbol, int limit = BinanceTRConstants.DefaultTradeLimit, int? fromId = null, CancellationToken ct = default)
    {
        symbol = BinanceTrHelper.NormalizeSymbol(symbol);

        var endpoint = EndpointBuilder.Create(BinanceTRConstants.PublicEndpoints.RecentTrades)
            .AddParameter("symbol", symbol)
            .AddParameter("limit", limit)
            .AddOptionalParameter("fromId", fromId)
            .Build();

        var response = await _httpClient.GetAsync<List<RecentTrade>>(endpoint, BinanceTRConstants.ApiBaseUrl, ct: ct);
        return new BinanceTrResponse<List<RecentTrade>> { Data = response };
    }

    public async Task<BinanceTrResponse<List<AggregateTrade>>?> GetAggregateTradesAsync(string symbol, int limit = BinanceTRConstants.DefaultTradeLimit, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, int? fromId = null, CancellationToken ct = default)
    {
        symbol = BinanceTrHelper.NormalizeSymbol(symbol);

        var endpoint = EndpointBuilder.Create(BinanceTRConstants.PublicEndpoints.AggregateTrades)
            .AddParameter("symbol", symbol)
            .AddParameter("limit", limit)
            .AddOptionalParameter("fromId", fromId)
            .AddDateTimeOffset("startTime", startDate)
            .AddDateTimeOffset("endTime", endDate)
            .Build();

        var response = await _httpClient.GetAsync<List<AggregateTrade>>(endpoint, BinanceTRConstants.ApiBaseUrl, ct: ct);
        return new BinanceTrResponse<List<AggregateTrade>> { Data = response };
    }

    public async Task<BinanceTrResponse<List<Kline>>?> GetKlinesAsync(string symbol, string interval, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, int? limit = null, CancellationToken ct = default)
    {
        symbol = BinanceTrHelper.NormalizeSymbol(symbol);

        var endpoint = EndpointBuilder.Create(BinanceTRConstants.PublicEndpoints.Klines)
            .AddParameter("symbol", symbol)
            .AddParameter("interval", interval.ToLower())
            .AddDateTimeOffset("startTime", startTime)
            .AddDateTimeOffset("endTime", endTime)
            .AddOptionalParameter("limit", limit)
            .Build();

        var response = await _httpClient.GetAsync<List<Kline>>(endpoint, BinanceTRConstants.ApiBaseUrl, ct: ct);
        return new BinanceTrResponse<List<Kline>> { Data = response };
    }
}
