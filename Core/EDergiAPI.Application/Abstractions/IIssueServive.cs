// IIssueService.cs
using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions
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
