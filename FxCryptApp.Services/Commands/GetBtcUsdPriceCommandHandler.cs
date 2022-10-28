using System.Globalization;
using System.Text.Json;
using AutoMapper;
using FxCryptApp.Common.ApplicationExceptions;
using FxCryptApp.Common.Models;
using FxCryptApp.Data.DataAccess.Abstractions;
using FxCryptApp.Data.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FxCryptApp.Services.Commands;

public class GetBtcUsdPriceCommandHandler : IRequestHandler<GetBtcUsdPriceCommand, GetBtcUsdPriceResponse>
{
	readonly HttpClient _client;
	readonly ITickerSourceRepository _tickerSourceRepository;
	readonly ILogger<GetBtcUsdPriceCommandHandler> _logger;
	readonly IMapper _mapper;
	readonly IMediator _mediator;

	public GetBtcUsdPriceCommandHandler(
		HttpClient client,
		ITickerSourceRepository tickerSourceRepository,
		ILogger<GetBtcUsdPriceCommandHandler> logger,
		IMapper mapper,
		IMediator mediator)
	{
		_client = client;
		_tickerSourceRepository = tickerSourceRepository;
		_logger = logger;
		_mapper = mapper;
		_mediator = mediator;
	}
	public async Task<GetBtcUsdPriceResponse> Handle(GetBtcUsdPriceCommand request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		ArgumentNullException.ThrowIfNull(request.BtcUsdPriceRequest);

		var tickerSource = await _tickerSourceRepository.GetById(request.BtcUsdPriceRequest.TickerSourceId, cancellationToken);
		if (tickerSource is null)
		{
			_logger.LogError("Unable to retrieve the requested BTC/USD using the provided source id {TickerSourceId}", request.BtcUsdPriceRequest.TickerSourceId);
			throw new NotFoundException($"Unable to retrieve the requested BTC/USD using the provided source id {request.BtcUsdPriceRequest.TickerSourceId}");
		}

		decimal bidPrice = default;

		var response = await _client.GetAsync(tickerSource.EndpointUrl, cancellationToken);
		if (response.IsSuccessStatusCode)
		{
			using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
			var result = await JsonSerializer.DeserializeAsync<JsonDocument>(stream, cancellationToken: cancellationToken);
			if (result is null)
			{
				_logger.LogError("Unable to retrieve the requested BTC/USD using the provided source {EndpointUrl}", tickerSource.EndpointUrl);
				throw new NotFoundException("Unable to retrieve BTC/USD from requested source.");
			}

			if (result.RootElement.TryGetProperty("bid", out var bid))
			{
				var bidString = bid.GetString() ?? throw new Exception("Unable to parse bid price");
				var style = NumberStyles.Any;
				bidPrice = decimal.Parse(bidString, style, CultureInfo.InvariantCulture);
			}
		}

		var btcUSdprice = new BtcUsdPrice()
		{
			Price = bidPrice,
			SeededDate = DateTimeOffset.UtcNow,
			TickerSourceId = tickerSource.Id
		};

		await _mediator.Publish(new SaveBtcUsdPriceCommand(btcUSdprice), cancellationToken);

		var resultBtc = _mapper.Map<GetBtcUsdPriceResponse>(btcUSdprice);
		return resultBtc;
	}
}


