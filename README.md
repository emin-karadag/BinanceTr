![BinanceTr](https://github.com/emin-karadag/BinanceTr/blob/main/BinanceTR/BinanceTR-Logo.png?raw=true)

## BinanceTR C# API
BinanceTR borsasında, alım-satım yapmak veya piyasa verilerini çekmek için geliştirilen kullanımı kolay ve pratik bir .NET 7 - C# kütüphanesidir.


Bu kütüphane sadece [**Binance TR**](https://www.trbinance.com/) borsasını destekler.
Binance TR'nin herkese açık [API dokümanı](https://www.trbinance.com/apidocs/) referans alınarak C# programlama dili ile Binance TR için özel uygulama geliştirmek isteyenler için geliştirilmiştir.

### Lisans: 
    MIT License

## Özellikleri
- NuGet aracılığıyla yükleyebilme. ([BinanceTR](https://www.nuget.org/packages/BinanceTR/1.2.7.1))
- .NET 7 desteği. (Linux/MacOS uyumluluğu)
- RestAPI, [Binance TR resmi dokümanının](https://www.trbinance.com/apidocs/) büyük çoğunluğunu destekler.
	- Aktif olarak yeni özellikler eklenmeye devam edilecek.
- Genel ve özel API uç noktaları.
	- Özel API uç noktaları için Api Key ve Secret Key gerekmektedir.
- RestAPI, birden fazla kullanıcıyı destekler.  Her bir kullanıcı için API bilgilerini parametre olarak gönderebilirsiniz.
- Hataların daha kolay çözülebilmesi için Binance TR sunucularının geriye döndürdüğü **hata kodları** ve **hata mesajları** kullanılır.

## Başlangıç
Özel API uç noktalarını kullanabilmek için Binance TR üzerinden hesap oluşturmanız gerekmektedir. Eğer hesabınız yok ise [buraya tıklayarak](https://www.trbinance.com/account/signup?ref=W919G4P8) referansım üzerinden kaydolabilir, böylece alım-satım işlemlerinde **%10 komisyon indirimi** kazanabilirsiniz.
> Halka açık piyasa verilerine erişmek için Binance TR hesabı gerekli değildir!

## Kurulum
Bu kütüphane NuGet'te mevcuttur, indirmek için çekinmeyin. ([https://www.nuget.org/packages/BinanceTR/1.2.7.1](https://www.nuget.org/packages/BinanceTR/1.2.7.1 "https://www.nuget.org/packages/BinanceTR/1.2.7.1"))

**NuGet PM**
```
Install-Package BinanceTR -Version 1.2.7.1
```

**dotnet cli**
```
dotnet add package BinanceTR --version 1.2.7.1
```
## Yol Haritası
Önümüzdeki süreçte `BinanceTR` kütüphanesine yeni özelliklerin eklenmesi ve genişletilmesi için çalışmalar yapılacaktır. Aşağıdaki tabloda üzerinde çalıştığımız yeni özellikleri görebilirsiniz.

| Özellik                 |    Durum     |  
|------------------------|:--------------:|
| OCO (Order-Cancel-Order) Desteği            |      ✔         |
| Hesap Ticaret Listesi (Account trade list)    |                |
| Para Çekme Talebi (Withdraw)                    |                |
| Para Çekme Geçmişi (Withdraw History)    |                |
| Para Yatırma Geçmişi (Deposit History)      |                |
| Para Yatırma Adresi (Deposit Address)       |                | |

## Örnek Kullanım (Halka Açık İşlemler)

**Bağımlılık Enjeksiyonu (Dependency Injection):**
```csharp
using BinanceTR.Business.Abstract;
using BinanceTR.Business.Concrete;

services.AddSingleton<IBinanceTrService, BinanceTrManager>();
```

**Constructor'da tanımalama:**
```csharp
using BinanceTR.Business.Abstract;

 private readonly IBinanceTrService _binanceTrService;
 public Test(IBinanceTrService binanceTrService)
 {
 	_binanceTrService = binanceTrService;
 }
```

------------


**Sunucuyu test edin:**
```csharp
var testResult = await _binanceTrService.Common.TestConnectivityAsync(stoppingToken).ConfigureAwait(false);;
if (!testResult.Success)
{
	Console.WriteLine(testResult.Message);
}
```

------------


**Borsa tarafından listelenen tüm sembolleri alın:**
```csharp
var symbolResult = await _binanceTrService.Common.GetSymbolsAsync(stoppingToken).ConfigureAwait(false);
var symbolInfos = symbolResult.Data;
```


> Yukarıdaki örnekler RestAPI'nin halka açık fonksiyonlarının kullanımına örnek olarak verilmiştir. Daha fazlası için kütüphaneyi indirip kullanabilirsiniz.

## Örnek Kullanım (Özel İşlemler)

**Bağımlılık Enjeksiyonu (Dependency Injection):**
```csharp
using BinanceTR.Business.Abstract;
using BinanceTR.Business.Concrete;

services.AddSingleton<IBinanceTrService, BinanceTrManager>();
```

**Constructor'da tanımalama:**
```csharp
using BinanceTR.Business.Abstract;

 private readonly IBinanceTrService _binanceTrService;
 public Test(IBinanceTrService binanceTrService)
 {
 	_binanceTrService = binanceTrService;
 }
```

**Options tanımlama:**
```csharp
var options = new BinanceTrOptions
{
	ApiKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxx",
	ApiSecret = "xxxxxxxxxxxxxxxxxxxxxxxx"
};
```

------------


**1. Limit tipinde yeni bir sipariş gönderin:**
Limit fiyatından yeni bir alış siparişi göndermek için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.PostNewLimitOrderAsync(options, "BTC_TRY", OrderSideEnum.BUY, 0.002M, 475000, stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```

------------


Limit fiyatından yeni bir satış siparişi göndermek için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.PostNewLimitOrderAsync(options, "BTC_TRY", OrderSideEnum.SELL, 0.002M, 500000, stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```

------------


**2. Market tipinde yeni bir sipariş gönderin:**
Market fiyatından yeni bir alış siparişi göndermek için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.PostBuyMarketOrderAsync(options, "BTC_TRY", 12, stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```

------------

Market fiyatından yeni bir satış siparişi göndermek için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.PostSellMarketOrderAsync(options, "BTC_TRY", 12, stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```


**3. Zarar - Durdur (Stop) siparişleri gönderin:**
Zarar - Durdur (Stop) siparişi göndermek için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.PostStopLimitOrderAsync(options, "BTC_TRY", OrderSideEnum.SELL, 0.000015M, 150000, 150000, stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```

**4. Siparişlerinizi iptal edin:**
Açmış olduğunuz siparişlerinizi iptal etmek için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.CancelOrderByIdAsync(options, 123456, stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```

**5. Sipariş detayını görüntüleyin:**
Açmış olduğunuz siparişe ait detay bilgiyi almak için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.GetOrderByIdAsync(options, 123456, stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```

**6. Tüm siparişlerinizi görüntüleyin:**
Bir sembole ait tüm siparişlerinize ait detay bilgiyi almak için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.GetAllOrdersAsync(options, "BTC_TRY", ct:stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```

**7. Tüm açık siparişlerinizi görüntüleyin:**
Bir sembole ait tüm açık siparişlerinize ait detay bilgiyi almak için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.GetAllOpenOrdersAsync(options, "BTC_TRY", ct:stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```


**8. Açık siparişlerinizi görüntüleyin:**
Bir sembole ait AL (BUY) tipindeki tüm açık siparişlerinize ait detay bilgiyi almak için aşağıdaki örneği kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.GetAllOpenBuyOrdersAsync(options, "BTC_TRY", ct:stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```


**9. Emir Emiri Bozar (Order Cancel Order - OCO) tipinde sipariş gönderin:**
Bir sembole ait aynı anda hem zarar durdur hem de hedef fiyat göndermek için OCO emir tipini kullanabilirsiniz.
```csharp
var orderResult = await _binanceTrService.Order.PostOcoOrderAsync(options, "BTC_TRY", OrderSideEnum.SELL, 0.000015M, 466000, 433000, 492000, stoppingToken).ConfigureAwait(false);
if (orderResult.Success)
{
	// ....
}
```


> Yukarıdaki örnekler RestAPI'nin özel fonksiyonlarının kullanımına örnek olarak verilmiştir. Özel fonksiyonları kullanabilmek için Binance TR üzerinden Api Key ve Secret Key oluşturmanız gerekmektedir. Daha fazlası için kütüphaneyi indirip kullanabilirsiniz.

------------

## Bağış Yap
Kütüphaneyi kullanıp beğendiyseniz destek olmak amaçlı bağışta bulunabilirsiniz. Aşağıda Bitcoin ve Ethereum için cüzdan adreslerim yer almaktadır.

<img src="https://cdn.worldvectorlogo.com/logos/tether-1.svg" width="24px"> **Tether (USDT) - TRC20:** `TC3ruh9qWbwAnCHGEkschnmcYUNxGumHJS`

<img src="https://cdn.worldvectorlogo.com/logos/bitcoin.svg" width="24px"> **Bitcoin (BTC) - ERC20:** `0x4a656a72fada0ccdef737ad8cc2e39686af5efbe`

<img src="https://cdn.worldvectorlogo.com/logos/ethereum-1.svg" width="18px"> **Ethereum - ETH:** `0x4a656a72fada0ccdef737ad8cc2e39686af5efbe`
