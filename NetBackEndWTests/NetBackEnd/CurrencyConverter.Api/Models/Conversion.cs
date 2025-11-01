
using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Api.Models
{
    public class Conversion
    {
        public int Id { get; set; }

        public string FromCurrency { get; set; } = "";

        public string ToCurrency { get; set; } = "";

        public decimal FromAmount { get; set; }

        public decimal ToAmount { get; set; }

        public decimal RateUsed { get; set; }

        public DateTime DateTimeUtc { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
    }
}
