using BinanceTR.Models.Public;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceTR.Core.Converters;

public class KlineArrayConverter : JsonConverter<Kline>
{
    public override Kline Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Kline verisi array formatında olmalıdır.");
        }

        var kline = new Kline();

        // Array'deki elemanları sırasıyla oku
        reader.Read(); // İlk eleman - OpenTime
        kline.OpenTime = reader.GetInt64();

        reader.Read(); // İkinci eleman - Open
        kline.Open = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        reader.Read(); // Üçüncü eleman - High
        kline.High = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        reader.Read(); // Dördüncü eleman - Low
        kline.Low = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        reader.Read(); // Beşinci eleman - Close
        kline.Close = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        reader.Read(); // Altıncı eleman - Volume
        kline.Volume = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        reader.Read(); // Yedinci eleman - CloseTime
        kline.CloseTime = reader.GetInt64();

        reader.Read(); // Sekizinci eleman - QuoteAssetVolume
        kline.QuoteAssetVolume = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        reader.Read(); // Dokuzuncu eleman - TradeCount
        kline.TradeCount = reader.GetInt32();

        reader.Read(); // Onuncu eleman - TakerBuyBaseAssetVolume
        kline.TakerBuyBaseAssetVolume = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        reader.Read(); // On birinci eleman - TakerBuyQuoteAssetVolume
        kline.TakerBuyQuoteAssetVolume = decimal.Parse(reader.GetString() ?? "0", CultureInfo.InvariantCulture);

        // Son eleman (ignore) - sadece okuyup geç
        reader.Read();
        reader.GetString(); // Ignore value

        reader.Read(); // Array'in sonunu oku
        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("Kline array formatı beklenen uzunlukta değil.");
        }

        return kline;
    }

    public override void Write(Utf8JsonWriter writer, Kline value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.OpenTime);
        writer.WriteStringValue(value.Open.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteStringValue(value.High.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteStringValue(value.Low.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteStringValue(value.Close.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteStringValue(value.Volume.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteNumberValue(value.CloseTime);
        writer.WriteStringValue(value.QuoteAssetVolume.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteNumberValue(value.TradeCount);
        writer.WriteStringValue(value.TakerBuyBaseAssetVolume.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteStringValue(value.TakerBuyQuoteAssetVolume.ToString("F8", CultureInfo.InvariantCulture));
        writer.WriteStringValue("0");
        writer.WriteEndArray();
    }
}
