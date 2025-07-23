using DergiAPI.Domain.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions
{
	public interface IReadIndexService
	{
		Task<List<ReadIndex>> GetAllAsync();
		Task<ReadIndex> GetByIdAsync(long id);
		Task CreateAsync(ReadIndex index);
		Task UpdateAsync(ReadIndex index);
		Task DeleteAsync(long id);
	}
}
