
using AutoMapper;
using CurrencyConverter.Api.Dtos;
using CurrencyConverter.Api.Models;

namespace CurrencyConverter.Api.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Conversion, ConvertResponse>()
                .ForMember(d => d.ConvertedAmount, o => o.MapFrom(s => s.ToAmount))
                .ForMember(d => d.Rate,           o => o.MapFrom(s => s.RateUsed))
                .ForMember(d => d.TimeUtc,        o => o.MapFrom(s => s.DateTimeUtc))
                .ForMember(d => d.FromCurrency,   o => o.MapFrom(s => s.FromCurrency))
                .ForMember(d => d.ToCurrency,     o => o.MapFrom(s => s.ToCurrency));
        }
    }
}
