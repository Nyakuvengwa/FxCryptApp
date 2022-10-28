using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FxCryptApp.Data.Entities;

[Table(nameof(BtcUsdPrice))]
public class BtcUsdPrice
{
	[Key]
	public long Id { get; set; }
	public decimal Price { get; set; }
	public DateTimeOffset SeededDate { get; set; }

	[ForeignKey(nameof(TickerSource))]
	public long TickerSourceId { get; set; }
	public TickerSource TickerSource { get; set; }
}
