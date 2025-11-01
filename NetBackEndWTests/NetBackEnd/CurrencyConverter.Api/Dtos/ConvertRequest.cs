
namespace CurrencyConverter.Api.Dtos
{
    public class ConvertRequest
    {
        public decimal Amount { get; set; }
        public string FromCurrency { get; set; } = "";
        public string ToCurrency { get; set; } = "";
    }
}
