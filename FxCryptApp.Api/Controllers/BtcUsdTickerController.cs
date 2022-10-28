using FxCryptApp.Common.Models;
using FxCryptApp.Services.Commands;
using FxCryptApp.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FxCryptApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class BtcUsdTickerController : ControllerBase
{
	readonly IMediator _mediator;

	public BtcUsdTickerController(IMediator mediator)
	{
		_mediator = mediator;
	}

	/// <summary>
	///  Retrieves and Saves a new record of the BTC/USD price.
	/// </summary>
	/// <param name="btcUsdPriceRequest">A request containing the ticker source identifier</param>
	/// <param name="cancellationToken">A cancellation token</param>
	/// <returns>BTC/USD bid price</returns>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<GetBtcUsdPriceResponse>> GetBtcUsdPriceRequest(GetBtcUsdPriceRequest btcUsdPriceRequest, CancellationToken cancellationToken)
	{
		var results = await _mediator.Send(new GetBtcUsdPriceCommand(btcUsdPriceRequest), cancellationToken);
		return Ok(results);
	}
	/// <summary>
	/// Retrieves the fetch history BTC/USD price for a particular source.
	/// </summary>
	/// <param name="tickerSourceId">Ticker Source identifier</param>
	/// <param name="cancellationToken">A cancellation token</param>
	/// <returns></returns>
	[HttpGet("{tickerSourceId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<List<GetBtcUsdPriceResponse>>> GetBtcUsdPriceRequestByTickerSourceId(long tickerSourceId, CancellationToken cancellationToken)
	{
		var results = await _mediator.Send(new GetBtcUsdPriceHistoryQuery(tickerSourceId), cancellationToken);
		return Ok(results);
	}
}
