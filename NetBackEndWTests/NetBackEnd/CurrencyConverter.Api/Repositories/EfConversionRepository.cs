
using CurrencyConverter.Api.Data;
using CurrencyConverter.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Api.Repositories
{
    public class EfConversionRepository : IConversionRepository
    {
        private readonly AppDbContext _db;
        public EfConversionRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Conversion> Query()
        {
            return _db.Conversions.AsNoTracking().OrderByDescending(x => x.DateTimeUtc);
        }
        public async Task AddAsync(Conversion item, CancellationToken ct = default)
        {
            await _db.Conversions.AddAsync(item, ct);
        }

        public Task SaveAsync(CancellationToken ct = default)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
