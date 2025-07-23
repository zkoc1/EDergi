using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class ArticleService : IArticleService
	{
		private readonly IReadRepository<Article> _readRepository;
		private readonly IWriteRepository<Article> _writeRepository;

		public ArticleService(IReadRepository<Article> readRepository, IWriteRepository<Article> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<Article>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<Article> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(a => a.Id == id);
		}

		public async Task CreateAsync(Article article)
		{
			await _writeRepository.AddAsync(article);
		}

		public async Task UpdateAsync(Article article)
		{
			await _writeRepository.UpdateAsync(article);
		}

		public async Task DeleteAsync(Guid id)
		{
			var article = await _readRepository.GetSingleAsync(a => a.Id == id);
			if (article != null)
			{
				await _writeRepository.RemoveAsync(article);
			}
		}
	}
}
