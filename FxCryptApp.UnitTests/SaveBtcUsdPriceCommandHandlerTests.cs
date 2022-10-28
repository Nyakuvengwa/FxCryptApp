namespace FxCryptApp.UnitTests;

public class SaveBtcUsdPriceCommandHandlerTests
{
	[Fact]
	public void SaveBtcUsdPriceCommandHandler_ShouldReturnCompletedTask_WhenProvidedAValidRequest()
	{
		var btcUsdPriceRepositoryMock = new Mock<IBtcUsdPriceRepository>();
		var loggerMock = new Mock<ILogger<SaveBtcUsdPriceCommandHandler>>();

		btcUsdPriceRepositoryMock.Setup(x => x.Add(It.IsAny<BtcUsdPrice>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

		var handler = new SaveBtcUsdPriceCommandHandler(btcUsdPriceRepositoryMock.Object, loggerMock.Object);

		SaveBtcUsdPriceCommand request = new SaveBtcUsdPriceCommand(new BtcUsdPrice { Price = 100M, SeededDate = DateTimeOffset.UtcNow, TickerSourceId = 1 });
		var result = handler.Handle(request, default);

		Assert.NotNull(result);
		Assert.True(result.IsCompleted);
	}
	[Fact]
	public async Task SaveBtcUsdPriceCommandHandler_ThrowsArgumentNullException_WhenRequestIsNull()
	{
		var btcUsdPriceRepositoryMock = new Mock<IBtcUsdPriceRepository>();
		var loggerMock = new Mock<ILogger<SaveBtcUsdPriceCommandHandler>>();

		btcUsdPriceRepositoryMock.Setup(x => x.Add(It.IsAny<BtcUsdPrice>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

		var handler = new SaveBtcUsdPriceCommandHandler(btcUsdPriceRepositoryMock.Object, loggerMock.Object);

		var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, default));
	}

	[Fact]
	public async Task SaveBtcUsdPriceCommandHandler_ThrowsArgumentNullException_WhenBtcUsdPriceRequestIsNull()
	{
		var btcUsdPriceRepositoryMock = new Mock<IBtcUsdPriceRepository>();
		var loggerMock = new Mock<ILogger<SaveBtcUsdPriceCommandHandler>>();

		btcUsdPriceRepositoryMock.Setup(x => x.Add(It.IsAny<BtcUsdPrice>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
		SaveBtcUsdPriceCommand request = new SaveBtcUsdPriceCommand(null);

		var handler = new SaveBtcUsdPriceCommandHandler(btcUsdPriceRepositoryMock.Object, loggerMock.Object);

		var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request, default));
	}
}