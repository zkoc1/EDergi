using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IArticleIssueService
	{
		Task<List<ArticleIssue>> GetAllAsync();
		Task<ArticleIssue> GetByIdAsync(Guid id);
		Task CreateAsync(ArticleIssue articleIssue);
		Task UpdateAsync(ArticleIssue articleIssue);
		Task DeleteAsync(Guid id);
	}
}

