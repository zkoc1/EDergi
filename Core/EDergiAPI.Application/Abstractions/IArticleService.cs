using DergiAPI.Domain.Entitites;

namespace EDergiAPI.Application.Abstractions
{
	public interface IArticleService
	{
		Task<List<Article>> GetAllAsync();
		Task<Article> GetByIdAsync(long id);
		Task CreateAsync(Article article);
		Task UpdateAsync(Article article);
		Task DeleteAsync(long id);
	}
}
