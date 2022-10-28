using AutoMapper;
using FxCryptApp.Common.Models;
using FxCryptApp.Data.DataAccess.Abstractions;
using MediatR;

namespace FxCryptApp.Services.Queries;

public class GetAllTickerSourcesQueryHandler : IRequestHandler<GetAllTickerSourcesQuery, List<TickerSourceResponse>>
{
	readonly ITickerSourceRepository _tickerSourceRepository;
	private readonly IMapper _mapper;

	public GetAllTickerSourcesQueryHandler(ITickerSourceRepository tickerSourceRepository, IMapper mapper)
	{
		_tickerSourceRepository = tickerSourceRepository;
		_mapper = mapper;
	}
	public async Task<List<TickerSourceResponse>> Handle(GetAllTickerSourcesQuery request, CancellationToken cancellationToken)
	{
		var result = await _tickerSourceRepository.GetAll(cancellationToken);
		return result?.Select(x => _mapper.Map<TickerSourceResponse>(x)).ToList() ?? new List<TickerSourceResponse>();
	}
}
