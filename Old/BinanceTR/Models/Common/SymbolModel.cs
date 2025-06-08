using BinanceTR.Core.Converters;
using BinanceTR.Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinanceTR.Models.Common
{
    public class SymbolModel : IBinanceTrModel
    {
        [JsonPropertyName("code")]
        [JsonConverter(typeof(StringToIntConvertor))]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public SymbolData SymbolData { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class SymbolData
    {
        [JsonPropertyName("list")]
        public List<SymbolDataList> List { get; set; }
    }

    public class SymbolDataList
    {
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringToIntConvertor))]
        public int Type { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; }

        [JsonPropertyName("basePrecision")]
        [JsonConverter(typeof(StringToIntConvertor))]
        public int BasePrecision { get; set; }

        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; }

        [JsonPropertyName("quotePrecision")]
        [JsonConverter(typeof(StringToIntConvertor))]
        public int QuotePrecision { get; set; }

        [JsonPropertyName("filters")]
        public List<Filter> Filters { get; set; }

        [JsonPropertyName("orderTypes")]
        public List<string> OrderTypes { get; set; }

        [JsonPropertyName("icebergEnable")]
        [JsonConverter(typeof(StringToBoolConvertor))]
        public bool IcebergEnable { get; set; }

        [JsonPropertyName("ocoEnable")]
        [JsonConverter(typeof(StringToBoolConvertor))]
        public bool OcoEnable { get; set; }

        [JsonPropertyName("spotTradingEnable")]
        [JsonConverter(typeof(StringToBoolConvertor))]
        public bool SpotTradingEnable { get; set; }

        [JsonPropertyName("marginTradingEnable")]
        [JsonConverter(typeof(StringToBoolConvertor))]
        public bool MarginTradingEnable { get; set; }

        [JsonPropertyName("permissions")]
        public List<string> Permissions { get; set; }
    }

    public class Filter
    {
        [JsonPropertyName("filterType")]
        public string FilterType { get; set; }

        [JsonPropertyName("minPrice")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal MinPrice { get; set; }

        [JsonPropertyName("maxPrice")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal MaxPrice { get; set; }

        [JsonPropertyName("tickSize")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal TickSize { get; set; }

        [JsonPropertyName("applyToMarket")]
        public bool ApplyToMarket { get; set; }

        [JsonPropertyName("multiplierUp")]
        public decimal? MultiplierUp { get; set; }

        [JsonPropertyName("multiplierDown")]
        public decimal? MultiplierDown { get; set; }

        [JsonPropertyName("avgPriceMins")]
        [JsonConverter(typeof(StringToIntConvertor))]
        public int AvgPriceMins { get; set; }

        [JsonPropertyName("minQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal MinQty { get; set; }

        [JsonPropertyName("maxQty")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal MaxQty { get; set; }

        [JsonPropertyName("stepSize")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal StepSize { get; set; }

        [JsonPropertyName("minNotional")]
        [JsonConverter(typeof(StringToDecimalConvertor))]
        public decimal MinNotional { get; set; }

        [JsonPropertyName("limit")]
        [JsonConverter(typeof(StringToIntConvertor))]
        public int Limit { get; set; }

        [JsonPropertyName("maxNumAlgoOrders")]
        [JsonConverter(typeof(StringToIntConvertor))]
        public int MaxNumAlgoOrders { get; set; }
    }
}
