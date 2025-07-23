using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IMNumberOfService
	{
		Task<List<MNumberOf>> GetAllAsync();
		Task<MNumberOf> GetByIdAsync(Guid id);
		Task CreateAsync(MNumberOf mNumberOf);
		Task UpdateAsync(MNumberOf mNumberOf);
		Task DeleteAsync(Guid id);
	}
}
