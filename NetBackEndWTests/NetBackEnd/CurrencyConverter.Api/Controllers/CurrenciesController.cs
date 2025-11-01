
using CurrencyConverter.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Api.Controllers
{
    [ApiController]
    [Route("api/currencies")]
    public class CurrenciesController : ControllerBase
    {
        private readonly IExchangeRateService _rates;
        public CurrenciesController(IExchangeRateService rates)
        {
            _rates = rates;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetCurrencies()
        {
            return Ok(_rates.GetKnownCurrencies().OrderBy(x => x));
        }
    }
}
