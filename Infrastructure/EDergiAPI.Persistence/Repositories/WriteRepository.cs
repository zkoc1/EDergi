using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using DergiAPI.Domain.Entitites.Commmon;
using DergiAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
	{
		protected readonly EDergiAPIDbContext _context;

		public WriteRepository(EDergiAPIDbContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		public async Task<bool> AddAsync(T model)
		{
			await Table.AddAsync(model);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> AddRangeAsync(List<T> models)
		{
			await Table.AddRangeAsync(models);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> RemoveAsync(T model)
		{
			Table.Remove(model);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> RemoveAsync(Guid id)
		{
			var entity = await Table.FindAsync(id);
			if (entity != null)
			{
				Table.Remove(entity);
				return await _context.SaveChangesAsync() > 0;
			}
			return false;
		}

		public void Update(T model)
		{
			Table.Update(model);
		}

		public async Task<bool> UpdateAsync(T model)
		{
			Table.Update(model);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task AddAsync(MNumberOf archive)
		{
			await _context.MNumbers.AddAsync(archive);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(long id)
		{
			var entity = await _context.MNumbers.FindAsync(id);
			if (entity != null)
			{
				_context.MNumbers.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<MNumberOf>> GetAllAsync()
		{
			return await _context.MNumbers.ToListAsync();
		}

		public async Task<MNumberOf> GetByIdAsync(long id)
		{
			return await _context.MNumbers.FindAsync(id);
		}

		public async Task UpdateAsync(MNumberOf archive)
		{
			_context.MNumbers.Update(archive);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(ArticleIssue articleIssue)
		{
			_context.ArticleIssues.Update(articleIssue);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Article article)
		{
			_context.Articles.Update(article);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Author author)
		{
			_context.Authors.Update(author);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(MDocument document)
		{
			_context.MDocuments.Update(document);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Issue issue)
		{
			_context.Issues.Update(issue);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Magazine magazine)
		{
			_context.Magazines.Update(magazine);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Publisher publisher)
		{
			_context.Publishers.Update(publisher);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(ReadIndex readIndex)
		{
			_context.ReadIndices.Update(readIndex);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(ViewStats viewStats)
		{
			_context.ViewStats.Update(viewStats);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Volume volume)
		{
			_context.Volumes.Update(volume);
			await _context.SaveChangesAsync();
		}

		public Task UpdateAsync(Admin admin)
		{
			throw new NotImplementedException();
		}
	}
}
