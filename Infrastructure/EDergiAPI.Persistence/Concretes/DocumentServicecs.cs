using DergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
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

		public async Task<List<MDocument>> GetAllAsync()
		{
			return _readRepository.GetAll().ToList();
		}

		public async Task<MDocument> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(d => d.Id == id);
		}

		public async Task CreateAsync(MDocument document)
		{
			await _writeRepository.Addasync(document);
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

		public Task<MDocument> GetByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(long id)
		{
			throw new NotImplementedException();
		}
	}
}
