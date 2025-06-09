using BinanceTR.Core.Constants;
using BinanceTR.Enums;
using BinanceTR.Models.Common;
using BinanceTR.Models.Private;

namespace BinanceTR.Interfaces;

public interface IPrivateService
{
    Task<BinanceTrResponse<AccountInfo>?> GetAccountInformationAsync(CancellationToken ct = default);
    Task<BinanceTrResponse<AssetInfo>?> GetAccountAssetInformationAsync(string asset, CancellationToken ct = default);
    Task<BinanceTrResponse<AccountTradeList>?> GetAccountTradeListAsync(string symbol, string? orderId = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, long? fromId = null, object? direct = null, int limit = BinanceTRConstants.DefaultTradeLimit, CancellationToken ct = default);
    Task<BinanceTrResponse<DepositHistoryList>?> GetDepositHistoryAsync(string? asset = null, WalletStatusEnum? status = WalletStatusEnum.PENDING, long? fromId = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, CancellationToken ct = default);
    Task<BinanceTrResponse<DepositAddress>?> GetDepositAddressAsync(string asset, string network, CancellationToken ct = default);
    Task<BinanceTrResponse<Order>?> NewOrderAsync(string symbol, OrderSideEnum side, OrderTypesEnum type, decimal? quantity = null, decimal? quoteOrderQty = null, decimal? price = null, decimal? stopPrice = null, decimal? icebergQty = null, string? clientId = null, CancellationToken ct = default);
    Task<BinanceTrResponse<Order>?> GetOrderDetailAsync(long orderId, CancellationToken ct = default);
    Task<BinanceTrResponse<Order>?> CancelOrderAsync(long orderId, CancellationToken ct = default);
    Task<BinanceTrResponse<OrderList>?> GetAllOrdersAsync(string symbol, OrderTypeEnum? type = null, OrderSideEnum? side = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, long? fromId = null, object? direct = null, int limit = BinanceTRConstants.DefaultTradeLimit, CancellationToken ct = default);
    Task<BinanceTrResponse<OcoOrder>?> NewOCOAsync(string symbol, OrderSideEnum side, decimal quantity, decimal price, decimal stopPrice, decimal stopLimitPrice, string? listClientId = null, string? limitClientId = null, string? stopClientId = null, CancellationToken ct = default);
}
