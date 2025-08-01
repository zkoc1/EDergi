using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.DTOs;

namespace DergiAPI.Application.Abstractions.Services
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
