using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes.Services
{
	public class WritingRulesService : IWritingRulesService
	{
		private readonly IReadRepository<WritingRules> _readRepository;
		private readonly IWriteRepository<WritingRules> _writeRepository;

		public WritingRulesService(IReadRepository<WritingRules> readRepository, IWriteRepository<WritingRules> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public async Task<List<WritingRules>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<WritingRules> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<WritingRules> CreateAsync(WritingRules writingRules)
		{
			await _writeRepository.Addasync(writingRules);
			await _writeRepository.SaveAsync();
			return writingRules;
		}

		public async Task<WritingRules> UpdateAsync(WritingRules writingRules)
		{
			_writeRepository.Update(writingRules);
			await _writeRepository.SaveAsync();
			return writingRules;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var writingRules = await _readRepository.GetByIdAsync(id);
			if (writingRules == null) return false;

			await _writeRepository.RemoveAsync(writingRules);
			await _writeRepository.SaveAsync();
			return true;
		}
	}
}
