
using System.Collections.Generic;

namespace CurrencyConverter.Api.Services
{
    public interface IExchangeRateService
    {
        Task<(DateTime timestampUtc, Dictionary<string, decimal> rates)> GetLatestRatesAsync(string baseCurrency, CancellationToken ct = default);
        IEnumerable<string> GetKnownCurrencies();
    }
}
