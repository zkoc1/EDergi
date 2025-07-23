using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;

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

		public async Task<List<Article>> GetAllAsync()
		{
			return _readRepository.GetAll().ToList(); 
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

		public Task<Article> GetByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(long id)
		{
			throw new NotImplementedException();
		}
	}
}
