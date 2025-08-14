using EDergi.Application.Repostories;
using EDergi.Domain.Entitites.Commmon;
using EDergi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EDergi.Persistence.Repostories
{
	public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
	{
		private readonly EDergiDbContext _context;

		public ReadRepository(EDergiDbContext context)
		{
			_context = context;
		}

		private DbSet<T> Table => _context.Set<T>();

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
			return await Table.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);
		}

		public Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
		{
			throw new NotImplementedException();
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
		{
			return await Table.AsNoTracking().FirstOrDefaultAsync(predicate);
		}
		public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
		{
			return await _context.Set<T>().Where(predicate).ToListAsync();
		}


		public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
		{
			return Table.Where(predicate).AsNoTracking();
		}
	}
}
