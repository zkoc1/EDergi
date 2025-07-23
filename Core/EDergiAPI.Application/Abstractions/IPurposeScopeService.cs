using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions.Services
{
	public interface IPurposeScopeService
	{
		Task<List<PurposeScope>> GetAllAsync();
		Task<PurposeScope> GetByIdAsync(Guid id);
		Task<PurposeScope> CreateAsync(PurposeScope purposeScope);
		Task<PurposeScope> UpdateAsync(PurposeScope purposeScope);
		Task<bool> DeleteAsync(Guid id);
	}
}
