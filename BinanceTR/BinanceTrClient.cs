using BinanceTR.Interfaces;
using BinanceTR.Services;

namespace BinanceTR;

public class BinanceTrClient : IDisposable
{
    private readonly BinanceTrHttpClient _httpClient;
    private bool _disposed = false;

    private readonly Lazy<IPublicService> _lazyPublicService;
    private readonly Lazy<IPrivateService> _lazyPrivateService;

    public IPublicService Public => _lazyPublicService.Value;
    public IPrivateService Private => _lazyPrivateService.Value;

    public BinanceTrClient()
    {
        _httpClient = new BinanceTrHttpClient();
        _lazyPublicService = new Lazy<IPublicService>(() => new PublicService(_httpClient));
        _lazyPrivateService = new Lazy<IPrivateService>(new PrivateService(_httpClient));
    }

    public BinanceTrClient(string apiKey, string secretKey)
    {
        if (string.IsNullOrEmpty(apiKey))
            throw new ArgumentException("API Key cannot be null or empty", nameof(apiKey));

        if (string.IsNullOrEmpty(secretKey))
            throw new ArgumentException("Secret Key cannot be null or empty", nameof(secretKey));

        _httpClient = new BinanceTrHttpClient(apiKey, secretKey);
        _lazyPublicService = new Lazy<IPublicService>(() => new PublicService(_httpClient));
        _lazyPrivateService = new Lazy<IPrivateService>(() => new PrivateService(_httpClient));
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _httpClient?.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}
