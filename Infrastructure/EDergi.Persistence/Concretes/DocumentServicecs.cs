using EDergi.Domain.Entitites;
using EDergi.Application.Repostories;
using EDergi.Application.Abstractions;
using EDergi.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.Persistence.Services
{
	public class DocumentService : IDocumentService
	{
		private readonly IReadRepository<MDocument> _readRepository;
		private readonly IWriteRepository<MDocument> _writeRepository;

		public DocumentService(IReadRepository<MDocument> readRepository, IWriteRepository<MDocument> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<MDocument>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<MDocument> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(d => d.Id == id);
		}

		public Task<List<MDocument>> GetByMagazineIdAsync(Guid magazineId)
		{
			var documents = _readRepository.GetWhere(d => d.MagazineId == magazineId).ToList();
			return Task.FromResult(documents);
		}

		public async Task<MDocument> CreateAsync(MDocument document)
		{
			await _writeRepository.AddAsync(document);
			return document;
		}

		public async Task<MDocument> UpdateAsync(MDocument document)
		{
			await _writeRepository.UpdateAsync(document);
			return document;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var document = await _readRepository.GetSingleAsync(d => d.Id == id);
			if (document == null) return false;

			await _writeRepository.RemoveAsync(document);
			return true;
		}
	}
}
