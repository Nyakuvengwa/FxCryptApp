using FxCryptApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FxCryptApp.Data;

public class FxCryptAppDbContext : DbContext
{
	public DbSet<TickerSource> TickerSources { get; set; }
	public DbSet<BtcUsdPrice> BtcUsdPrices { get; set; }
	public FxCryptAppDbContext(DbContextOptions options) : base(options)
	{	
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<TickerSource>().HasData(new List<TickerSource>()
		{
			new TickerSource()
			{
				RetryLimit = 3,
				EndpointUrl = "https://www.bitstamp.net/api/v2/ticker/btcusd/",
				Name = "Bitstamp API",
				Id = 1,
			},
			new TickerSource()
			{
				RetryLimit = 3,
				EndpointUrl = "https://api.bitfinex.com/v1/pubticker/btcusd/",
				Name = "Bitfinex API",
				Id = 2,
			},
		});
		base.OnModelCreating(modelBuilder);
	}
}
