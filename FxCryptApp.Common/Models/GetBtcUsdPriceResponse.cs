using FxCryptApp.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FxCryptApp.Common.Models;

public class GetBtcUsdPriceResponse
{
	public long Id { get; set; }
	public string Price { get; set; }
	public DateTimeOffset SeededDate { get; set; }
	public long TickerSourceId { get; set; }
	public TickerSource TickerSource { get; set; }
}
