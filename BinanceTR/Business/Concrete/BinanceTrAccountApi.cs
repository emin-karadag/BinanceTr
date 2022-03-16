using BinanceTR.Business.Abstract;
using BinanceTR.Core.Results.Abstract;
using BinanceTR.Core.Results.Concrete;
using BinanceTR.Core.Utilities;
using BinanceTR.Models;
using BinanceTR.Models.Account;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Concrete
{
    public class BinanceTrAccountApi : IBinanceTrAccountApi
    {
        private const string _prefix = "/open/v1/account";

        public async Task<IDataResult<List<AccountAsset>>> GetAccountInformationAsync(BinanceTrOptions options, CancellationToken ct = default)
        {
            try
            {
                var result = await RequestHelper.SendRequestAsync(HttpMethod.Get, $"{_prefix}/spot", options, ct: ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
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

        public async Task<IDataResult<AssetInformationData>> GetAssetIformationAsync(BinanceTrOptions options, string assetName, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "asset", assetName }
                };

                var result = await RequestHelper.SendRequestAsync(HttpMethod.Get, $"{_prefix}/spot/asset", options, parameters, ct).ConfigureAwait(false);
                var data = RequestHelper.CheckResult(result);
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
    }
}
