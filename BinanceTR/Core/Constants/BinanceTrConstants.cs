namespace BinanceTR.Core.Constants;

public static class BinanceTRConstants
{
    public const string BaseUrl = "https://www.binance.tr";
    public const string ApiBaseUrl = "https://api.binance.me";

    public const int DefaultOrderBookLimit = 100;
    public const int DefaultTradeLimit = 500;
    public const int MaxOrderBookLimit = 5000;

    public static class PublicEndpoints
    {
        public const string ServerTime = "/open/v1/common/time";
        public const string Symbols = "/open/v1/common/symbols";
        public const string OrderBook = "/api/v3/depth";
        public const string RecentTrades = "/api/v3/trades";
        public const string AggregateTrades = "/api/v3/aggTrades";
        public const string Klines = "/api/v1/klines";
    }

    public static class PrivateEndpoints
    {
        public const string AccountInfo = "/open/v1/account/spot";
        public const string AccountAsset = "/open/v1/account/spot/asset";
        public const string Orders = "/open/v1/orders";
        public const string OrderDetail = "/open/v1/orders/detail";
        public const string CancelOrder = "/open/v1/orders/cancel";
        public const string OcoOrder = "/open/v1/orders/oco";
        public const string Deposits = "/open/v1/deposits";
        public const string DepositAddress = "/open/v1/deposits/address";
    }
}
