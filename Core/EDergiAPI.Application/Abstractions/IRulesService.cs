using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions.Services
{
	public interface IRulesService
	{
		Task<List<Rules>> GetAllAsync();
		Task<Rules> GetByIdAsync(Guid id);
		Task<Rules> CreateAsync(Rules rules);
		Task<Rules> UpdateAsync(Rules rules);
		Task<bool> DeleteAsync(Guid id);
	}
}

