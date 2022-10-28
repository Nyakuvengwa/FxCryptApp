namespace FxCryptApp.Common.Models;

public class TickerSourceResponse
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string EndpointUrl { get; set; }
	public long RetryLimit { get; set; }
}
