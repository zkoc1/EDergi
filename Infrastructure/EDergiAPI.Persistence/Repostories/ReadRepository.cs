using DergiAPI.Application.Repostories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Repostories
{
	public class ReadRepository<T> : IReadRepository<T> where T : class
	{
		private readonly DbContext _context;
		public DbSet<T> Table => _context.Set<T>();

		public ReadRepository(DbContext context)
		{
			_context = context;
		}

		public IQueryable<T> GetAll()
		{
			return Table.AsNoTracking();
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await Table.AsNoTracking().ToListAsync();
		}

		public async Task<T> GetByIdAsync(Guid id)
		{
			return await Table.FindAsync(id);
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
		{
			return await Table.AsNoTracking().FirstOrDefaultAsync(predicate);
		}

		public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
		{
			return Table.Where(predicate).AsNoTracking();
		}
	}
}
