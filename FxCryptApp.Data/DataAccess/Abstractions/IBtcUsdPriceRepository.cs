using FxCryptApp.Data.Entities;

namespace FxCryptApp.Data.DataAccess.Abstractions;

public interface IBtcUsdPriceRepository : IGenericRepository<BtcUsdPrice>
{
	public Task Add(BtcUsdPrice entity, CancellationToken cancellationToken = default);
}
