using AutoMapper;
using FxCryptApp.Common.Models;
using FxCryptApp.Data.Entities;

namespace FxCryptApp.Common.Mapping;

public class FxCryptAppProfile : Profile
{
	public FxCryptAppProfile()
	{
		CreateMap<TickerSourceResponse, TickerSource>().ReverseMap();
		CreateMap<BtcUsdPrice, GetBtcUsdPriceResponse>()
			.ForMember(x => x.Price, opt => opt.ConvertUsing(new CurrencyFormatter()));
	}

	
}
public class CurrencyFormatter : IValueConverter<decimal, string>
	{
		public string Convert(decimal source, ResolutionContext context)
			=> source.ToString("f2");
	}