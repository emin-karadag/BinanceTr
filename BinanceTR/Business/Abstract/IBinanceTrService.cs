namespace BinanceTR.Business.Abstract
{
    public interface IBinanceTrService
    {
        public IBinanceTrCommonApi Common { get; }
        public IBinanceTrMarketApi Market { get; }
        public IBinanceTrAccountApi Account { get; }
        public IBinanceTrOrderApi Order { get; }
    }
}
