using DergiAPI.Domain.Entitites;
using DergiAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DergiAPI.Application.Repostories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : class
	{
		protected readonly EDergiAPIDbContext _context;
		public WriteRepository(EDergiAPIDbContext context)
		{
			_context = context;
		}
		public DbSet<T> Table => throw new NotImplementedException();

		public Task<bool> Addasync(T model)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Addasync(List<T> model)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(MNumberOf archive)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(Article article)
		{
			throw new NotImplementedException();
		}


		public Task<bool> AddAsync(Author author)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(Issue issue)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(MDocument document)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(Magazine magazine)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(Publisher publisher)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(ReadIndex readIndex)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(ViewStats viewStats)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(Volume volume)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(ArticleIssue articleIssue)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task<List<MNumberOf>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<MNumberOf> GetByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public void Remove(Issue issue)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAsync(T model)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAsync(MNumberOf author)
		{
			throw new NotImplementedException();
		}

		public Task SaveAsync()
		{
			throw new NotImplementedException();
		}


		public void Update(Issue issue)
		{
			throw new NotImplementedException();
		}

		public void Update(Magazine magazine)
		{
			throw new NotImplementedException();
		}

		public void Update(Publisher publisher)
		{
			throw new NotImplementedException();
		}


		public void Update(ViewStats viewStats)
		{
			throw new NotImplementedException();
		}

		public void Update(Volume volume)
		{
			throw new NotImplementedException();
		}


		public Task<bool> UpdateAsync(T model)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(MNumberOf archive)
		{
			throw new NotImplementedException();
		}
	}
}
