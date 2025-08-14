using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;

namespace EDergi.Application.Abstractions.Services
{
	public interface IArticleService
	{
		Task<List<Article>> GetAllAsync();
		Task<Article> GetByIdAsync(Guid id);
		Task CreateAsync(ArticleCreateDto dto);
		Task<List<Article>> GetByIssueIdAsync(Guid ıssueId);
		Task UpdateAsync(Article article);
		Task DeleteAsync(Guid id);
		//Task<List<ArticleListDto>> GetByIssueIdAsync(Guid issueId);
		Task<List<ArticleListDto>> GetPendingAsync();
		Task RejectAsync(Guid id);
	
	}
}
