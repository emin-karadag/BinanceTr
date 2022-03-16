using BinanceTR.Core.Results.Abstract;
using BinanceTR.Models.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Abstract
{
    public interface IBinanceTrCommonApi
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
    }
}
