using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BinanceTR.Core.Helpers;

public static class RequestHelper
{
    public static string BuildQueryString(Dictionary<string, object>? parameters, bool signed, string? secretKey = null)
    {
        if (parameters == null || parameters.Count == 0)
            parameters = [];

        if (signed)
        {
            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("Secret key is required for signed requests");

            parameters["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        var queryParams = parameters
            .Where(p => p.Value != null)
            .Select(p => $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value.ToString())}")
            .ToArray();

        var queryString = string.Join("&", queryParams);

        if (signed && !string.IsNullOrEmpty(queryString) && !string.IsNullOrEmpty(secretKey))
        {
            var signature = CreateSignature(queryString, secretKey);
            queryString += $"&signature={signature}";
        }

        return queryString;
    }

    public static string CreateSignature(string queryString, string secretKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var messageBytes = Encoding.UTF8.GetBytes(queryString);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(messageBytes);
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}
