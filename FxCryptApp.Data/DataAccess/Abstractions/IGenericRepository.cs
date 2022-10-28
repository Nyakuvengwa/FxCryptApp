using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FxCryptApp.Data.DataAccess.Abstractions
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T?> GetById(long id, CancellationToken cancellationToken = default);
		Task<List<T>> GetAll(CancellationToken cancellationToken = default);
		Task<List<T>> Find(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
	}
}
