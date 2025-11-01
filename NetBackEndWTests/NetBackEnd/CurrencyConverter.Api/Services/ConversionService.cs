
namespace CurrencyConverter.Api.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IExchangeRateService _rates;
        public ConversionService(IExchangeRateService rates) => _rates = rates;

        public async Task<(decimal converted, decimal rate, DateTime ts)> ConvertAsync(decimal amount, string from, string to, CancellationToken ct = default)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            if (string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("From currency is required.", nameof(from));
            }
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("To currency is required.", nameof(to));
            }

            var baseCurrencyCode = from.ToUpperInvariant();
            var (timestampUtc, latestRates) = await _rates.GetLatestRatesAsync(baseCurrencyCode, ct);

            var targetCurrencyCode = to.ToUpperInvariant();
            if (!latestRates.TryGetValue(targetCurrencyCode, out var conversionRate))
                throw new InvalidOperationException($"Unsupported target currency: {to}");

            var convertedAmount = amount * conversionRate;
            return (
                decimal.Round(convertedAmount, 4),
                decimal.Round(conversionRate, 6),
                timestampUtc
            );
        }
    }
}
