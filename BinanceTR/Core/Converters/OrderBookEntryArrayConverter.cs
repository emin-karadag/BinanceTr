using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using static BinanceTR.Models.Public.OrderBook;

namespace BinanceTR.Core.Converters;

public class OrderBookEntryArrayConverter : JsonConverter<OrderBookEntry>
{
    public override OrderBookEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("OrderBookEntry verisi array formatında olmalıdır.");
        }

        var entry = new OrderBookEntry();

        // İlk eleman - Price
        reader.Read();
        entry.Price = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        // İkinci eleman - Quantity
        reader.Read();
        entry.Quantity = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        // Array'in sonunu oku
        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("OrderBookEntry array formatı beklenen uzunlukta değil.");
        }

        return entry;
    }

    public override void Write(Utf8JsonWriter writer, OrderBookEntry value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteStringValue(value.Price.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteStringValue(value.Quantity.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteEndArray();
    }
}
