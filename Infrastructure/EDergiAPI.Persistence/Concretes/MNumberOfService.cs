using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class MNumberOfService : IMNumberOfService
	{
		private readonly IReadRepository<MNumberOf> _readRepository;
		private readonly IWriteRepository<MNumberOf> _writeRepository;

		public MNumberOfService(IReadRepository<MNumberOf> readRepository, IWriteRepository<MNumberOf> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<MNumberOf>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<MNumberOf> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(m => m.Id == id);
		}

		public async Task CreateAsync(MNumberOf mNumberOf)
		{
			await _writeRepository.AddAsync(mNumberOf);
		}

		public async Task UpdateAsync(MNumberOf mNumberOf)
		{
			await _writeRepository.UpdateAsync(mNumberOf);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _readRepository.GetSingleAsync(m => m.Id == id);
			if (entity != null)
			{
				await _writeRepository.RemoveAsync(entity);
			}
		}
	}
}
