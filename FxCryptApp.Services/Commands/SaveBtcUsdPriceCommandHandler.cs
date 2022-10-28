using FxCryptApp.Data.DataAccess.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FxCryptApp.Services.Commands;

public class SaveBtcUsdPriceCommandHandler : INotificationHandler<SaveBtcUsdPriceCommand>
{
	readonly IBtcUsdPriceRepository _btcUsdPriceRepository;
	readonly ILogger<SaveBtcUsdPriceCommandHandler> _logger;

	public SaveBtcUsdPriceCommandHandler(IBtcUsdPriceRepository btcUsdPriceRepository, ILogger<SaveBtcUsdPriceCommandHandler> logger)
	{
		_btcUsdPriceRepository = btcUsdPriceRepository;
		_logger = logger;
	}
	public async Task Handle(SaveBtcUsdPriceCommand notification, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(notification, nameof(notification));
		ArgumentNullException.ThrowIfNull(notification.BtcUsdPrice, nameof(notification.BtcUsdPrice));

		await _btcUsdPriceRepository.Add(notification.BtcUsdPrice, cancellationToken);
		_logger.LogInformation("Successfully added new BTC/USD Price.");
	}
}
