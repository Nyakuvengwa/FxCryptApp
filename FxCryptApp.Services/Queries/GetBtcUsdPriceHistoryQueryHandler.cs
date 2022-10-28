using AutoMapper;
using FxCryptApp.Common.Models;
using FxCryptApp.Data.DataAccess.Abstractions;
using MediatR;

namespace FxCryptApp.Services.Queries;

public class GetBtcUsdPriceHistoryQueryHandler : IRequestHandler<GetBtcUsdPriceHistoryQuery, List<GetBtcUsdPriceResponse>>
{
	readonly IBtcUsdPriceRepository _btcUsdPriceRepository;
	readonly IMapper _mapper;

	public GetBtcUsdPriceHistoryQueryHandler(IBtcUsdPriceRepository btcUsdPriceRepository, IMapper mapper)
	{
		_btcUsdPriceRepository = btcUsdPriceRepository;
		_mapper = mapper;
	}
	public async Task<List<GetBtcUsdPriceResponse>> Handle(GetBtcUsdPriceHistoryQuery request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		if (request.TickerSourceId.Equals(default))
		{
			throw new ArgumentException(nameof(request.TickerSourceId));
		}

		var result = await _btcUsdPriceRepository.Find(x => x.TickerSourceId == request.TickerSourceId, cancellationToken);
		return result?.Select(x => _mapper.Map<GetBtcUsdPriceResponse>(x)).ToList() ?? new List<GetBtcUsdPriceResponse>();
	}
}
