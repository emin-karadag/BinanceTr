using BinanceTR.Models;
using BinanceTR.Models.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Core.Utilities
{
    public static class RequestHelper
    {
        public static async Task<string> SendRequestAsync(HttpMethod method, string url, BinanceTrOptions options, Dictionary<string, string> parameters = null, CancellationToken ct = default)
        {
            try
            {
                using var httpClient = new HttpClient();
                var requestUri = BinanceTrHelper.GetRequestUrl(url, true);
                var requestMessage = new HttpRequestMessage(method, requestUri);
                requestMessage.Headers.Add("X-MBX-APIKEY", options.ApiKey);

                if (method == HttpMethod.Get)
                    requestMessage.RequestUri = new Uri(requestMessage.RequestUri.OriginalString + BinanceTrHelper.CreateQueryString(BinanceTrHelper.BuildRequest(options.ApiSecret, parameters)));
                else
                    requestMessage.Content = new FormUrlEncodedContent(BinanceTrHelper.BuildRequest(options.ApiSecret, parameters));

                var response = await httpClient.SendAsync(requestMessage, ct).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<string> SendRequestWithoutAuth(string url, Dictionary<string, string> parameters = null, bool baseUrl = false, CancellationToken ct = default)
        {
            try
            {
                using var httpClient = new HttpClient();
                var requestUri = BinanceTrHelper.GetRequestUrl(url, baseUrl);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                requestMessage.RequestUri = new Uri(requestMessage.RequestUri.OriginalString + BinanceTrHelper.CreateQueryString(BinanceTrHelper.BuildRequest(null, parameters)));

                var response = await httpClient.SendAsync(requestMessage, ct).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string CheckResult(string result)
        {
            if (result != "[]" && result != "" && result.Contains("msg"))
            {
                if (result.StartsWith("["))
                {
                    var messages = "";
                    JsonSerializer.Deserialize<List<ErrorModel>>(result).ForEach(p => messages += p.Msg + "\n");
                    result = messages;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<ErrorModel>(result);
                    return error.Code > 0 ? error.Msg : result;
                }
            }
            return result;
        }
    }
}
