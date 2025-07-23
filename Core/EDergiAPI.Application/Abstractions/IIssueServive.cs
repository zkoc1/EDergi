using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IIssueService
	{
		Task<List<Issue>> GetAllAsync();
		Task<Issue> GetByIdAsync(Guid id);
		Task CreateAsync(Issue issue);
		Task UpdateAsync(Issue issue);
		Task DeleteAsync(Guid id);
	}
}
