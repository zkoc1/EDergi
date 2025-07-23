using DergiAPI.Domain.Entitites;

namespace EDergiAPI.Application.Abstractions
{
	public interface IArchiveService
	{
		Task<List<MNumberOf>> GetAllAsync();
		Task<MNumberOf> GetByIdAsync(long id);
		Task CreateAsync(MNumberOf archive);
		Task UpdateAsync(MNumberOf archive);
		Task DeleteAsync(long id);
	}
}

