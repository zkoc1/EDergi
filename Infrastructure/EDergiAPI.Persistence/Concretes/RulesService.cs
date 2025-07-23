using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes.Services
{
	public class RulesService : IRulesService
	{
		private readonly IReadRepository<Rules> _readRepository;
		private readonly IWriteRepository<Rules> _writeRepository;

		public RulesService(IReadRepository<Rules> readRepository, IWriteRepository<Rules> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public async Task<List<Rules>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<Rules> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<Rules> CreateAsync(Rules rules)
		{
			await _writeRepository.Addasync(rules);
			await _writeRepository.SaveAsync();
			return rules;
		}

		public async Task<Rules> UpdateAsync(Rules rules)
		{
			_writeRepository.Update(rules);
			await _writeRepository.SaveAsync();
			return rules;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var rules = await _readRepository.GetByIdAsync(id);
			if (rules == null) return false;

			await _writeRepository.RemoveAsync(rules);
			await _writeRepository.SaveAsync();
			return true;
		}
	}
}
