using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DergiAPI.Application.Repostories
{
	public interface IReadRepository<T> where T : class
	{
		IQueryable<T> GetAll();

		IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);

		Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);

		Task<T> GetByIdAsync(Guid id);

		Task<List<T>> GetAllAsync();
	}
}
