using EDergi.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Interfaces.Services
{
	public interface IDocumentService
	{
		Task<List<MDocument>> GetAllAsync();
		Task<MDocument> GetByIdAsync(Guid id);
		Task<List<MDocument>> GetByMagazineIdAsync(Guid magazineId);
		Task<MDocument> CreateAsync(MDocument document);
		Task<MDocument> UpdateAsync(MDocument document);
		Task<bool> DeleteAsync(Guid id);
	}
}
