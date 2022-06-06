using BinanceTR.Models.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class OrderSideConvertor : JsonConverter<OrderSideEnum>
    {
        public override OrderSideEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetInt32() == 0 ? OrderSideEnum.BUY : OrderSideEnum.SELL;
        }

        public override void Write(Utf8JsonWriter writer, OrderSideEnum value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to OrderSideEnum");
        }
    }
}
