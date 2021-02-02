using BinanceTR.Core.Results.Abstract;
using BinanceTR.Models;
using BinanceTR.Models.Account;
using BinanceTR.Models.Common;
using BinanceTR.Models.Enums;
using BinanceTR.Models.Order;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Abstract
{
    public interface IBinanceTrService
    {
        /// <summary>
        /// Rest Api'ye bağlantıyı test edin ve geçerli sunucu saatini alın.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<long>> TestConnectivityAsync(CancellationToken ct = default);

        /// <summary>
        /// Borsa tarafından desteklenen tüm sembolleri geriye döndürür.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<SymbolDataList>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Belli bir sembolün sipariş defterini alın.
        /// </summary>
        /// <param name="symbol">Sipariş defterini almak istediğiniz sembol. (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan 100, maksimum 5000. Geçerli sınırlar: [5, 10, 20, 50, 100, 500]</param>
        /// <returns></returns>
        Task<IDataResult<OrderBookData>> GetOrderBookAsync(string symbol, int limit = 100, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait son işlemleri alın. (Son 500'e kadar)
        /// </summary>
        /// <param name="symbol">Son işlemleri almak istediğiniz sembol. (Örnek: BTCTRY)</param>
        /// <param name="limit">Almak istediğiniz son işlem adedi. (Varsayılan: 500, Maksimum: 1000)</param>
        /// <returns></returns>
        Task<IDataResult<List<RecentTradesModel>>> GetRecentTradesAsync(string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Sıkıştırılmış/Toplu işlemleri alın. Aynı fiyattan gerçekleşen siparişlerin miktarı toplanmış olacaktır. startTime ve endTime gönderilmezse, en sonki toplu işlemler döndürülür.
        /// </summary>
        /// <param name="symbol">Toplu işlemleri almak istediğiniz sembol. (Örnek: BTCTRY)</param>
        /// <param name="startTime">Başlangıç tarihi</param>
        /// <param name="endTime">Bitiş tarihi</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<AggregateTradesModel>>> GetAggregateTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembol için KLine bilgilerini alın. startTime ve endTime gönderilmezse, en son klineler döndürülür.
        /// </summary>
        /// <param name="symbol">KLine bilgisini almak istediğiniz sembol. (Örnek: BTCTRY)</param>
        /// <param name="interval">Kline aralığı (1m, 1h, 4h, 1w...)</param>
        /// <param name="startTime">Başlangıç süresi</param>
        /// <param name="endTime">Bitiş süresi</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<string>> GetKlinesAsync(string symbol, KLineIntervalEnum interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Mevcut hesabınızın bilgierini alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <returns></returns>
        Task<IDataResult<List<AccountAsset>>> GetAccountInformationAsync(BinanceTrOptions options, CancellationToken ct = default);

        /// <summary>
        /// Hesabınızdaki bir varlığın detaylı bilgisini alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="assetName">Detayını görüntülemek istediğiniz varlığın adı (Örnek: BTC)</param>
        /// <returns></returns>
        Task<IDataResult<AssetInformationData>> GetAssetIformationAsync(BinanceTrOptions options, string assetName, CancellationToken ct = default);

        /// <summary>
        /// Limit tipinde yeni bir sipariş gönderin.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="side">Buy (Al) veya Sell(Sat)</param>
        /// <param name="origQuoteQty">Sipariş adedi (Örnek: 0.000056M, 7, 1.6M)</param>
        /// <param name="price">Sipariş fiyatı</param>
        /// <returns></returns>
        Task<IDataResult<LimitOrderData>> PostNewLimitOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal origQuoteQty, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Market fiyatından alış yapın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="origQty">Sipariş adedi (Örnek: 10 TRY, 20 USDT)</param>
        /// <returns></returns>
        Task<IDataResult<PostOrderModelData>> PostBuyMarketOrderAsync(BinanceTrOptions options, string symbol, decimal origQty, CancellationToken ct = default);

        /// <summary>
        /// Market fiyatından satış yapın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Sipariş sembolü (Örnek: BTC_TRY)</param>
        /// <param name="origQuoteQty">Sipariş adedi (Örnek: 0.000056M, 7, 1.6M)</param>
        /// <returns></returns>
        Task<IDataResult<PostOrderModelData>> PostSellMarketOrderAsync(BinanceTrOptions options, string symbol, decimal origQuoteQty, CancellationToken ct = default);

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
        /// Bir sembole ait tüm açık siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait Al(Buy) tipindeki siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenBuyOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembole ait Sat(Sell) tipindeki siparişleri alın.
        /// </summary>
        /// <param name="options">Binance TR ApiKey ve SecretKey bilgileri</param>
        /// <param name="symbol">Tüm açık siparişlerini almak istediğiniz sembol adı (Örnek: BTC_TRY)</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<OpenOrderList>>> GetAllOpenSellOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default);
    }
}
