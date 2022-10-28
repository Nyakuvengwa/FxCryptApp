using System.Linq.Expressions;
using FxCryptApp.Data.DataAccess.Abstractions;
using FxCryptApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FxCryptApp.Data.DataAccess;

public class BtcUsdPriceRepository : IBtcUsdPriceRepository
{
	readonly FxCryptAppDbContext _dbContext;

	public BtcUsdPriceRepository(FxCryptAppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Add(BtcUsdPrice entity, CancellationToken cancellationToken = default)
	{
		await _dbContext.AddAsync(entity, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<List<BtcUsdPrice>> Find(Expression<Func<BtcUsdPrice, bool>> expression, CancellationToken cancellationToken = default)
	{
		return await _dbContext.BtcUsdPrices
			.Where(expression)
			.ToListAsync(cancellationToken);
	}

	public async Task<List<BtcUsdPrice>> GetAll(CancellationToken cancellationToken = default)
	{
		return await _dbContext.BtcUsdPrices.ToListAsync(cancellationToken);
	}

	public async Task<BtcUsdPrice> GetById(long id, CancellationToken cancellationToken = default)
	{
		return await _dbContext.BtcUsdPrices.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}
}
