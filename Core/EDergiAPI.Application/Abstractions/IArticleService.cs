using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IArticleService
	{
		Task<List<Article>> GetAllAsync();
		Task<Article> GetByIdAsync(Guid id);
		Task CreateAsync(Article article);
		Task UpdateAsync(Article article);
		Task DeleteAsync(Guid id);
	}
}
