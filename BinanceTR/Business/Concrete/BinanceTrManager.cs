using BinanceTR.Business.Abstract;
using BinanceTR.Core.Results.Abstract;
using BinanceTR.Core.Results.Concrete;
using BinanceTR.Core.Utilities;
using BinanceTR.Models;
using BinanceTR.Models.Account;
using BinanceTR.Models.Common;
using BinanceTR.Models.Enums;
using BinanceTR.Models.Order;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BinanceTR.Business.Concrete
{
    public class BinanceTrManager : IBinanceTrService
    {
        private async Task<string> SendRequestAsync(HttpMethod method, string url, BinanceTrOptions options, Dictionary<string, string> parameters = null)
        {
            try
            {
                using var httpClient = new HttpClient();
                var requestUri = BinanceTrHelper.GetRequestUrl(url, true);
                var requestMessage = new HttpRequestMessage(method, requestUri);
                requestMessage.Headers.Add("X-MBX-APIKEY", options.ApiKey);

                if (method == HttpMethod.Get)
                    requestMessage.RequestUri = new Uri(requestMessage.RequestUri.OriginalString + BinanceTrHelper.CreateQueryString(BinanceTrHelper.BuildRequest(options.ApiSecret, parameters)));
                else
                    requestMessage.Content = new FormUrlEncodedContent(BinanceTrHelper.BuildRequest(options.ApiSecret, parameters));

                var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static async Task<string> SendRequestWithoutAuth(string url, Dictionary<string, string> parameters = null, bool baseUrl = false)
        {
            try
            {
                using var httpClient = new HttpClient();
                var requestUri = BinanceTrHelper.GetRequestUrl(url, baseUrl);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                requestMessage.RequestUri = new Uri(requestMessage.RequestUri.OriginalString + BinanceTrHelper.CreateQueryString(BinanceTrHelper.BuildRequest(null, parameters)));

                var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string CheckResult(string result)
        {
            if (result != "[]" && result != "" && result.Contains("msg"))
            {
                if (result.StartsWith("["))
                {
                    var messages = "";
                    JsonSerializer.Deserialize<List<ErrorModel>>(result).ForEach(p => messages += p.Msg + "\n");
                    result = messages;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<ErrorModel>(result);
                    return error.Code > 0 ? error.Msg : result;
                }
            }
            return result;
        }

        public async Task<IDataResult<long>> TestConnectivityAsync()
        {
            try
            {
                var result = await SendRequestWithoutAuth("/open/v1/common/time", null, true).ConfigureAwait(false);
                var data = CheckResult(result);
                var model = JsonSerializer.Deserialize<TimeModel>(data);
                return new SuccessDataResult<long>(model.Timestamp, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<long>(ex.Message);
            }
        }

        public async Task<IDataResult<List<SymbolDataList>>> GetSymbolsAsync()
        {
            try
            {
                var result = await SendRequestWithoutAuth("/open/v1/common/symbols", null, true).ConfigureAwait(false);
                var data = CheckResult(result);
                var model = JsonSerializer.Deserialize<SymbolModel>(data);
                return new SuccessDataResult<List<SymbolDataList>>(model.SymbolData.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<SymbolDataList>>(ex.Message);
            }
        }

        public async Task<IDataResult<OrderBookData>> GetOrderBookAsync(string symbol, int limit = 100)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };
                var result = await SendRequestWithoutAuth("/open/v1/market/depth", parameters, true).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<OrderBookData>(data);

                var model = JsonSerializer.Deserialize<OrderBookModel>(result);
                return new SuccessDataResult<OrderBookData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderBookData>(ex.Message);
            }
        }

        public async Task<IDataResult<List<RecentTradesModel>>> GetRecentTradesAsync(string symbol, int limit = 500)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };
                var result = await SendRequestWithoutAuth("/trades", parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<RecentTradesModel>>(data);

                var model = JsonSerializer.Deserialize<List<RecentTradesModel>>(result);
                return new SuccessDataResult<List<RecentTradesModel>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<RecentTradesModel>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<AggregateTradesModel>>> GetAggregateTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int limit = 500)
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

                var result = await SendRequestWithoutAuth("/aggTrades", parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<AggregateTradesModel>>(data);

                var model = JsonSerializer.Deserialize<List<AggregateTradesModel>>(result);
                return new SuccessDataResult<List<AggregateTradesModel>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AggregateTradesModel>>(ex.Message);
            }
        }

        public async Task<IDataResult<string>> GetKlinesAsync(string symbol, KLineIntervalEnum interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500)
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

                var result = await SendRequestWithoutAuth("/klines", parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<string>(data);

                return new SuccessDataResult<string>(data);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<string>(ex.Message);
            }
        }

        public async Task<IDataResult<List<AccountAsset>>> GetAccountInformationAsync(BinanceTrOptions options)
        {
            try
            {
                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/account/spot", options).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<AccountAsset>>(data);

                var model = JsonSerializer.Deserialize<AccountInformationModel>(result);
                return new SuccessDataResult<List<AccountAsset>>(model.AccountData.AccountAssets, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AccountAsset>>(ex.Message);
            }
        }

        public async Task<IDataResult<AssetInformationData>> GetAssetIformationAsync(BinanceTrOptions options, string assetName)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "asset", assetName }
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/account/spot/asset", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<AssetInformationData>(data);

                var model = JsonSerializer.Deserialize<AssetInformationModel>(result);
                return new SuccessDataResult<AssetInformationData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AssetInformationData>(ex.Message);
            }
        }

        public async Task<IDataResult<LimitOrderData>> PostNewLimitOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal origQuoteQty, decimal price)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", side.GetDisplayName() },
                    { "type", OrderTypeEnum.LIMIT.GetDisplayName() },
                    { "quantity", origQuoteQty.ToString(CultureInfo.InvariantCulture) },
                    { "price", price.ToString(CultureInfo.InvariantCulture) }
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<LimitOrderData>(data);

                var model = JsonSerializer.Deserialize<LimitOrderModel>(result);
                return new SuccessDataResult<LimitOrderData>(model.LimitOrderData, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<LimitOrderData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostBuyMarketOrderAsync(BinanceTrOptions options, string symbol, decimal origQty)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", OrderSideEnum.BUY.GetDisplayName() },
                    { "type", OrderTypeEnum.MARKET.GetDisplayName() },
                    { "quoteOrderQty", origQty.ToString(CultureInfo.InvariantCulture) },
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<PostOrderModelData>(data);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostSellMarketOrderAsync(BinanceTrOptions options, string symbol, decimal origQuoteQty)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", OrderSideEnum.SELL.GetDisplayName() },
                    { "type", OrderTypeEnum.MARKET.GetDisplayName() },
                    { "quantity", origQuoteQty.ToString(CultureInfo.InvariantCulture) },
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<PostOrderModelData>(data);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostStopLimitOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal origQuoteQty, decimal limitPrice, decimal stopPrice)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", side.GetDisplayName() },
                    { "type", OrderTypeEnum.STOP_LOSS_LIMIT.GetDisplayName() },
                    { "quantity", origQuoteQty.ToString(CultureInfo.InvariantCulture) },
                    { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
                    { "price", limitPrice.ToString(CultureInfo.InvariantCulture) },
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<PostOrderModelData>(data);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<OrderDetailData>> GetOrderByIdAsync(BinanceTrOptions options, long orderId)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "orderId", orderId.ToString() }
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders/detail", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<OrderDetailData>(data);

                var model = JsonSerializer.Deserialize<OrderDetailModel>(result);
                return new SuccessDataResult<OrderDetailData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderDetailData>(ex.Message);
            }
        }

        public async Task<IDataResult<CancelOrderData>> CancelOrderByIdAsync(BinanceTrOptions options, long orderId)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "orderId", orderId.ToString() }
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders/cancel", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<CancelOrderData>(data);

                var model = JsonSerializer.Deserialize<CancelOrderModel>(result);
                return new SuccessDataResult<CancelOrderData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CancelOrderData>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() }
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<OpenOrderList>>(data);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<OpenOrderList>>(data);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenBuyOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                    { "side", OrderSideEnum.BUY.GetDisplayName() },
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<OpenOrderList>>(data);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenSellOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                    { "side", OrderSideEnum.SELL.GetDisplayName() },
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders", options, parameters).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<OpenOrderList>>(data);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }
    }
}
