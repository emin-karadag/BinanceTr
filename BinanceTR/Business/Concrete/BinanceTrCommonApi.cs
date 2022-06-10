using BinanceTR.Business.Abstract;
using BinanceTR.Core.Results.Abstract;
using BinanceTR.Core.Results.Concrete;
using BinanceTR.Core.Utilities;
using BinanceTR.Models.Common;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Concrete
{
    public class BinanceTrCommonApi : IBinanceTrCommonApi
    {
        private const string _prefix = "/open/v1/common";

        public async Task<IDataResult<long>> TestConnectivityAsync(CancellationToken ct = default)
        {
            try
            {
                var result = await RequestHelper.SendRequestWithoutAuth($"{_prefix}/time", null, true, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                var model = JsonSerializer.Deserialize<TimeModel>(data.result);
                return new SuccessDataResult<long>(model.Timestamp, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<long>(ex.Message);
            }
        }

        public async Task<IDataResult<List<SymbolDataList>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            try
            {
                var result = await RequestHelper.SendRequestWithoutAuth($"{_prefix}/symbols", null, true, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
                var model = JsonSerializer.Deserialize<SymbolModel>(data.result);
                return new SuccessDataResult<List<SymbolDataList>>(model.SymbolData.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<SymbolDataList>>(ex.Message);
            }
        }
    }
}
