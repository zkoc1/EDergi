using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;

namespace DergiAPI.Persistence.Concretes
{
	public class ArchiveService : IArchiveService
	{
		private readonly IRepository<MNumberOf> _repository;

		public ArchiveService(IRepository<MNumberOf> repository)
		{
			_repository = repository;
		}

		public async Task<List<MNumberOf>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<MNumberOf> GetByIdAsync(long id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task CreateAsync(MNumberOf archive)
		{
			await _repository.AddAsync(archive);
		}

		public async Task UpdateAsync(MNumberOf archive)
		{
			await _repository.UpdateAsync(archive);
		}

		public async Task DeleteAsync(long id)
		{
			await _repository.DeleteAsync(id);
		}
	}
}

