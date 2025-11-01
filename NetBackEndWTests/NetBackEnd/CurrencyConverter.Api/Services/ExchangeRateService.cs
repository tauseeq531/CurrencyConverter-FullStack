
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CurrencyConverter.Api.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;

        public ExchangeRateService(HttpClient http, IConfiguration config, IMemoryCache cache)
        {
            _http = http;
            _config = config;
            _cache = cache;
        }

        public IEnumerable<string> GetKnownCurrencies()
        {
            return new[] { "USD","EUR","GBP","SAR","AED","PKR","INR","CAD","AUD","JPY","CNY" };
        }

        public async Task<(DateTime timestampUtc, Dictionary<string, decimal> rates)> GetLatestRatesAsync(string baseCurrency, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(baseCurrency))
            { 
                baseCurrency = "USD"; 
            }

            baseCurrency = baseCurrency.ToUpperInvariant();

            var cacheKey = $"rates:{baseCurrency}";
            if (_cache.TryGetValue(cacheKey, out (DateTime, Dictionary<string, decimal>) cached))
                return cached;

            var apiKey = _config["OpenExchangeRates:ApiKey"];
            var baseUrl = _config["OpenExchangeRates:BaseUrl"];
            var url = $"{baseUrl}/latest.json?app_id={apiKey}";

            using var resp = await _http.GetAsync(url, ct);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync(ct);

            using var doc = JsonDocument.Parse(json);
            var tsUnix = doc.RootElement.GetProperty("timestamp").GetInt64();
            var ts = DateTimeOffset.FromUnixTimeSeconds(tsUnix).UtcDateTime;
            var ratesProp = doc.RootElement.GetProperty("rates");

            var usdRates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            foreach (var prop in ratesProp.EnumerateObject())
            {
                if (prop.Value.TryGetDecimal(out var d)) usdRates[prop.Name] = d;
            }

            Dictionary<string, decimal> finalRates = usdRates;
            if (!baseCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase) &&
                usdRates.TryGetValue(baseCurrency, out var baseRate) && baseRate != 0m)
            {
                finalRates = usdRates.ToDictionary(k => k.Key, v => v.Value / baseRate, StringComparer.OrdinalIgnoreCase);
            }

            var ttlSeconds = int.TryParse(_config["OpenExchangeRates:CacheSeconds"], out var s) ? s : 3600;
            _cache.Set(cacheKey, (ts, finalRates), TimeSpan.FromSeconds(ttlSeconds));

            return (ts, finalRates);
        }
    }
}
