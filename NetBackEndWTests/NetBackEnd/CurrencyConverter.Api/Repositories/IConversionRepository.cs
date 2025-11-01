
using CurrencyConverter.Api.Models;

namespace CurrencyConverter.Api.Repositories
{
    public interface IConversionRepository
    {
        IQueryable<Conversion> Query();
        Task AddAsync(Conversion item, CancellationToken ct = default);
        Task SaveAsync(CancellationToken ct = default);
    }
}
