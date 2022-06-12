using BinanceTR.Models.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class SymbolTypeConvertor : JsonConverter<SymbolTypeEnum>
    {
        public override SymbolTypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetInt32();
            return value switch
            {
                1 => SymbolTypeEnum.MAIN,
                _ => SymbolTypeEnum.NEXT
            };
        }

        public override void Write(Utf8JsonWriter writer, SymbolTypeEnum value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to {nameof(SymbolTypeEnum)}");
        }
    }
}
