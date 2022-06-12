using BinanceTR.Models.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class OrderStatusConvertor : JsonConverter<OrderStatusEnum>
    {
        public override OrderStatusEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetInt32();
            return value switch
            {
                0 => OrderStatusEnum.NEW,
                1 => OrderStatusEnum.PARTIALLY_FILLED,
                2 => OrderStatusEnum.FILLED,
                3 => OrderStatusEnum.CANCELED,
                4 => OrderStatusEnum.PENDING_CANCEL,
                5 => OrderStatusEnum.REJECTED,
                6 => OrderStatusEnum.EXPIRED,
                _ => OrderStatusEnum.SYSTEM_PROCESSING
            };
        }

        public override void Write(Utf8JsonWriter writer, OrderStatusEnum value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to {nameof(OrderStatusEnum)}");
        }
    }
}
