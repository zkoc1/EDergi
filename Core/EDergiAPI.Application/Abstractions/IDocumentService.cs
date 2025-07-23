using DergiAPI.Domain.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions
{
	public interface IDocumentService
	{
		Task<List<MDocument>> GetAllAsync();
		Task<MDocument> GetByIdAsync(long id);
		Task CreateAsync(MDocument document);
		Task UpdateAsync(MDocument document);
		Task DeleteAsync(long id);
	}
}
