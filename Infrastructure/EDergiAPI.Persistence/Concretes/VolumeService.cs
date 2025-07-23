using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes.Services
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

		public async Task<List<Volume>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<Volume> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<Volume> CreateAsync(Volume volume)
		{
			await _writeRepository.Addasync(volume);
			await _writeRepository.SaveAsync();
			return volume;
		}

		public async Task<Volume> UpdateAsync(Volume volume)
		{
			_writeRepository.Update(volume);
			await _writeRepository.SaveAsync();
			return volume;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var volume = await _readRepository.GetByIdAsync(id);
			if (volume == null) return false;

			await _writeRepository.RemoveAsync(volume);
			await _writeRepository.SaveAsync();
			return true;
		}
	}
}
