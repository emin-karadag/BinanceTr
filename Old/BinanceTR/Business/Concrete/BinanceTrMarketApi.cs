using BinanceTR.Business.Abstract;
using BinanceTR.Core.Results.Abstract;
using BinanceTR.Core.Results.Concrete;
using BinanceTR.Core.Utilities;
using BinanceTR.Models.Enums;
using BinanceTR.Models.Market;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Concrete
{
    public class BinanceTrMarketApi : IBinanceTrMarketApi
    {
        private const string _prefix = "/open/v1/market";

        public async Task<IDataResult<OrderBookData>> GetOrderBookAsync(string symbol, int limit = 100, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };
                var result = await RequestHelper.SendRequestWithoutAuth($"{_prefix}/depth", parameters, true, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<OrderBookData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<OrderBookModel>(result);
                return new SuccessDataResult<OrderBookData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderBookData>(ex.Message);
            }
        }

        public async Task<IDataResult<List<RecentTradesModel>>> GetRecentTradesAsync(string symbol, long? fromId = null, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };

                if (fromId is not null)
                    parameters.Add("fromId", fromId.ToString());

                var result = await RequestHelper.SendRequestWithoutAuth($"/trades", parameters, ct: ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<List<RecentTradesModel>>(data.result, data.code);

                var model = JsonSerializer.Deserialize<List<RecentTradesModel>>(result);
                return new SuccessDataResult<List<RecentTradesModel>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<RecentTradesModel>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<AggregateTradesModel>>> GetAggregateTradesAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };

                if (startTime.HasValue && endTime.HasValue)
                {
                    parameters["startTime"] = BinanceTrHelper.GetTimestamp(startTime.Value).ToString();
                    parameters["endTime"] = BinanceTrHelper.GetTimestamp(endTime.Value).ToString();
                }

                if (fromId is not null)
                    parameters.Add("fromId", fromId.ToString());

                var result = await RequestHelper.SendRequestWithoutAuth($"/aggTrades", parameters, ct: ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<List<AggregateTradesModel>>(data.result, data.code);

                var model = JsonSerializer.Deserialize<List<AggregateTradesModel>>(result);
                return new SuccessDataResult<List<AggregateTradesModel>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AggregateTradesModel>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<KLinesModel>>> GetKlinesAsync(string symbol, KLineIntervalEnum interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()},
                    { "interval", interval.GetDisplayName()},
                };

                if (startTime.HasValue && endTime.HasValue)
                {
                    parameters["startTime"] = BinanceTrHelper.GetTimestamp(startTime.Value).ToString();
                    parameters["endTime"] = BinanceTrHelper.GetTimestamp(endTime.Value).ToString();
                }

                var result = await RequestHelper.SendRequestWithoutAuth($"/klines", parameters, ct: ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<List<KLinesModel>>(data.result, data.code);

                var model = new List<KLinesModel>();
                var jsonList = JsonSerializer.Deserialize<List<List<object>>>(result);

                foreach (var jsonItem in jsonList)
                {
                    var item = new KLinesModel
                    {
                        OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(jsonItem[0].ToString())).DateTime,
                        Open = Convert.ToDecimal(jsonItem[1].ToString(), CultureInfo.InvariantCulture),
                        High = Convert.ToDecimal(jsonItem[2].ToString(), CultureInfo.InvariantCulture),
                        Low = Convert.ToDecimal(jsonItem[3].ToString(), CultureInfo.InvariantCulture),
                        Close = Convert.ToDecimal(jsonItem[4].ToString(), CultureInfo.InvariantCulture),
                        Volume = Convert.ToDecimal(jsonItem[5].ToString(), CultureInfo.InvariantCulture),
                        CloseTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(jsonItem[6].ToString())).DateTime,
                        QuoteAssetVolume = Convert.ToDecimal(jsonItem[7].ToString(), CultureInfo.InvariantCulture),
                        NumberOfTrades = Convert.ToInt32(jsonItem[8].ToString()),
                        TakerBuyBaseAssetVolume = Convert.ToDecimal(jsonItem[9].ToString(), CultureInfo.InvariantCulture),
                        TakerBuyQuoteAssetVolume = Convert.ToDecimal(jsonItem[10].ToString(), CultureInfo.InvariantCulture),
                    };
                    model.Add(item);
                }

                return new SuccessDataResult<List<KLinesModel>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<KLinesModel>>(ex.Message);
            }
        }
    }
}
