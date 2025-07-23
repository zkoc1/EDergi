using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
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
			var authors = _readRepository.GetAll().ToList();
			return Task.FromResult(authors);
		}

		public async Task<Author> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(a => a.Id == id);
		}

		public async Task CreateAsync(Author author)
		{
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
