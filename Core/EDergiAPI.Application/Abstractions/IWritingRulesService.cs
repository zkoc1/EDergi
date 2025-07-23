using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions.Services
{
	public interface IWritingRulesService
	{
		Task<List<WritingRules>> GetAllAsync();
		Task<WritingRules> GetByIdAsync(Guid id);
		Task<WritingRules> CreateAsync(WritingRules writingRules);
		Task<WritingRules> UpdateAsync(WritingRules writingRules);
		Task<bool> DeleteAsync(Guid id);
	}
}
