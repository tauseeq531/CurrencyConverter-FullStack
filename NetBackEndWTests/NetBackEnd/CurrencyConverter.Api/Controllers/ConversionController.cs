
using AutoMapper;
using CurrencyConverter.Api.Dtos;
using CurrencyConverter.Api.Models;
using CurrencyConverter.Api.Repositories;
using CurrencyConverter.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Api.Controllers
{
    [ApiController]
    [Route("api/conversion")]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversion;
        private readonly IConversionRepository _repo;
        private readonly IMapper _mapper;

        public ConversionController(IConversionService conversion, IConversionRepository repo, IMapper mapper)
        {
            _conversion = conversion;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ConvertResponse>> Convert([FromBody] ConvertRequest request, CancellationToken ct)
        {
            if (request == null)
            {
                return BadRequest("Request is required.");
            }
            if (string.IsNullOrWhiteSpace(request.FromCurrency) || string.IsNullOrWhiteSpace(request.ToCurrency))
            {
                return BadRequest("Currency codes are required.");
            }

            var (converted, rate, ts) = await _conversion.ConvertAsync(request.Amount, request.FromCurrency, request.ToCurrency, ct);

            var entity = new Conversion
            {
                FromAmount = request.Amount,
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                ToAmount = converted,
                RateUsed = rate,
                DateTimeUtc = DateTime.UtcNow,
                UserId = 1
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveAsync(ct);

            var response = _mapper.Map<ConvertResponse>(entity);
            return Ok(response);
        }

        [HttpGet("history")]

        public async Task<ActionResult<IEnumerable<ConvertResponse>>> History(CancellationToken ct = default)
        {
            var list = await _repo.Query()
                .OrderByDescending(x => x.Id) 
                .Take(10)
                .ToListAsync(ct);

            var result = list.Select(x => _mapper.Map<ConvertResponse>(x)).ToList();
            return Ok(result);
        }
    }
}
