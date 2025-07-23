using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
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

		public Task<List<ReadIndex>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<ReadIndex> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(r => r.Id == id);
		}

		public async Task CreateAsync(ReadIndex readIndex)
		{
			await _writeRepository.AddAsync(readIndex);
		}

		public async Task UpdateAsync(ReadIndex readIndex)
		{
			await _writeRepository.UpdateAsync(readIndex);
		}

		public async Task DeleteAsync(Guid id)
		{
			var readIndex = await _readRepository.GetSingleAsync(r => r.Id == id);
			if (readIndex != null)
			{
				await _writeRepository.RemoveAsync(readIndex);
			}
		}
	}
}
