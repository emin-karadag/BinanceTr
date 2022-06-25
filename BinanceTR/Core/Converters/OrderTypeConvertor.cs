using BinanceTR.Models.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class OrderTypeConvertor : JsonConverter<OrderTypeEnum>
    {
        public override OrderTypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetInt32();
            return value switch
            {
                1 => OrderTypeEnum.LIMIT,
                2 => OrderTypeEnum.MARKET,
                3 => OrderTypeEnum.STOP_LOSS,
                4 => OrderTypeEnum.STOP_LOSS_LIMIT,
                5 => OrderTypeEnum.TAKE_PROFIT,
                6 => OrderTypeEnum.TAKE_PROFIT_LIMIT,
                _ => OrderTypeEnum.LIMIT_MAKER
            };
        }

        public override void Write(Utf8JsonWriter writer, OrderTypeEnum value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to {nameof(OrderTypeEnum)}");
        }
    }
}
