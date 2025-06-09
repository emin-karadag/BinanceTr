using System.Text.Json.Serialization;

namespace BinanceTR.Models.Public
{
    public class SymbolInfo
    {
        public SymbolList[]? List { get; set; }

        public class SymbolList
        {
            public int Type { get; set; }
            public string Symbol { get; set; } = "";
            public string BaseAsset { get; set; } = "";
            public decimal BasePrecision { get; set; }
            public string QuoteAsset { get; set; } = "";
            public decimal QuotePrecision { get; set; }
            public Filter[]? Filters { get; set; }

            public string[]? OrderTypes { get; set; }
            public int IcebergEnable { get; set; }
            public int OcoEnable { get; set; }
            public int SpotTradingEnable { get; set; }
            public int MarginTradingEnable { get; set; }
            public string[]? Permissions { get; set; }


            // Kullanýþlý helper property'ler
            [JsonIgnore]
            public bool IsSpotTradingEnabled => SpotTradingEnable == 1;

            [JsonIgnore]
            public bool IsMarginTradingEnabled => MarginTradingEnable == 1;

            [JsonIgnore]
            public bool IsIcebergEnabled => IcebergEnable == 1;

            [JsonIgnore]
            public bool IsOcoEnabled => OcoEnable == 1;

            public class Filter
            {
                public string FilterType { get; set; } = "";
                public decimal? MinPrice { get; set; }
                public decimal? MaxPrice { get; set; }
                public decimal? TickSize { get; set; }
                public bool ApplyToMarket { get; set; }
                public decimal? MinQty { get; set; }
                public decimal? MaxQty { get; set; }
                public decimal? StepSize { get; set; }
                public int? Limit { get; set; }
                public double? BidMultiplierUp { get; set; }
                public double? BidMultiplierDown { get; set; }
                public double? AskMultiplierUp { get; set; }
                public double? AskMultiplierDown { get; set; }
                public string? AvgPriceMins { get; set; }
                public decimal? MinNotional { get; set; }
                public int? MaxNumAlgoOrders { get; set; }


                // Kullanýþlý helper method'lar
                [JsonIgnore]
                public bool IsPriceFilter => FilterType == "PRICE_FILTER";

                [JsonIgnore]
                public bool IsLotSizeFilter => FilterType == "LOT_SIZE";

                [JsonIgnore]
                public bool IsMarketLotSizeFilter => FilterType == "MARKET_LOT_SIZE";

                [JsonIgnore]
                public bool IsNotionalFilter => FilterType == "NOTIONAL";

                [JsonIgnore]
                public bool IsPercentPriceBySideFilter => FilterType == "PERCENT_PRICE_BY_SIDE";

                [JsonIgnore]
                public bool IsIcebergPartsFilter => FilterType == "ICEBERG_PARTS";

                [JsonIgnore]
                public bool IsMaxNumOrdersFilter => FilterType == "MAX_NUM_ORDERS";

                [JsonIgnore]
                public bool IsMaxNumAlgoOrdersFilter => FilterType == "MAX_NUM_ALGO_ORDERS";

                [JsonIgnore]
                public bool IsTrailingDeltaFilter => FilterType == "TRAILING_DELTA";
            }
        }

        // Extension method'lar için kullanýþlý helper'lar
        public SymbolList? GetSymbol(string symbol) => List?.FirstOrDefault(x => x.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<SymbolList> GetSymbolsByQuoteAsset(string quoteAsset) => List?.Where(x => x.QuoteAsset.Equals(quoteAsset, StringComparison.OrdinalIgnoreCase)) ?? [];

        public IEnumerable<SymbolList> GetSymbolsByBaseAsset(string baseAsset) => List?.Where(x => x.BaseAsset.Equals(baseAsset, StringComparison.OrdinalIgnoreCase)) ?? [];

        public IEnumerable<SymbolList> GetSpotTradingEnabledSymbols() => List?.Where(x => x.IsSpotTradingEnabled) ?? [];

        public IEnumerable<SymbolList> GetMarginTradingEnabledSymbols() => List?.Where(x => x.IsMarginTradingEnabled) ?? [];
    }
}
