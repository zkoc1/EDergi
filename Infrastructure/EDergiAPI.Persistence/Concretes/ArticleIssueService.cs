using DergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using Microsoft.EntityFrameworkCore;

namespace DergiAPI.Persistence.Concretes
{
	public class ArticleIssueService : IArticleIssueService
	{
		private readonly IWriteRepository<ArticleIssue> _writeRepository;
		private readonly IReadRepository<ArticleIssue> _readRepository;

		public ArticleIssueService(IWriteRepository<ArticleIssue> writeRepository, IReadRepository<ArticleIssue> readRepository)
		{
			_writeRepository = writeRepository;
			_readRepository = readRepository;
		}

		public async Task<List<ArticleIssue>> GetAllAsync()
		{
			return await _readRepository.GetAll().ToListAsync();
		}

		public async Task<ArticleIssue?> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task AddAsync(ArticleIssue articleIssue)
		{
			await _writeRepository.AddAsync(articleIssue);
			await _writeRepository.SaveAsync();
		}

		public async Task UpdateAsync(ArticleIssue articleIssue)
		{
			_writeRepository.Update(articleIssue);
			await _writeRepository.SaveAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			await _writeRepository.RemoveAsync(id);
			await _writeRepository.SaveAsync();
		}
	}
}
