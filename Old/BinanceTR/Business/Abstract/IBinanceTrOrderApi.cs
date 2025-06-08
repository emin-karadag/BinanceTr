using BinanceTR.Core.Results.Abstract;
using BinanceTR.Models;
using BinanceTR.Models.Enums;
using BinanceTR.Models.Order;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Abstract
{
    public interface IBinanceTrOrderApi
    {
        /// <summary>
        /// Limit tipinde yeni bir sipariş gönderin.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="side">Buy (Al) veya Sell(Sat)</param>
        /// <param name="origQuoteQty">Sipariş adedi (Örnek: 0.000056M, 7, 1.6M)</param>
        /// <param name="price">Sipariş fiyatı</param>
        /// <param name="clientId">Özel sipariş ID</param>
        /// <returns></returns>
        Task<IDataResult<LimitOrderData>> PostNewLimitOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal origQuoteQty, decimal price, string clientId = "", CancellationToken ct = default);

        /// <summary>
        /// Market fiyatından alış yapın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="origQty">Sipariş adedi (Örnek: 10 TRY, 20 USDT)</param>
        /// <returns></returns>
        Task<IDataResult<PostOrderModelData>> PostBuyMarketOrderAsync(BinanceTrOptions options, string symbol, decimal origQty, string clientId = "", CancellationToken ct = default);

        /// <summary>
        /// Market fiyatından satış yapın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="origQuoteQty">Sipariş adedi (Örnek: 0.000056M, 7, 1.6M)</param>
        /// <returns></returns>
        Task<IDataResult<PostOrderModelData>> PostSellMarketOrderAsync(BinanceTrOptions options, string symbol, decimal origQuoteQty, string clientId = "", CancellationToken ct = default);

        /// <summary>
        /// Bir sembol için Stop-Limit emri girin.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="side">Al (Buy) veya Sat (Sell) tipinde sipariş tipi</param>
        /// <param name="origQuoteQty">Sipariş adedi (Örnek: 0.000056M, 7, 1.6M)</param>
        /// <param name="limitPrice">Limit fiyat</param>
        /// <param name="stopPrice">Stop fiyat</param>
        /// <returns></returns>
        Task<IDataResult<PostOrderModelData>> PostStopLimitOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal origQuoteQty, decimal limitPrice, decimal stopPrice, CancellationToken ct = default);

        /// <summary>
        /// Bir siparişin detayını görüntüleyin.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="orderId">Detayını görüntülemek istediğiniz sipariş Id'si</param>
        /// <returns></returns>
        Task<IDataResult<OrderDetailData>> GetOrderByIdAsync(BinanceTrOptions options, long orderId, CancellationToken ct = default);

        /// <summary>
        /// Bir siparişin detayını görüntüleyin.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="clientId">Detayını görüntülemek istediğiniz sipariş Id'si</param>
        /// <returns></returns>
        Task<IDataResult<OrderDetailData>> GetOrderByClientIdAsync(BinanceTrOptions options, string clientId, CancellationToken ct = default);

        /// <summary>
        /// Bir siparişi iptal edin.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="orderId">İptal etmek istediğiniz sipariş Id'si</param>
        /// <returns></returns>
        Task<IDataResult<CancelOrderData>> CancelOrderByIdAsync(BinanceTrOptions options, long orderId, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait tüm siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait alım yönlü tüm geçmiş siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm geçmiş siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllBuyOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait satış yönlü tüm geçmiş siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm geçmiş siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllSellOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait tüm açık siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Açık tüm siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="limit">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenOrdersAsync(BinanceTrOptions options, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait Al(Buy) tipindeki siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenBuyOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Al(Buy) tipindeki tüm açık siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="limit">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenBuyOrdersAsync(BinanceTrOptions options, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait Sat(Sell) tipindeki siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenSellOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Sat(Sell) tipindeki tüm açık siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="limit">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenSellOrdersAsync(BinanceTrOptions options, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait Emir Emiri Bozar (Order Cancel Order [OCO]) tipinde sipariş gönderin.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="side">Al (Buy) veya Sat (Sell) tipinde sipariş tipi</param>
        /// <param name="quantity">Sipariş adedi (Örnek: 0.000056M, 7, 1.6M)</param>
        /// <param name="price">Hedef fiyat</param>
        /// <param name="stopPrice">Zarar durdur fiyatı</param>
        /// <param name="stopLimitPrice">Zarar durdur tetikleme fiyatı</param>
        /// <returns></returns>
        Task<IDataResult<OcoOrderData>> PostOcoOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal quantity, decimal price, decimal stopPrice, decimal stopLimitPrice, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait sipariş bilgilerini alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="orderId"></param>
        /// <param name="startTime">Başlangıç tarihi</param>
        /// <param name="endTime">Bitiş tarihi</param>
        /// <param name="fromId">Sipariş Id. Varsayılan olarak son işlem alınır.</param>
        /// <param name="direct">searching direction: prev - in ascending order from the start order ID; next - in descending order from the start order ID</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<TradeListModelData>> GetAccountTradeListAsync(BinanceTrOptions options, string symbol, string orderId = "", DateTime? startTime = null, DateTime? endTime = null, long fromId = 0, string direct = "", int limit = 500, CancellationToken ct = default);
    }
}
