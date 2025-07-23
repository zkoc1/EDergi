using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes.Services
{
	public class IssueService : IIssueService
	{
		private readonly IWriteRepository<Issue> _writeRepository;
		private readonly IReadRepository<Issue> _readRepository;

		public IssueService(IWriteRepository<Issue> writeRepository, IReadRepository<Issue> readRepository)
		{
			_writeRepository = writeRepository;
			_readRepository = readRepository;
		}

		public async Task<Issue> CreateAsync(Issue issue)
		{
			await _writeRepository.AddAsync(issue);
			await _writeRepository.SaveAsync();
			return issue;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var issue = await _readRepository.GetByIdAsync(id);
			if (issue == null) return false;

			_writeRepository.Remove(issue);
			await _writeRepository.SaveAsync();
			return true;
		}

		public async Task<List<Issue>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<Issue> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<Issue> UpdateAsync(Issue issue)
		{
			_writeRepository.Update(issue);
			await _writeRepository.SaveAsync();
			return issue;
		}
	}
}
