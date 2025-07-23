using DergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class ReadIndexService : IReadIndexService
	{
		private readonly IReadRepository<ReadIndex> _readRepository;
		private readonly IWriteRepository<ReadIndex> _writeRepository;

		public ReadIndexService(IReadRepository<ReadIndex> readRepository, IWriteRepository<ReadIndex> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public async Task<List<ReadIndex>> GetAllAsync()
		{
			return _readRepository.GetAll().ToList();
		}

		public async Task<ReadIndex> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(i => i.Id == id);
		}

		public async Task CreateAsync(ReadIndex index)
		{
			await _writeRepository.Addasync(index);
		}

		public async Task UpdateAsync(ReadIndex index)
		{
			await _writeRepository.UpdateAsync(index);
		}

		public async Task DeleteAsync(Guid id)
		{
			var index = await _readRepository.GetSingleAsync(i => i.Id == id);
			if (index != null)
			{
				await _writeRepository.RemoveAsync(index);
			}
		}

		public Task<ReadIndex> GetByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(long id)
		{
			throw new NotImplementedException();
		}
	}
}
