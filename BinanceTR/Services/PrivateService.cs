using BinanceTR.Core.Builders;
using BinanceTR.Core.Constants;
using BinanceTR.Enums;
using BinanceTR.Interfaces;
using BinanceTR.Models.Common;
using BinanceTR.Models.Private;

namespace BinanceTR.Services;

public class PrivateService(BinanceTrHttpClient httpClient) : IPrivateService
{
    private readonly BinanceTrHttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<BinanceTrResponse<AccountInfo>?> GetAccountInformationAsync(CancellationToken ct = default)
    {
        return await _httpClient.GetAsync<BinanceTrResponse<AccountInfo>>(BinanceTRConstants.PrivateEndpoints.AccountInfo, null, true, ct: ct);
    }

    public async Task<BinanceTrResponse<AssetInfo>?> GetAccountAssetInformationAsync(string asset, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddString("asset", asset)
            .Build();

        return await _httpClient.GetAsync<BinanceTrResponse<AssetInfo>>(BinanceTRConstants.PrivateEndpoints.AccountAsset, parameters, true, ct: ct);
    }

    public async Task<BinanceTrResponse<AccountTradeList>?> GetAccountTradeListAsync(string symbol, string? orderId = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, long? fromId = null, object? direct = null, int limit = BinanceTRConstants.DefaultTradeLimit, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddString("symbol", symbol)
            .AddParameter("limit", limit)
            .AddString("orderId", orderId)
            .AddDateTimeOffset("startTime", startDate)
            .AddDateTimeOffset("endTime", endDate)
            .AddParameter("fromId", fromId)
            .AddParameter("direct", direct)
            .Build();

        return await _httpClient.GetAsync<BinanceTrResponse<AccountTradeList>>(BinanceTRConstants.PrivateEndpoints.Orders, parameters, true, ct: ct);
    }

    public async Task<BinanceTrResponse<DepositHistoryList>?> GetDepositHistoryAsync(string? asset = null, WalletStatusEnum? status = WalletStatusEnum.PENDING, long? fromId = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddString("asset", asset)
            .AddEnum<WalletStatusEnum>("status", status)
            .AddParameter("fromId", fromId)
            .AddDateTimeOffset("startTime", startDate)
            .AddDateTimeOffset("endTime", endDate)
            .Build();

        return await _httpClient.GetAsync<BinanceTrResponse<DepositHistoryList>>(BinanceTRConstants.PrivateEndpoints.Deposits, parameters, true, ct: ct);
    }

    public async Task<BinanceTrResponse<DepositAddress>?> GetDepositAddressAsync(string asset, string network, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddString("asset", asset)
            .AddString("network", network)
            .Build();

        return await _httpClient.GetAsync<BinanceTrResponse<DepositAddress>>(BinanceTRConstants.PrivateEndpoints.DepositAddress, parameters, true, ct: ct);
    }

    public async Task<BinanceTrResponse<Order>?> NewOrderAsync(string symbol, OrderSideEnum side, OrderTypesEnum type, decimal? quantity = null, decimal? quoteOrderQty = null, decimal? price = null, decimal? stopPrice = null, decimal? icebergQty = null, string? clientId = null, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddString("symbol", symbol)
            .AddEnum<OrderSideEnum>("side", side)
            .AddEnum<OrderTypesEnum>("type", type)
            .AddDecimal("quantity", quantity)
            .AddDecimal("quoteOrderQty", quoteOrderQty)
            .AddDecimal("price", price)
            .AddDecimal("stopPrice", stopPrice)
            .AddDecimal("icebergQty", icebergQty)
            .AddString("clientId", clientId)
            .Build();

        return await _httpClient.PostAsync<BinanceTrResponse<Order>>(BinanceTRConstants.PrivateEndpoints.Orders, parameters, true, ct: ct);
    }

    public async Task<BinanceTrResponse<Order>?> GetOrderDetailAsync(long orderId, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddParameter("orderId", orderId)
            .Build();

        return await _httpClient.GetAsync<BinanceTrResponse<Order>>(BinanceTRConstants.PrivateEndpoints.OrderDetail, parameters, true, ct: ct);
    }

    public async Task<BinanceTrResponse<Order>?> CancelOrderAsync(long orderId, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddParameter("orderId", orderId)
            .Build();

        return await _httpClient.PostAsync<BinanceTrResponse<Order>>(BinanceTRConstants.PrivateEndpoints.CancelOrder, parameters, true, ct: ct);
    }

    public async Task<BinanceTrResponse<OrderList>?> GetAllOrdersAsync(string symbol, OrderTypeEnum? type = null, OrderSideEnum? side = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, long? fromId = null, object? direct = null, int limit = BinanceTRConstants.DefaultTradeLimit, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddString("symbol", symbol)
            .AddParameter("limit", limit)
            .AddEnum<OrderTypeEnum>("type", type)
            .AddEnum<OrderSideEnum>("side", side)
            .AddDateTimeOffset("startTime", startDate)
            .AddDateTimeOffset("endTime", endDate)
            .AddParameter("fromId", fromId)
            .AddParameter("direct", direct)
            .Build();

        return await _httpClient.GetAsync<BinanceTrResponse<OrderList>>(BinanceTRConstants.PrivateEndpoints.Orders, parameters, true, ct: ct);
    }

    public async Task<BinanceTrResponse<OcoOrder>?> NewOCOAsync(string symbol, OrderSideEnum side, decimal quantity, decimal price, decimal stopPrice, decimal stopLimitPrice, string? listClientId = null, string? limitClientId = null, string? stopClientId = null, CancellationToken ct = default)
    {
        var parameters = RequestBuilder.Create()
            .AddString("symbol", symbol)
            .AddEnum<OrderSideEnum>("side", side)
            .AddDecimal("quantity", quantity)
            .AddDecimal("price", price)
            .AddDecimal("stopPrice", stopPrice)
            .AddDecimal("stopLimitPrice", stopLimitPrice)
            .AddString("listClientId", listClientId)
            .AddString("limitClientId", limitClientId)
            .AddString("stopClientId", stopClientId)
            .Build();

        return await _httpClient.PostAsync<BinanceTrResponse<OcoOrder>>(BinanceTRConstants.PrivateEndpoints.OcoOrder, parameters, true, ct: ct);
    }
}
