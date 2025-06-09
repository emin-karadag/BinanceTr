# 🚀 BinanceTR - Binance TR API Kütüphanesi

**BinanceTR**, [Binance TR](https://www.binance.tr) kripto para borsası için geliştirilmiş modern, yüksek performanslı ve developer-friendly .NET kütüphanesidir.

[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download)
[![C#](https://img.shields.io/badge/C%23-12.0-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/license-MIT-orange.svg)](LICENSE)
[![NuGet](https://img.shields.io/badge/NuGet-Ready-brightgreen.svg)](https://www.nuget.org/packages/BinanceTR)

## ✨ Özellikler

- 🔥 **.NET 9** ile modern C# 12 desteği
- ⚡ **Zero External Dependencies** - Sadece .NET built-in kütüphaneler
- 🏗️ **Fluent API** ile kolay kullanım
- 🔐 **Type-Safe** operasyonlar
- 🔄 **Async/Await** pattern ile asenkron operasyonlar
- 🏪 **Builder Pattern** ile temiz kod organizasyonu
- 🛡️ **Enterprise-Grade** kod kalitesi
- 📈 **Performance Optimized** - Lazy loading, StringBuilder kullanımı

## 📦 Kurulum

### NuGet Package Manager ile:
```bash
Install-Package BinanceTr
```

### .NET CLI ile:
```bash
dotnet add package BinanceTr
```

### PackageReference ile:
```xml
<PackageReference Include="BinanceTr" Version="1.3.0" />
```

## 🚀 Hızlı Başlangıç

### Temel Kullanım

```csharp
using BinanceTr;

// Public endpoints için (API Key gerekmez)
var client = new BinanceTrClient();

// Private endpoints için (API Key ve Secret Key gereklidir)
var client = new BinanceTrClient("YOUR_API_KEY", "YOUR_SECRET_KEY");
```

### IDisposable Pattern
```csharp
using var client = new BinanceTrClient("API_KEY", "SECRET_KEY");
// Client otomatik olarak dispose edilir
```

## 📊 Public Endpoints (Genel Erişim)

Public endpoint'ler API Key gerektirmez ve genel piyasa verilerine erişim sağlar.

### 🕐 Server Zamanı
```csharp
using var client = new BinanceTrClient();

var serverTime = await client.Public.GetServerTimeAsync();
Console.WriteLine($"Server Time: {serverTime?.DateTimeUTC}");
```

### 📈 Sembol Bilgileri
```csharp
var symbols = await client.Public.GetSymbolsAsync();
if (symbols?.Data?.List != null)
{
    // Spot trading aktif semboller
    var activeSymbols = symbols.Data.GetSpotTradingEnabledSymbols();
    
    foreach (var symbol in activeSymbols.Take(5))
    {
        Console.WriteLine($"Symbol: {symbol.Symbol}");
        Console.WriteLine($"Base Asset: {symbol.BaseAsset}");
        Console.WriteLine($"Quote Asset: {symbol.QuoteAsset}");
        Console.WriteLine($"Spot Trading: {symbol.IsSpotTradingEnabled}");
        Console.WriteLine("---");
    }
}
```

### 📖 Order Book (Emir Defteri)
```csharp
var orderBook = await client.Public.GetOrderBookAsync("BTC_TRY", limit: 10);
if (orderBook?.Data != null)
{
    var book = orderBook.Data;
    
    Console.WriteLine($"Best Bid: {book.BestBid}");
    Console.WriteLine($"Best Ask: {book.BestAsk}");
    Console.WriteLine($"Spread: {book.Spread}");
    Console.WriteLine($"Spread %: {book.SpreadPercentage:F2}%");
    
    // İlk 5 bid/ask
    Console.WriteLine("\nTop 5 Bids:");
    foreach (var bid in book.Bids.Take(5))
    {
        Console.WriteLine($"Price: {bid.Price}, Quantity: {bid.Quantity}");
    }
}
```

### 📈 Son İşlemler
```csharp
var recentTrades = await client.Public.GetRecentTradesAsync("BTC_TRY", limit: 10);
if (recentTrades?.Data != null)
{
    foreach (var trade in recentTrades.Data.Take(5))
    {
        Console.WriteLine($"Price: {trade.Price}, Quantity: {trade.Qty}");
        Console.WriteLine($"Time: {trade.DateTimeUTC}");
        Console.WriteLine($"Buyer Maker: {trade.IsBuyerMaker}");
        Console.WriteLine("---");
    }
}
```

### 📊 Kline/Candlestick Verileri
```csharp
var klines = await client.Public.GetKlinesAsync(
    symbol: "BTC_TRY",
    interval: "1h",
    limit: 24 // Son 24 saat
);

if (klines?.Data != null)
{
    foreach (var kline in klines.Data.Take(5))
    {
        Console.WriteLine($"Open: {kline.Open}");
        Console.WriteLine($"High: {kline.High}");
        Console.WriteLine($"Low: {kline.Low}");
        Console.WriteLine($"Close: {kline.Close}");
        Console.WriteLine($"Volume: {kline.Volume}");
        Console.WriteLine($"Time: {kline.OpenTimeUtc}");
        Console.WriteLine("---");
    }
}
```

## 🔐 Private Endpoints (Özel Erişim)

Private endpoint'ler API Key ve Secret Key gerektirir. Hesap bilgileri, işlem geçmişi ve emir operasyonları için kullanılır.

### 👤 Hesap Bilgileri
```csharp
using var client = new BinanceTrClient("YOUR_API_KEY", "YOUR_SECRET_KEY");

var accountInfo = await client.Private.GetAccountInformationAsync();
if (accountInfo?.Data != null)
{
    var account = accountInfo.Data;
    
    Console.WriteLine($"Trading Enabled: {account.IsTradingEnabled}");
    Console.WriteLine($"Withdraw Enabled: {account.IsWithdrawEnabled}");
    Console.WriteLine($"Deposit Enabled: {account.IsDepositEnabled}");
    
    // Bakiyesi olan varlıklar
    var nonZeroAssets = account.NonZeroAssets;
    Console.WriteLine($"\nAssets with Balance ({nonZeroAssets.Count()}):");
    
    foreach (var asset in nonZeroAssets)
    {
        Console.WriteLine($"{asset.Asset}: {asset.TotalBalance} (Free: {asset.Free}, Locked: {asset.Locked})");
        Console.WriteLine($"Free %: {asset.FreePercentage:F2}%, Locked %: {asset.LockedPercentage:F2}%");
    }
    
    // Belirli bir varlığın bakiyesi
    var btcBalance = account.GetTotalBalance("TRY");
    Console.WriteLine($"\nTRY Balance: {btcBalance}");
}
```

### 💰 Varlık Detay Bilgisi
```csharp
var assetInfo = await client.Private.GetAccountAssetInformationAsync("BTC");
if (assetInfo?.Data != null)
{
    var asset = assetInfo.Data;
    Console.WriteLine($"Asset: {asset.Asset}");
    Console.WriteLine($"Total Balance: {asset.TotalBalance}");
    Console.WriteLine($"Available: {asset.Free}");
    Console.WriteLine($"In Orders: {asset.Locked}");
}
```

### 📋 Emir Geçmişi
```csharp
var tradeHistory = await client.Private.GetAccountTradeListAsync(
    symbol: "BTC_TRY",
    limit: 10
);

if (tradeHistory?.Data?.List != null)
{
    foreach (var trade in tradeHistory.Data.List.Take(5))
    {
        Console.WriteLine($"Order ID: {trade.OrderId}");
        Console.WriteLine($"Symbol: {trade.Symbol}");
        Console.WriteLine($"Price: {trade.Price}");
        Console.WriteLine($"Quantity: {trade.OrigQty}");
        Console.WriteLine($"Executed: {trade.ExecutedQty}");
        Console.WriteLine($"Fill %: {trade.FillPercentage:F2}%");
        Console.WriteLine($"Remaining: {trade.RemainingQty}");
        Console.WriteLine($"Time: {trade.CreateTimeUTC}");
        Console.WriteLine("---");
    }
}
```

### 📈 Emir Verme
```csharp
// Limit Emir
var newOrder = await client.Private.NewOrderAsync(
    symbol: "BTC_TRY",
    side: OrderSideEnum.BUY,
    type: OrderTypesEnum.LIMIT,
    quantity: 0.001m,
    price: 45000.00m
);

if (newOrder?.Data != null)
{
    var order = newOrder.Data;
    Console.WriteLine($"Order ID: {order.OrderId}");
    Console.WriteLine($"Status: {order.Status}");
    Console.WriteLine($"Symbol: {order.Symbol}");
    Console.WriteLine($"Side: {order.Side}");
    Console.WriteLine($"Type: {order.Type}");
    Console.WriteLine($"Price: {order.Price}");
    Console.WriteLine($"Quantity: {order.OrigQty}");
    Console.WriteLine($"Executed: {order.ExecutedQty}");
    Console.WriteLine($"Remaining: {order.RemainingQty}");
    Console.WriteLine($"Fill %: {order.FillPercentage:F2}%");
}
```

### 🔍 Emir Detayı
```csharp
var orderDetail = await client.Private.GetOrderDetailAsync(123456789);
if (orderDetail?.Data != null)
{
    var order = orderDetail.Data;
    Console.WriteLine($"Order Status: {order.Status}");
    Console.WriteLine($"Average Price: {order.AverageExecutionPrice}");
    Console.WriteLine($"Has Execution: {order.HasExecution}");
    Console.WriteLine($"Is Partially Executed: {order.IsPartiallyExecuted}");
    Console.WriteLine($"Execution Value: {order.ExecutionValue}");
}
```

### ❌ Emir İptali
```csharp
var cancelResult = await client.Private.CancelOrderAsync(123456789);
if (cancelResult?.Data != null)
{
    Console.WriteLine($"Order {cancelResult.Data.OrderId} cancelled successfully");
}
```

### 📊 Tüm Emirler
```csharp
var allOrders = await client.Private.GetAllOrdersAsync(
    symbol: "BTC_TRY",
    limit: 50
);

if (allOrders?.Data?.List != null)
{
    var orders = allOrders.Data;
    
    Console.WriteLine($"Total Orders: {orders.TotalOrdersCount}");
    Console.WriteLine($"Success Rate: {orders.SuccessRate:F2}%");
    Console.WriteLine($"Total Executed Value: {orders.TotalExecutedValue}");
    
    // Aktif emirler
    var activeOrders = orders.ActiveOrders;
    Console.WriteLine($"\nActive Orders: {activeOrders.Count()}");
    
    // Tamamlanan emirler
    var filledOrders = orders.FilledOrders;
    Console.WriteLine($"Filled Orders: {filledOrders.Count()}");
    
    // Son 7 günün emirleri
    var recentOrders = orders.GetRecentOrders(7);
    Console.WriteLine($"Last 7 days orders: {recentOrders.Count()}");
}
```

### 💰 Yatırma Geçmişi
```csharp
var depositHistory = await client.Private.GetDepositHistoryAsync(
    asset: "BTC",
    status: WalletStatusEnum.SUCCESS
);

if (depositHistory?.Data?.List != null)
{
    var deposits = depositHistory.Data;
    
    Console.WriteLine($"Latest Deposit: {deposits.LatestDeposit?.Amount} {deposits.LatestDeposit?.Asset}");
    Console.WriteLine($"Largest Deposit: {deposits.LargestDeposit?.Amount} {deposits.LargestDeposit?.Asset}");
    
    // BTC toplam yatırım miktarı
    var totalBtcDeposits = deposits.GetTotalAmountByAsset("BTC");
    Console.WriteLine($"Total BTC Deposits: {totalBtcDeposits}");
    
    foreach (var deposit in deposits.List.Take(5))
    {
        Console.WriteLine($"Asset: {deposit.Asset}");
        Console.WriteLine($"Amount: {deposit.FormattedAmount}");
        Console.WriteLine($"Network: {deposit.NetworkDisplayName}");
        Console.WriteLine($"Time: {deposit.InsertTimeUTC}");
        Console.WriteLine($"Status: {deposit.Status}");
        Console.WriteLine("---");
    }
}
```

### 🏦 Yatırma Adresi
```csharp
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
```

## 🔧 Gelişmiş Kullanım

### Hata Yönetimi
```csharp
try
{
    var client = new BinanceTrClient("API_KEY", "SECRET_KEY");
    var accountInfo = await client.Private.GetAccountInformationAsync();
    
    if (accountInfo?.Data != null)
    {
        // Başarılı işlem
        Console.WriteLine($"Account active: {accountInfo.Data.IsAccountActive}");
    }
    else
    {
        // API hatası
        Console.WriteLine($"Error: {accountInfo?.Message}");
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid API credentials: {ex.Message}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Network error: {ex.Message}");
}
```

### CancellationToken Kullanımı
```csharp
var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

try
{
    var symbols = await client.Public.GetSymbolsAsync(cts.Token);
    // İşlem 30 saniye içinde tamamlanmazsa iptal edilir
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation timed out");
}
```

### Raw Response Erişimi
```csharp
var response = await client.Public.GetSymbolsAsync();
if (response != null)
{
    // Ham JSON yanıtına erişim
    Console.WriteLine($"Raw JSON: {response.RawResponse}");
    
    // Response metadata
    Console.WriteLine($"Response Code: {response.Code}");
    Console.WriteLine($"Timestamp: {response.DateTimeUTC}");
    Console.WriteLine($"Message: {response.Message}");
}
```
## 🔒 Güvenlik

- API anahtarlarınızı asla kaynak kodunda saklamayın
- Production ortamında environment variable veya güvenli yapılandırma kullanın
- API anahtarlarınızı düzenli olarak yenileyin
- Gereksiz izinleri API anahtarınıza vermeyin

```csharp
// ✅ Güvenli kullanım
var apiKey = Environment.GetEnvironmentVariable("BINANCETR_API_KEY");
var secretKey = Environment.GetEnvironmentVariable("BINANCETR_SECRET_KEY");
var client = new BinanceTrClient(apiKey!, secretKey!);

// ❌ Güvenli olmayan kullanım
var client = new BinanceTrClient("hardcoded_api_key", "hardcoded_secret");
```

## 📞 İletişim

- **Geliştirici**: Mehmet Emin KARADAĞ
- **GitHub**: [https://github.com/emin-karadag](https://github.com/emin-karadag)
- **BtcTurk API Dokümantasyonu**: [https://www.binance.tr/apidocs](https://www.binance.tr/apidocs)

## 💰 Bağış

Bu projeyi beğendiyseniz ve geliştirilmesine katkıda bulunmak istiyorsanız, kripto para bağışı yapabilirsiniz:

**Tüm Ağlar İçin Cüzdan Adresi:**
```
0x21bc1e50042708a30275c151e43f7b1c1be99f2f
```

Desteklenen tokenlar: Tüm ERC-20/BEP-20 tokenları

---

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## ⚠️ Sorumluluk Reddi

Bu kütüphane topluluk tarafından geliştirilmiştir ve Binance TR'nin resmi kütüphanesi değildir. Kullanımından doğacak her türlü zarar ve sorumluluğun kullanıcıya ait olduğunu kabul etmiş sayılırsınız.


**⭐ Bu projeyi beğendiyseniz, lütfen yıldız vermeyi unutmayın!** 