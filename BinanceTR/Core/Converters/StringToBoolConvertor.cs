using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class StringToBoolConvertor : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetInt32() == 1;
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to bool");
        }
    }
}
