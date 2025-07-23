using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes.Services
{
	public class MagazineService : IMagazineService
	{
		private readonly IReadRepository<Magazine> _readRepository;
		private readonly IWriteRepository<Magazine> _writeRepository;

		public MagazineService(IReadRepository<Magazine> readRepository, IWriteRepository<Magazine> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public async Task<List<Magazine>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<Magazine> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<Magazine> CreateAsync(Magazine magazine)
		{
			await _writeRepository.Addasync(magazine);
			await _writeRepository.SaveAsync();
			return magazine;
		}

		public async Task<Magazine> UpdateAsync(Magazine magazine)
		{
			_writeRepository.Update(magazine);
			await _writeRepository.SaveAsync();
			return magazine;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var magazine = await _readRepository.GetByIdAsync(id);
			if (magazine == null) return false;

			await _writeRepository.RemoveAsync(magazine);
			await _writeRepository.SaveAsync();
			return true;
		}
	}
}
