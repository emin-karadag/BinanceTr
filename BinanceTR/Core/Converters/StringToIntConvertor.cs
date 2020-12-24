using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class StringToIntConvertor : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                int.TryParse(reader.GetString(), out int result);
                return result;
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32();
            }
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to int");
        }
    }
}
