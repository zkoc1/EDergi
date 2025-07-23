using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions.Services
{
	public interface IIssueService
	{
		Task<Issue> GetByIdAsync(Guid id);
		Task<List<Issue>> GetAllAsync();
		Task<Issue> CreateAsync(Issue issue);
		Task<Issue> UpdateAsync(Issue issue);
		Task<bool> DeleteAsync(Guid id);
	}
}
