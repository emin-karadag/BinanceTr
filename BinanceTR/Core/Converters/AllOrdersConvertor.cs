using BinanceTR.Models.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters
{
    public class AllOrdersConvertor : JsonConverter<AllOrdersEnum>
    {
        public override AllOrdersEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetInt32();
            return value switch
            {
                1 => AllOrdersEnum.Open,
                2 => AllOrdersEnum.History,
                _ => AllOrdersEnum.All
            };
        }

        public override void Write(Utf8JsonWriter writer, AllOrdersEnum value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException($"Unable to parse {value} to {nameof(AllOrdersEnum)}");
        }
    }
}
