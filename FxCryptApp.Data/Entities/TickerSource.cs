using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FxCryptApp.Data.Entities;

[Table(nameof(TickerSource))]
public class TickerSource
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	[Url]
	public string EndpointUrl { get; set; }

	public long RetryLimit { get; set; }
}
