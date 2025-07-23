using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class ArticleIssueService : IArticleIssueService
	{
		private readonly IReadRepository<ArticleIssue> _readRepository;
		private readonly IWriteRepository<ArticleIssue> _writeRepository;

		public ArticleIssueService(IReadRepository<ArticleIssue> readRepository, IWriteRepository<ArticleIssue> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<ArticleIssue>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<ArticleIssue> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(ai => ai.Id == id);
		}

		public async Task CreateAsync(ArticleIssue articleIssue)
		{
			await _writeRepository.AddAsync(articleIssue);
		}

		public async Task UpdateAsync(ArticleIssue articleIssue)
		{
			await _writeRepository.UpdateAsync(articleIssue);
		}

		public async Task DeleteAsync(Guid id)
		{
			var articleIssue = await _readRepository.GetSingleAsync(ai => ai.Id == id);
			if (articleIssue != null)
			{
				await _writeRepository.RemoveAsync(articleIssue);
			}
		}
	}
}
