using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes.Services
{
	public class ViewStatsService : IViewStatsService
	{
		private readonly IReadRepository<ViewStats> _readRepository;
		private readonly IWriteRepository<ViewStats> _writeRepository;

		public ViewStatsService(IReadRepository<ViewStats> readRepository, IWriteRepository<ViewStats> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public async Task<List<ViewStats>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<ViewStats> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<ViewStats> CreateAsync(ViewStats viewStats)
		{
			await _writeRepository.Addasync(viewStats);
			await _writeRepository.SaveAsync();
			return viewStats;
		}

		public async Task<ViewStats> UpdateAsync(ViewStats viewStats)
		{
			_writeRepository.Update(viewStats);
			await _writeRepository.SaveAsync();
			return viewStats;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var viewStats = await _readRepository.GetByIdAsync(id);
			if (viewStats == null) return false;

			await _writeRepository.RemoveAsync(viewStats);
			await _writeRepository.SaveAsync();
			return true;
		}
	}
}
