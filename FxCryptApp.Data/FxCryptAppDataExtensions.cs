using FxCryptApp.Data.DataAccess;
using FxCryptApp.Data.DataAccess.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FxCryptApp.Data;

public static class FxCryptAppDataExtensions
{
	public static void RegisterFxCryptAppRepositories(this IServiceCollection services)
	{
		services.AddTransient<ITickerSourceRepository, TickerSourceRepository>();
		services.AddTransient<IBtcUsdPriceRepository, BtcUsdPriceRepository>();
	}
}
