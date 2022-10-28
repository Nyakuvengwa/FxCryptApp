namespace FxCryptApp.UnitTests;

public class GetAllTickerSourcesQueryHandlerTests
{
	[Fact]
	public async Task GetAllTickerSourcesQueryHandler_ShouldReturnAList_WhenInvoked()
	{
		var tickerSourceRepositoryMock = new Mock<ITickerSourceRepository>();
		tickerSourceRepositoryMock
			.Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
			.ReturnsAsync(new List<Data.Entities.TickerSource>() { new TickerSource() { EndpointUrl = "http://example.com", Id = 1, Name = "Test", RetryLimit = 1 } });
		
		var mapperMock = new Mock<AutoMapper.IMapper>();
		mapperMock
			.Setup(m => m.Map<TickerSourceResponse>(It.IsAny<TickerSource>()))
			.Returns(new TickerSourceResponse() { EndpointUrl = "http://example.com", Id = 1, Name = "Test", RetryLimit = 1 });

		var handler = new GetAllTickerSourcesQueryHandler(tickerSourceRepositoryMock.Object, mapperMock.Object);

		var results = await handler.Handle(new GetAllTickerSourcesQuery(), default);

		Assert.NotNull(results);
		Assert.NotEmpty(results);
	}

	[Fact]
	public async Task GetAllTickerSourcesQueryHandler_ShouldReturnEmptyList_WhenDbDoesNotContainSources()
	{
		var tickerSourceRepositoryMock = new Mock<ITickerSourceRepository>();
		var mapperMock = new Mock<AutoMapper.IMapper>();
		mapperMock
			.Setup(m => m.Map<TickerSourceResponse>(It.IsAny<TickerSource>()))
			.Returns(new TickerSourceResponse() { EndpointUrl = "http://example.com", Id = 1, Name = "Test", RetryLimit = 1 });

		var handler = new GetAllTickerSourcesQueryHandler(tickerSourceRepositoryMock.Object, mapperMock.Object);

		var results = await handler.Handle(new GetAllTickerSourcesQuery(), default);

		Assert.NotNull(results);
		Assert.Empty(results);
	}
}
