using System.Net;
using AutoMapper;
using FxCryptApp.Common.ApplicationExceptions;
using Moq.Protected;

namespace FxCryptApp.UnitTests;

public class GetBtcUsdPriceCommandHandlerTest
{
	[Theory]
	[InlineData(@"{""mid"":""20632.5"",""bid"":""20631.0"",""ask"":""20634.0"",""last_price"":""20625.0"",""low"":""20460.0"",""high"":""21011.0"",""volume"":""2493.78869475"",""timestamp"":""1666883683.168351""}")]
	[InlineData(@"{""timestamp"": ""1666884090"", ""open"": ""20771"", ""high"": ""20996"", ""low"": ""20450"", ""last"": ""20597"", ""volume"": ""1603.67401015"", ""vwap"": ""20710"", ""bid"": ""20590"", ""ask"": ""20593"", ""open_24"": ""20857"", ""percent_change_24"": ""-1.25""}")]
	public async Task GetBtcUsdPriceCommandHandler_ReturnsNewObject_WhenValidTickerSourceIsProvidedAsync(string httpResponse)
	{
		var loggerMock = new Mock<ILogger<GetBtcUsdPriceCommandHandler>>();

		var mapperMock = new Mock<IMapper>();
		mapperMock.Setup(x => x.Map<GetBtcUsdPriceResponse>(It.IsAny<BtcUsdPrice>())).Returns(new GetBtcUsdPriceResponse()
		{
			Id = 1,
			Price = 20590.ToString("F2"),
			SeededDate = DateTimeOffset.UtcNow,
			TickerSourceId = 1,
		});

		var response = new HttpResponseMessage
		{
			StatusCode = HttpStatusCode.OK,
			Content = new StringContent(httpResponse),
		};
		var handlerMock = new Mock<HttpMessageHandler>();
		handlerMock
		   .Protected()
		   .Setup<Task<HttpResponseMessage>>(
			  "SendAsync",
			  ItExpr.IsAny<HttpRequestMessage>(),
			  ItExpr.IsAny<CancellationToken>())
		   .ReturnsAsync(response);
		var httpClient = new HttpClient(handlerMock.Object);

		var tickerSourceRepositoryMock = new Mock<ITickerSourceRepository>();
		tickerSourceRepositoryMock
			.Setup(x => x.GetById(It.IsAny<long>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(new TickerSource() { Id = 1, EndpointUrl = @"https://www.example.com/api/btcusd/", Name = "Test Api", RetryLimit = 1 });


		var mediatorMock = new Mock<MediatR.IMediator>();
		var handler = new GetBtcUsdPriceCommandHandler(
			httpClient,
			tickerSourceRepositoryMock.Object,
			loggerMock.Object,
			mapperMock.Object,
			mediatorMock.Object);

		GetBtcUsdPriceCommand request = new GetBtcUsdPriceCommand(new Common.Models.GetBtcUsdPriceRequest() { TickerSourceId = 1 });

		var result = await handler.Handle(request, default);

		// Assert
		Assert.NotNull(result);
	}

	[Fact]
	public async Task GetBtcUsdPriceCommandHandler_ReturnsThrowsNotFound_WhenTickerSourceIsNotInTheDbAsync()
	{
		var loggerMock = new Mock<ILogger<GetBtcUsdPriceCommandHandler>>();

		var mapperMock = new Mock<IMapper>();
		var handlerMock = new Mock<HttpMessageHandler>();

		var httpClient = new HttpClient(handlerMock.Object);

		var tickerSourceRepositoryMock = new Mock<ITickerSourceRepository>();
		tickerSourceRepositoryMock
			.Setup(x => x.GetById(It.IsAny<long>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(null as TickerSource);

		var mediatorMock = new Mock<MediatR.IMediator>();

		var handler = new GetBtcUsdPriceCommandHandler(
			httpClient,
			tickerSourceRepositoryMock.Object,
			loggerMock.Object,
			mapperMock.Object,
			mediatorMock.Object);

		GetBtcUsdPriceCommand request = new GetBtcUsdPriceCommand(new Common.Models.GetBtcUsdPriceRequest() { TickerSourceId = 1 });

		var exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
		// Assert
		Assert.NotNull(exception);
		Assert.Equal("Unable to retrieve the requested BTC/USD using the provided source id 1", exception.Message);
	}
}
