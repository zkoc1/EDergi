using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;

namespace EDergi.Application.Abstractions.Services
{
	public interface IArticleService
	{
		Task<List<Article>> GetAllAsync();
		Task<Article> GetByIdAsync(Guid id);
		Task CreateAsync(ArticleCreateDto dto);
		Task<List<ArticleListDto>> GetPendingAsync();

		Task UpdateAsync(Article article);
		Task DeleteAsync(Guid id);
		
		Task RejectAsync(Guid id);
	
	}
}
