using BinanceTR.Business.Abstract;

namespace BinanceTR.Business.Concrete
{
    public class BinanceTrManager : IBinanceTrService
    {
        public IBinanceTrCommonApi Common { get; }
        public IBinanceTrMarketApi Market { get; }
        public IBinanceTrAccountApi Account { get; }
        public IBinanceTrOrderApi Order { get; }

        public BinanceTrManager()
        {
            Common = new BinanceTrCommonApi();
            Market = new BinanceTrMarketApi();
            Account = new BinanceTrAccountApi();
            Order = new BinanceTrOrderApi();
        }
    }
}
