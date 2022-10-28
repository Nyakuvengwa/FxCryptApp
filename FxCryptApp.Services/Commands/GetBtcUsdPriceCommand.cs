using FxCryptApp.Common.Models;
using MediatR;

namespace FxCryptApp.Services.Commands;

public class GetBtcUsdPriceCommand : IRequest<GetBtcUsdPriceResponse>
{
	public GetBtcUsdPriceRequest BtcUsdPriceRequest { get; }
	public GetBtcUsdPriceCommand(GetBtcUsdPriceRequest btcUsdPriceRequest)
	{
		BtcUsdPriceRequest = btcUsdPriceRequest;
	}
}
