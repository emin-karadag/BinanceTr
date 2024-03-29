﻿using BinanceTR.Business.Abstract;
using BinanceTR.Core.Results.Abstract;
using BinanceTR.Core.Results.Concrete;
using BinanceTR.Core.Utilities;
using BinanceTR.Models;
using BinanceTR.Models.Enums;
using BinanceTR.Models.Order;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Concrete
{
    public class BinanceTrOrderApi : IBinanceTrOrderApi
    {
        private const string _prefix = "/open/v1/orders";

        public async Task<IDataResult<LimitOrderData>> PostNewLimitOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal origQuoteQty, decimal price, string clientId = "", CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", side.GetDisplayName() },
                    { "type", OrderTypeEnum.LIMIT.GetDisplayName() },
                    { "quantity", origQuoteQty.ToString(CultureInfo.InvariantCulture) },
                    { "price", price.ToString(CultureInfo.InvariantCulture) },
                    { "clientId", clientId }
                };

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Post, _prefix, options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<LimitOrderData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<LimitOrderModel>(result);
                return new SuccessDataResult<LimitOrderData>(model.LimitOrderData, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<LimitOrderData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostBuyMarketOrderAsync(BinanceTrOptions options, string symbol, decimal origQty, string clientId = "", CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", OrderSideEnum.BUY.GetDisplayName() },
                    { "type", OrderTypeEnum.MARKET.GetDisplayName() },
                    { "quoteOrderQty", origQty.ToString(CultureInfo.InvariantCulture) },
                    { "clientId", clientId },
                };

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Post, _prefix, options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<PostOrderModelData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostSellMarketOrderAsync(BinanceTrOptions options, string symbol, decimal origQuoteQty, string clientId = "", CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", OrderSideEnum.SELL.GetDisplayName() },
                    { "type", OrderTypeEnum.MARKET.GetDisplayName() },
                    { "quantity", origQuoteQty.ToString(CultureInfo.InvariantCulture) },
                    { "clientId", clientId },
                };

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Post, _prefix, options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<PostOrderModelData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostStopLimitOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal origQuoteQty, decimal limitPrice, decimal stopPrice, CancellationToken ct = default)
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

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Post, _prefix, options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<PostOrderModelData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<OrderDetailData>> GetOrderByIdAsync(BinanceTrOptions options, long orderId, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "orderId", orderId.ToString() }
                };

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Get, $"{_prefix}/detail", options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<OrderDetailData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<OrderDetailModel>(result);
                return new SuccessDataResult<OrderDetailData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderDetailData>(ex.Message);
            }
        }

        public async Task<IDataResult<OrderDetailData>> GetOrderByClientIdAsync(BinanceTrOptions options, string clientId, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "clientId", clientId }
                };

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Get, $"{_prefix}/detail", options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<OrderDetailData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<OrderDetailModel>(result);
                return new SuccessDataResult<OrderDetailData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderDetailData>(ex.Message);
            }
        }

        public async Task<IDataResult<CancelOrderData>> CancelOrderByIdAsync(BinanceTrOptions options, long orderId, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "orderId", orderId.ToString() }
                };

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Post, $"{_prefix}/cancel", options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<CancelOrderData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<CancelOrderModel>(result);
                return new SuccessDataResult<CancelOrderData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CancelOrderData>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.History.GetDisplayName() },
                };

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllBuyOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.History.GetDisplayName() },
                    { "side", OrderSideEnum.BUY.GetDisplayName() },
                };

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllSellOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.History.GetDisplayName() },
                    { "side", OrderSideEnum.SELL.GetDisplayName() },
                };

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                };

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenOrdersAsync(BinanceTrOptions options, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                };

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenBuyOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default)
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

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenBuyOrdersAsync(BinanceTrOptions options, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                    { "side", OrderSideEnum.BUY.GetDisplayName() },
                };

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenSellOrdersAsync(BinanceTrOptions options, string symbol, int limit = 500, CancellationToken ct = default)
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

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenSellOrdersAsync(BinanceTrOptions options, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                    { "side", OrderSideEnum.SELL.GetDisplayName() },
                };

                return await GetAllOrdersAsync(options, parameters, ct);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        private async Task<IDataResult<List<OpenOrderList>>> GetAllOrdersAsync(BinanceTrOptions options, Dictionary<string, string> parameters, CancellationToken ct = default)
        {
            try
            {
                var result = await RequestHelper.SendRequestAsync(HttpMethod.Get, _prefix, options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<List<OpenOrderList>>(data.result, data.code);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<OcoOrderData>> PostOcoOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal quantity, decimal price, decimal stopPrice, decimal stopLimitPrice, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", side.GetDisplayName() },
                    { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                    { "price", price.ToString(CultureInfo.InvariantCulture) },
                    { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
                    { "stopLimitPrice", stopLimitPrice.ToString(CultureInfo.InvariantCulture) }
                };

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Post, $"{_prefix}/oco", options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<OcoOrderData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<OcoOrderModel>(result);
                return new SuccessDataResult<OcoOrderData>(model.OcoOrderData, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OcoOrderData>(ex.Message);
            }
        }

        public async Task<IDataResult<TradeListModelData>> GetAccountTradeListAsync(BinanceTrOptions options, string symbol, string orderId = "", DateTime? startTime = null, DateTime? endTime = null, long fromId = 0, string direct = "", int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "orderId", orderId },
                    { "fromId", fromId.ToString() },
                    { "direct", direct },
                    { "limit", limit.ToString() }
                };

                if (startTime.HasValue && endTime.HasValue)
                {
                    parameters["startTime"] = BinanceTrHelper.GetTimestamp(startTime.Value).ToString();
                    parameters["endTime"] = BinanceTrHelper.GetTimestamp(endTime.Value).ToString();
                }

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Get, $"{_prefix}/trades", options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                if (!BinanceTrHelper.IsJson(data.result))
                    return new ErrorDataResult<TradeListModelData>(data.result, data.code);

                var model = JsonSerializer.Deserialize<TradeListModel>(result);
                return new SuccessDataResult<TradeListModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TradeListModelData>(ex.Message);
            }
        }
    }
}
