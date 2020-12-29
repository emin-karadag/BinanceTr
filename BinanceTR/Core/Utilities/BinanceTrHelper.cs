using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace BinanceTR.Core.Utilities
{
    public static class BinanceTrHelper
    {
        private const string VERSION = "v3";
        private const string BASE_URL = "https://www.trbinance.com";
        private const string API_URL = "https://api.binance.cc/api/" + VERSION;

        public static long GetTimestamp()
        {
            //return (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
            var offset = new TimeSpan().TotalMilliseconds;
            return (long)(DateTime.UtcNow.AddMilliseconds(offset) - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static long GetTimestamp(DateTime dateTime)
        {
            //return (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
            var offset = new TimeSpan().TotalMilliseconds;
            return (long)(dateTime.AddMilliseconds(offset) - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static string GetRequestUrl(string url, bool baseUrl = false)
        {
            return baseUrl ? BASE_URL + url : API_URL + url;
        }

        public static string CreateQueryString(Dictionary<string, string> parameters)
        {
            if (parameters == null)
                return "";

            string[] queryStrings = new string[parameters.Count];
            for (int i = 0; i < parameters.Count; i++)
            {
                queryStrings[i] = string.Format("{0}={1}",
                    HttpUtility.UrlEncode(parameters.ElementAt(i).Key),
                    HttpUtility.UrlEncode(parameters.ElementAt(i).Value));
            }

            return "?" + string.Join("&", queryStrings);
        }

        public static Dictionary<string, string> BuildRequest(string apiSecret = null, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            //(parameters ??= new Dictionary<string, string>()).Add("recvWindow", "60000");
            parameters.Add("timestamp", GetTimestamp().ToString(CultureInfo.InvariantCulture));

            if (!string.IsNullOrEmpty(apiSecret))
                parameters.Add("signature", CreateHmac(apiSecret, parameters == null ? null : new FormUrlEncodedContent(parameters)));
            return parameters;
        }

        public static string CreateHmac(string secretKey, FormUrlEncodedContent args)
        {
            HMACSHA256 hash = new HMACSHA256(new ASCIIEncoding().GetBytes(secretKey));
            byte[] bytes = hash.ComputeHash(args.ReadAsByteArrayAsync().Result);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static bool IsJson(string text)
        {
            if (text == null)
                return true;
            try
            {
                JsonDocument.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
