// IIssueService.cs
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IIssueService
	{
		Task<List<Issue>> GetAllAsync();
		Task<Issue> GetByIdAsync(Guid id);
		Task CreateAsync(IssueCreateDto dto);
		Task UpdateAsync(Issue issue);
		Task DeleteAsync(Guid id);
	}
}
