using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class StringToDecimalConvertor : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                if (str != "null")
                    return Convert.ToDecimal(str, new CultureInfo("en-US"));
                else
                    return 0;
            }
            return 0;
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to decimal");
        }
    }
}
