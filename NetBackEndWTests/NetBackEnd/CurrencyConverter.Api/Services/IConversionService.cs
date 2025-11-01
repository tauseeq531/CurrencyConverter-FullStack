
namespace CurrencyConverter.Api.Services
{
    public interface IConversionService
    {
        Task<(decimal converted, decimal rate, DateTime ts)> ConvertAsync(decimal amount, string from, string to, CancellationToken ct = default);
    }
}
