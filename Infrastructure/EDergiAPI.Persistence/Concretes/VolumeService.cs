using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class VolumeService : IVolumeService
	{
		private readonly IReadRepository<Volume> _readRepository;
		private readonly IWriteRepository<Volume> _writeRepository;

		public VolumeService(IReadRepository<Volume> readRepository, IWriteRepository<Volume> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<Volume>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<Volume> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(v => v.Id == id);
		}

		public async Task CreateAsync(Volume volume)
		{
			await _writeRepository.AddAsync(volume);
		}

		public async Task UpdateAsync(Volume volume)
		{
			await _writeRepository.UpdateAsync(volume);
		}

		public async Task DeleteAsync(Guid id)
		{
			var volume = await _readRepository.GetSingleAsync(v => v.Id == id);
			if (volume != null)
			{
				await _writeRepository.RemoveAsync(volume);
			}
		}
	}
}
