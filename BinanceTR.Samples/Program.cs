using BinanceTR;
using BinanceTR.Enums;

Console.WriteLine("🚀 BinanceTR Kütüphane Örnekleri");
Console.WriteLine("=================================\n");

try
{
    // API bilgileri (gerçek kullanımda environment variables kullanın)
    var apiKey = "969A84CBE7443EdaD6e490DDCf648968B27IwtlBAMBJ7YPvXCZnsKrxtPDDFw9s";
    var secretKey = "2C3Bb08f240F8DdbC7E48f6e040412a7PJX5Ttxy9XfONEo5y0XO46kVwLbgneHR";



    using var client = new BinanceTrClient(apiKey, secretKey);

    var depositAddress = await client.Private.GetDepositAddressAsync("AVAX", "AVAXC");
    if (depositAddress?.Data != null)
    {
        var address = depositAddress.Data;

        Console.WriteLine($"Asset: {address.Asset}");
        Console.WriteLine($"Network: {address.NetworkDisplayName}");
        Console.WriteLine($"Address: {address.DisplayAddress}");
        Console.WriteLine($"Min Deposit: {address.FormattedMinimumDeposit}");
        Console.WriteLine($"Has Address Tag: {address.HasAddressTag}");
    }







    Console.WriteLine("📊 PUBLIC ENDPOINTS (API Key gerekmez)");
    Console.WriteLine("======================================\n");

    using var publicClient = new BinanceTrClient();

    // 1. Sunucu Zamanı Örneği
    Console.WriteLine("🕐 Sunucu Zamanı:");
    var serverTime = await publicClient.Public.GetServerTimeAsync();
    Console.WriteLine($"✅ Sunucu zamanı: {serverTime?.DateTimeUTC}");
    Console.WriteLine();

    // 2. BTC_TRY Emir Defteri Örneği
    Console.WriteLine("📖 BTC_TRY Emir Defteri:");
    var orderBook = await publicClient.Public.GetOrderBookAsync("BTC_TRY", limit: 5);
    if (orderBook?.Data != null)
    {
        var book = orderBook.Data;
        Console.WriteLine($"✅ En iyi alış: {book.BestBid:N2} ₺");
        Console.WriteLine($"   En iyi satış: {book.BestAsk:N2} ₺");
        Console.WriteLine($"   Spread: {book.Spread:N2} ₺ (%{book.SpreadPercentage:F2})");
    }
    Console.WriteLine();

    Console.WriteLine("🔐 PRIVATE ENDPOINTS (API Key gerekir)");
    Console.WriteLine("=====================================\n");

    using var privateClient = new BinanceTrClient(apiKey, secretKey);

    // 3. Hesap Bilgileri Örneği
    Console.WriteLine("👤 Hesap Bilgileri:");
    try
    {
        var accountInfo = await privateClient.Private.GetAccountInformationAsync();
        if (accountInfo?.Data != null)
        {
            var account = accountInfo.Data;
            Console.WriteLine($"✅ İşlem aktif: {account.IsTradingEnabled}");
            Console.WriteLine($"   Bakiyesi olan varlık sayısı: {account.NonZeroAssets.Count()}");

            // İlk 3 varlığı göster
            foreach (var asset in account.NonZeroAssets.Take(3))
            {
                Console.WriteLine($"   {asset.Asset}: {asset.TotalBalance:N8}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️  API Anahtarı geçersiz: {ex.Message.Split('.')[0]}");
    }
    Console.WriteLine();

    // 4. BTC_TRY Emir Geçmişi Örneği
    Console.WriteLine("📋 BTC_TRY Emir Geçmişi:");
    try
    {
        var orders = await privateClient.Private.GetAllOrdersAsync("BTC_TRY", limit: 3);
        if (orders?.Data?.List != null)
        {
            Console.WriteLine($"✅ {orders.Data.List.Length} emir bulundu");
            Console.WriteLine($"   Başarı oranı: %{orders.Data.SuccessRate:F1}");

            foreach (var order in orders.Data.List.Take(2))
            {
                Console.WriteLine($"   Emir #{order.OrderId}: {order.Side} {order.OrigQty:N4} BTC @ {order.Price:N2} ₺");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️  Emir geçmişi alınamadı: {ex.Message.Split('.')[0]}");
    }
    Console.WriteLine();

    // 5. Limit Emir Verme Örneği
    Console.WriteLine("📈 Limit Emir Verme:");
    try
    {
        var newOrder = await privateClient.Private.NewOrderAsync(
            symbol: "BTC_TRY",
            side: OrderSideEnum.BUY,
            type: OrderTypesEnum.LIMIT,
            quantity: 0.0001m,
            price: 100000.00m
        );

        if (newOrder?.Data != null)
        {
            var order = newOrder.Data;
            Console.WriteLine($"✅ Limit emir oluşturuldu!");
            Console.WriteLine($"   Emir ID: {order.OrderId}");
            Console.WriteLine($"   {order.Side} {order.OrigQty:N4} BTC @ {order.Price:N2} ₺");
            Console.WriteLine($"   Durum: {order.Status}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️  Emir verilemedi: {ex.Message.Split('.')[0]}");
        Console.WriteLine("   (Normal - test için çok düşük fiyat kullanıldı)");
    }

    Console.WriteLine("\n✅ Örnekler tamamlandı!");
    Console.WriteLine("\n💡 Daha fazla örnek için README.md dosyasına bakın.");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Hata: {ex.Message}");
}

Console.WriteLine("\nÇıkmak için bir tuşa basın...");
Console.ReadKey();