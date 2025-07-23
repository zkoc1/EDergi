using DergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
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

			public async Task<List<Author>> GetAllAsync()
			{
				// Not: ToList çağrısı async değil ama uygun
				return _readRepository.GetAll().ToList();
			}

			public async Task<Author> GetByIdAsync(Guid id)
			{
				return await _readRepository.GetSingleAsync(a => a.Id == id);
			}

			public async Task CreateAsync(Author author)
			{
				await _writeRepository.Addasync(author);
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

		public Task<Author> GetByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(long id)
		{
			throw new NotImplementedException();
		}
	}
	}


