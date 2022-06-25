using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class LongToDateTimeConvertor : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                var value = reader.GetInt64();
                return DateTimeOffset.FromUnixTimeMilliseconds(value).DateTime;
            }
            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to datetime");
        }
    }
}
