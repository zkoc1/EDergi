using DergiAPI.Domain.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions
{
	public interface IAuthorService
	{
		Task<List<Author>> GetAllAsync();
		Task<Author> GetByIdAsync(long id);
		Task CreateAsync(Author author);
		Task UpdateAsync(Author author);
		Task DeleteAsync(long id);
	}
}
