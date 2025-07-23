using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes.Services
{
	public class PurposeScopeService : IPurposeScopeService
	{
		private readonly IReadRepository<PurposeScope> _readRepository;
		private readonly IWriteRepository<PurposeScope> _writeRepository;

		public PurposeScopeService(IReadRepository<PurposeScope> readRepository, IWriteRepository<PurposeScope> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public async Task<List<PurposeScope>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<PurposeScope> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<PurposeScope> CreateAsync(PurposeScope purposeScope)
		{
			await _writeRepository.Addasync(purposeScope);
			await _writeRepository.SaveAsync();
			return purposeScope;
		}

		public async Task<PurposeScope> UpdateAsync(PurposeScope purposeScope)
		{
			_writeRepository.Update(purposeScope);
			await _writeRepository.SaveAsync();
			return purposeScope;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var purposeScope = await _readRepository.GetByIdAsync(id);
			if (purposeScope == null) return false;

			await _writeRepository.RemoveAsync(purposeScope);
			await _writeRepository.SaveAsync();
			return true;
		}
	}
}
