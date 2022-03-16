using BinanceTR.Core.Results.Abstract;
using BinanceTR.Models;
using BinanceTR.Models.Account;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Abstract
{
    public interface IBinanceTrAccountApi
    {
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
    }
}
