using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class IssueService : IIssueService
	{
		private readonly IReadRepository<Issue> _readRepository;
		private readonly IWriteRepository<Issue> _writeRepository;

		public IssueService(IReadRepository<Issue> readRepository, IWriteRepository<Issue> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<Issue>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<Issue> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(i => i.Id == id);
		}

		public async Task CreateAsync(Issue issue)
		{
			await _writeRepository.AddAsync(issue);
		}

		public async Task UpdateAsync(Issue issue)
		{
			await _writeRepository.UpdateAsync(issue);
		}

		public async Task DeleteAsync(Guid id)
		{
			var issue = await _readRepository.GetSingleAsync(i => i.Id == id);
			if (issue != null)
			{
				await _writeRepository.RemoveAsync(issue);
			}
		}
	}
}
