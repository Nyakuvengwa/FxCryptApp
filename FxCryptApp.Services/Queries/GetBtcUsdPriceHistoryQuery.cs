using FxCryptApp.Common.Models;
using MediatR;

namespace FxCryptApp.Services.Queries;

public class GetBtcUsdPriceHistoryQuery : IRequest<List<GetBtcUsdPriceResponse>>
{
	public long TickerSourceId { get; }

	public GetBtcUsdPriceHistoryQuery(long tickerSourceId)
	{
		TickerSourceId = tickerSourceId;
	}
}
