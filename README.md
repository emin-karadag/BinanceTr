![BinanceTr](https://github.com/emin-karadag/BinanceTr/blob/main/BinanceTR/BinanceTR-Logo.png?raw=true)

## BinanceTR C# API
BinanceTR borsasında, çeşitli kripto para çiftlerinde alım-satım yapmak için geliştirilen kullanımı kolay ve pratik bir .NET 5 - C# kütüphanesidir.


Bu kütüphane sadece [**Binance TR**](https://www.trbinance.com/) borsasını destekler.
Binance TR'nin herkese açık [API dokümanı](https://www.trbinance.com/apidocs/) referans alınarak C# programlama dili ile Binance TR için özel uygulama geliştirmek isteyenler için geliştirilmiştir.

### Lisans: 
    MIT License

## Özellikleri
- NuGet aracılığıyla yükleyebilme. ([BinanceTR](https://www.nuget.org/packages/BinanceTR/1.0.0))
- .NET 5 desteği. (Linux/MacOS uyumluluğu)
- RestAPI, [Binance TR resmi dokümanının](https://www.trbinance.com/apidocs/) büyük çoğunluğunu destekler.
	- Aktif olarak yeni özellikler eklenmeye devam edilecek.
- Genel ve özel API uç noktaları.
	- Özel API uç noktaları için Api Key ve Secret Key gerekmektedir.
- RestAPI, birden fazla kullanıcıyı destekler.  Her bir kullanıcı için API bilgilerini parametre olarak gönderebilirsiniz.
- Hataların daha kolay çözülebilmesi için Binance TR sunucularının geriye döndürdüğü **hata kodları** ve **hata mesajları** kullanılır.

## Başlangıç
Özel API uç noktalarını kullanabilmek için Binance TR üzerinden hesap oluşturmanız gerekmektedir. Eğer hesabınız yok ise [Buraya tıklayarak](https://www.trbinance.com/account/signup?ref=W919G4P8) referansım üzerinden kaydolabilir, böylece alım-satım işlemlerinde **%10 komisyon indiriminden** faydalanabilirsiniz.
> Halka açık piyasa verilerine erişmek için Binance TR hesabı gerekli değildir!

## Kurulum
Bu kütüphane NuGet'te mevcuttur, indirmek için çekinmeyin. ([https://www.nuget.org/packages/BinanceTR/1.0.0](https://www.nuget.org/packages/BinanceTR/1.0.0 "https://www.nuget.org/packages/BinanceTR/1.0.0"))

**NuGet PM**
```
Install-Package BinanceTR -Version 1.0.0
```

**dotnet cli**
```
dotnet add package BinanceTR --version 1.0.0
```
## Yol Haritası
Önümüzdeki süreçte `BinanceTR` kütüphanesine yeni özelliklerin eklenmesi ve genişletilmesi için çalışmalar yapılacaktır. Aşağıdaki tabloda üzerinde çalıştığımız yeni özellikleri görebilirsiniz.

| Özellik                 |    Durum     |  
|------------------------|:--------------:|
| OCO (Order-Cancel-Order) Desteği            |                |
| Hesap Ticaret Listesi (Account trade list)    |                |
| Para Çekme Talebi (Withdraw)                    |                |
| Para Çekme Geçmişi (Withdraw History)    |                |
| Para Yatırma Geçmişi (Deposit History)      |                |
| Para Yatırma Adresi (Deposit Address)       |                | |

## Örnek Kullanım (Halka Açık İşlemler)
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
var testResult = await _binanceTrService.TestConnectivityAsync().ConfigureAwait(false);
if (!testResult.Success)
{
	Console.WriteLine(testResult.Message);
}
```

------------


**Borsa tarafından listelenen tüm sembolleri alın:**
```csharp
var symbolResult = await _binanceTrService.GetSymbolsAsync().ConfigureAwait(false);
var symbolInfos = symbolResult.Data.SymbolData;
```

> Yukarıdaki örnekler RestAPI'nin halka açık fonksiyonlarının kullanımına örnek olarak verilmiştir. Daha fazlası için kütüphaneyi indirip kullanabilirsiniz.

## Bağış Yap
Kütüphaneyi kullanıp beğendiyseniz destek olmak amaçlı bağışta bulunabilirsiniz. Aşağıda Bitcoin ve Ethereum için cüzdan adreslerim yer almaktadır.

<img src="https://cdn.worldvectorlogo.com/logos/tether-1.svg" width="36px"> -> **Tether (USDT) - TRC20:** `TC3ruh9qWbwAnCHGEkschnmcYUNxGumHJS`

<img src="https://cdn.worldvectorlogo.com/logos/bitcoin.svg" width="36px"> -> **Bitcoin (BTC) - ERC20:** `0x4a656a72fada0ccdef737ad8cc2e39686af5efbe`

<img src="https://cdn.worldvectorlogo.com/logos/ethereum-1.svg" width="28px"> -> **Ethereum - ETH:** `0x4a656a72fada0ccdef737ad8cc2e39686af5efbe`