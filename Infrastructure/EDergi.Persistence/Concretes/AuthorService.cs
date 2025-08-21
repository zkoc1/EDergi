using EDergi.Application.Abstractions;
using EDergi.Application.Abstractions.Services;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EDergi.Persistence.Concretes
{
	public class AuthorService : IAuthorService
	{
		private readonly IReadRepository<Author> _readRepository;
		private readonly IWriteRepository<Author> _writeRepository;
		

		public AuthorService(IReadRepository<Author> readRepository, IWriteRepository<Author> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<Author>> GetAllAsync()
		{
			var list = _readRepository.GetAll()?.ToList() ?? new List<Author>();
			return Task.FromResult(list);
		}

		public async Task<Author> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(a => a.Id == id);
		}

		public async Task CreateAsync(AuthorCreateDto dto)
		{
			var author = new Author
			{
				Id = Guid.NewGuid(),
				Name = dto.Name,
				Email = dto.Email,
				Affiliation = dto.Affiliation,
				CreatedDate = DateTime.UtcNow,
				ArticleAuthors = dto.ArticleIds != null
					? dto.ArticleIds.Select(articleId => new ArticleAuthor
					{
						ArticleId = articleId
					}).ToList()
					: new List<ArticleAuthor>()
			};

			await _writeRepository.AddAsync(author);
		}


		public async Task UpdateAsync(Author author)
		{
			await _writeRepository.UpdateAsync(author);
		}

		public async Task DeleteAsync(Guid id)
		{
			var author = await _readRepository.GetSingleAsync(a => a.Id == id);
			if (author != null)
			{
				await _writeRepository.RemoveAsync(author);
			}
		}
	}
}
