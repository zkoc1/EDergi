using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IMDocumentService
	{
		Task<List<MDocument>> GetAllAsync();
		Task<MDocument> GetByIdAsync(Guid id);
		Task CreateAsync(MDocument document);
		Task UpdateAsync(MDocument document);
		Task DeleteAsync(Guid id);
	}
}
