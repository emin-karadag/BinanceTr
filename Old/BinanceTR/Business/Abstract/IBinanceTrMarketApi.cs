using BinanceTR.Core.Results.Abstract;
using BinanceTR.Models.Enums;
using BinanceTR.Models.Market;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Abstract
{
    public interface IBinanceTrMarketApi
    {
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
        /// <param name="fromId">ID</param>
        /// <returns></returns>
        Task<IDataResult<List<RecentTradesModel>>> GetRecentTradesAsync(string symbol, long? fromId = null, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Sıkıştırılmış/Toplu işlemleri alın. Aynı fiyattan gerçekleşen siparişlerin miktarı toplanmış olacaktır. startTime ve endTime gönderilmezse, en sonki toplu işlemler döndürülür.
        /// </summary>
        /// <param name="symbol">Toplu işlemleri almak istediğiniz sembol. (Örnek: BTCTRY)</param>
        /// <param name="fromId">ID</param>
        /// <param name="startTime">Başlangıç tarihi</param>
        /// <param name="endTime">Bitiş tarihi</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<AggregateTradesModel>>> GetAggregateTradesAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default);

        /// <summary>
        /// Bir sembol için KLine bilgilerini alın. startTime ve endTime gönderilmezse, en son klineler döndürülür.
        /// </summary>
        /// <param name="symbol">KLine bilgisini almak istediğiniz sembol. (Örnek: BTCTRY)</param>
        /// <param name="interval">Kline aralığı (1m, 1h, 4h, 1w...)</param>
        /// <param name="startTime">Başlangıç süresi</param>
        /// <param name="endTime">Bitiş süresi</param>
        /// <param name="limit">Varsayılan: 500, Maksimum: 1000</param>
        /// <returns></returns>
        Task<IDataResult<List<KLinesModel>>> GetKlinesAsync(string symbol, KLineIntervalEnum interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default);
    }
}
