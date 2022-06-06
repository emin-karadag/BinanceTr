using BinanceTR.Business.Abstract;
using BinanceTR.Core.Results.Abstract;
using BinanceTR.Core.Results.Concrete;
using BinanceTR.Core.Utilities;
using BinanceTR.Models.Enums;
using BinanceTR.Models.Market;
using System;
using System.Collections.Generic;
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

        public async Task<IDataResult<List<RecentTradesModel>>> GetRecentTradesAsync(string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };
                var result = await RequestHelper.SendRequestWithoutAuth($"{_prefix}/trades", parameters, ct: ct).ConfigureAwait(false);
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

        public async Task<IDataResult<List<AggregateTradesModel>>> GetAggregateTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default)
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

                var result = await RequestHelper.SendRequestWithoutAuth($"{_prefix}/agg-trades", parameters, ct: ct).ConfigureAwait(false);
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

        public async Task<IDataResult<string>> GetKlinesAsync(string symbol, KLineIntervalEnum interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default)
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

                var result = await RequestHelper.SendRequestWithoutAuth($"{_prefix}/klines", parameters, ct: ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<string>(data.result, data.code);

                return new SuccessDataResult<string>(data.result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<string>(ex.Message);
            }
        }
    }
}
