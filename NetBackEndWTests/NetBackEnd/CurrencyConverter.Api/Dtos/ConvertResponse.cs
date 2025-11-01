
using System;

namespace CurrencyConverter.Api.Dtos
{
    public class ConvertResponse
    {
        public decimal ConvertedAmount { get; set; }
        public decimal Rate { get; set; }
        public string FromCurrency { get; set; } = "";
        public string ToCurrency { get; set; } = "";
        public DateTime TimeUtc { get; set; }
    }
}
