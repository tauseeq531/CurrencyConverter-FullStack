
using CurrencyConverter.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Api.Controllers
{
    [ApiController]
    [Route("api/rates")]
    public class RatesController : ControllerBase
    {
        private readonly IExchangeRateService _rates;
        public RatesController(IExchangeRateService rates)
        {
            _rates = rates;
        }

        [HttpGet("{baseCurrency}")]
        public async Task<ActionResult<object>> GetRates([FromRoute] string baseCurrency, CancellationToken ct)
        {
            var (timestampUtc, latestRates) = await _rates.GetLatestRatesAsync(baseCurrency, ct);

            var response = new
            {
                BaseCurrency = baseCurrency.ToUpperInvariant(),
                TimestampUtc = timestampUtc,
                Rates = latestRates
            };

            return Ok(response);
        }

    }
}
