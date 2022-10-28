using FxCryptApp.Data.Entities;
using MediatR;

namespace FxCryptApp.Services.Commands;

public class SaveBtcUsdPriceCommand : INotification
{
	public BtcUsdPrice	BtcUsdPrice { get; }

	public SaveBtcUsdPriceCommand(BtcUsdPrice btcUsdPrice)
	{
		BtcUsdPrice = btcUsdPrice;
	}
}

