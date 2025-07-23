using DergiAPI.Domain.Entitites;

namespace DergiAPI.Application.Abstractions
{
	public interface IArticleIssueService
	{
		Task<List<ArticleIssue>> GetAllAsync();
		Task<ArticleIssue?> GetByIdAsync(Guid id);
		Task AddAsync(ArticleIssue articleIssue);
		Task UpdateAsync(ArticleIssue articleIssue);
		Task DeleteAsync(Guid id);
	}
}
