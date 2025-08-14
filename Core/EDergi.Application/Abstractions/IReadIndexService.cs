using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Interfaces.Services
{
	public interface IReadIndexService
	{
		Task<List<ReadIndex>> GetAllAsync();
		Task<ReadIndex> GetByIdAsync(Guid id);
		Task<List<ReadIndex>> GetByMagazineIdAsync(Guid magazineId);
		
		Task<ReadIndex> CreateAsync(ReadIndex readIndex);
		Task<ReadIndex> UpdateAsync(ReadIndex readIndex);
		Task<bool> DeleteAsync(Guid id);
	}
}
