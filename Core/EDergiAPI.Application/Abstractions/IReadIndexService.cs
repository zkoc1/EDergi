using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IReadIndexService
	{
		Task<List<ReadIndex>> GetAllAsync();
		Task<ReadIndex> GetByIdAsync(Guid id);
		Task CreateAsync(ReadIndex readIndex);
		Task UpdateAsync(ReadIndex readIndex);
		Task DeleteAsync(Guid id);
	}
}
