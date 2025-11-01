
using CurrencyConverter.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Conversion> Conversions => Set<Conversion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversion>(b =>
            {
                b.ToTable("Conversions");
                b.HasKey(x => x.Id);
                b.Property(x => x.FromCurrency).IsRequired().HasMaxLength(3);
                b.Property(x => x.ToCurrency).IsRequired().HasMaxLength(3);
                b.Property(x => x.FromAmount).HasColumnType("decimal(18,4)");
                b.Property(x => x.ToAmount).HasColumnType("decimal(18,4)");
                b.Property(x => x.RateUsed).HasColumnType("decimal(18,6)");
                b.Property(x => x.DateTimeUtc).HasColumnType("datetime2");
                b.HasIndex(x => x.DateTimeUtc);
                b.Property(x => x.UserId).IsRequired();
            });
        }
    }
}
