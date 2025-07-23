using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
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

		public Task<List<Magazine>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<Magazine> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(m => m.Id == id);
		}

		public async Task CreateAsync(Magazine magazine)
		{
			await _writeRepository.AddAsync(magazine);
		}

		public async Task UpdateAsync(Magazine magazine)
		{
			await _writeRepository.UpdateAsync(magazine);
		}

		public async Task DeleteAsync(Guid id)
		{
			var magazine = await _readRepository.GetSingleAsync(m => m.Id == id);
			if (magazine != null)
			{
				await _writeRepository.RemoveAsync(magazine);
			}
		}
	}
}
