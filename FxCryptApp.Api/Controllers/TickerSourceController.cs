using FxCryptApp.Common.Models;
using FxCryptApp.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FxCryptApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class TickerSourceController : ControllerBase
	{
		readonly IMediator _mediator;

		public TickerSourceController(IMediator mediator)
		{
			_mediator = mediator;
		}
		/// <summary>
		/// Provides all the available ticker sources for BTC/USD
		/// </summary>
		/// <returns>A list of all the available ticker sources</returns>
		/// <response code="200">Returns a list of all available sources</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]	
		public async Task<ActionResult<List<TickerSourceResponse>>> GetAll(CancellationToken cancellationToken)
		{
			var results = await _mediator.Send(new GetAllTickerSourcesQuery(), cancellationToken);
			return Ok(results);
		}
	}
}
