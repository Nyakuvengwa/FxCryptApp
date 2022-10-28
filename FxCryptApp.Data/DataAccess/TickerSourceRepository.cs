using System.Linq.Expressions;
using FxCryptApp.Data.DataAccess.Abstractions;
using FxCryptApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FxCryptApp.Data.DataAccess;

public class TickerSourceRepository : ITickerSourceRepository
{
	readonly FxCryptAppDbContext _dbContext;

	public TickerSourceRepository(FxCryptAppDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task<List<TickerSource>> Find(Expression<Func<TickerSource, bool>> expression, CancellationToken cancellationToken = default)
	{
		return await _dbContext.TickerSources
			.Where(expression)
			.ToListAsync(cancellationToken);
	}

	public async Task<List<TickerSource>> GetAll(CancellationToken cancellationToken = default)
	{
		return await _dbContext.TickerSources
			.ToListAsync(cancellationToken);
	}

	public async Task<TickerSource> GetById(long id, CancellationToken cancellationToken = default)
	{
		return await _dbContext.TickerSources
				.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
	}
}
