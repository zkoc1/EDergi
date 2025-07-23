using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class MDocumentService : IMDocumentService
	{
		private readonly IReadRepository<MDocument> _readRepository;
		private readonly IWriteRepository<MDocument> _writeRepository;

		public MDocumentService(IReadRepository<MDocument> readRepository, IWriteRepository<MDocument> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<MDocument>> GetAllAsync()
		{
			var documents = _readRepository.GetAll().ToList();
			return Task.FromResult(documents);
		}

		public async Task<MDocument> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(d => d.Id == id);
		}

		public async Task CreateAsync(MDocument document)
		{
			await _writeRepository.AddAsync(document);
		}

		public async Task UpdateAsync(MDocument document)
		{
			await _writeRepository.UpdateAsync(document);
		}

		public async Task DeleteAsync(Guid id)
		{
			var document = await _readRepository.GetSingleAsync(d => d.Id == id);
			if (document != null)
			{
				await _writeRepository.RemoveAsync(document);
			}
		}
	}
}
