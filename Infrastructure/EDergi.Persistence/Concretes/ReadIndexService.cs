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
	public class ReadIndexService : IReadIndexService
	{
		private readonly IReadRepository<ReadIndex> _readRepository;
		private readonly IWriteRepository<ReadIndex> _writeRepository;

		public ReadIndexService(IReadRepository<ReadIndex> readRepository, IWriteRepository<ReadIndex> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<ReadIndex>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<ReadIndex> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(r => r.Id == id);
		}

		public Task<List<ReadIndex>> GetByMagazineIdAsync(Guid magazineId)
		{
			var list = _readRepository.GetWhere(r => r.MagazineId == magazineId).ToList();
			return Task.FromResult(list);
		}

		public async Task<ReadIndex> CreateAsync(ReadIndex readIndex)
		{
			await _writeRepository.AddAsync(readIndex);
			return readIndex;
		}

		public async Task<ReadIndex> UpdateAsync(ReadIndex readIndex)
		{
			await _writeRepository.UpdateAsync(readIndex);
			return readIndex;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var entity = await _readRepository.GetSingleAsync(r => r.Id == id);
			if (entity == null) return false;

			await _writeRepository.RemoveAsync(entity);
			return true;
		}
	}
}
