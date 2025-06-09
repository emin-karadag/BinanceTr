using BinanceTR.Core.Helpers;
using System.Text.Json;

namespace BinanceTR.Services;

public class BinanceTrHttpClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string? _apiKey;
    private readonly string? _secretKey;
    private bool _disposed = false;

    public BinanceTrHttpClient(string? apiKey = null, string? secretKey = null)
    {
        _apiKey = apiKey;
        _secretKey = secretKey;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://www.binance.tr")
        };

        if (!string.IsNullOrEmpty(_apiKey))
        {
            _httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", _apiKey);
        }

        _jsonOptions = BinanceTrHelper.DefaultJsonSerializerOptions;
    }



    public async Task<T?> SendRequestAsync<T>(HttpMethod method, string endpoint, string? baseUrl = null, Dictionary<string, object>? parameters = null, bool signed = false, CancellationToken ct = default)
    {
        var queryString = RequestHelper.BuildQueryString(parameters, signed, _secretKey);
        var requestUri = $"{endpoint}{(string.IsNullOrEmpty(queryString) ? "" : "?" + queryString)}";

        var fullUri = !string.IsNullOrEmpty(baseUrl)
            ? new Uri(new Uri(baseUrl), requestUri)
            : new Uri(_httpClient.BaseAddress!, requestUri);

        using var request = new HttpRequestMessage(method, fullUri);

        using var response = await _httpClient.SendAsync(request, ct).ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false);

        var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);

        if (result != null)
        {
            var rawResponseProperty = result.GetType().GetProperty("RawResponse");
            rawResponseProperty?.SetValue(result, content);
        }

        return result;
    }

    public Task<T?> GetAsync<T>(string endpoint, string? baseUrl = null, CancellationToken ct = default)
        => SendRequestAsync<T>(HttpMethod.Get, endpoint, baseUrl, ct: ct);

    public Task<T?> GetAsync<T>(string endpoint, Dictionary<string, object>? parameters = null, bool signed = false, CancellationToken ct = default)
        => SendRequestAsync<T>(HttpMethod.Get, endpoint, null, parameters, signed, ct: ct);

    public Task<T?> PostAsync<T>(string endpoint, Dictionary<string, object>? parameters = null, bool signed = false, CancellationToken ct = default)
        => SendRequestAsync<T>(HttpMethod.Post, endpoint, null, parameters, signed, ct: ct);

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
