using EDergi.Application.Interfaces.Services;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Persistence.Concretes
{
	public class ViewStatsService : IViewStatsService
	{
		private readonly IReadRepository<ViewStats> _readRepository;
		private readonly IWriteRepository<ViewStats> _writeRepository;
		private readonly IReadRepository<Magazine> _magazineReadRepository;

		public ViewStatsService(
			IReadRepository<ViewStats> readRepository,
			IWriteRepository<ViewStats> writeRepository,
			IReadRepository<Magazine> magazineReadRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
			_magazineReadRepository = magazineReadRepository;
		}

		public async Task<IEnumerable<ViewStats>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<ViewStats> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<ViewStats> GetByMagazineIdAsync(Guid magazineId)
		{
			return await _readRepository.GetSingleAsync(v => v.MagazineId == magazineId);
		}

		public async Task<ViewStats> CreateAsync(ViewStatsDto dto, Guid magazineId)
		{
			var magazine = await _magazineReadRepository.GetByIdAsync(magazineId);
			if (magazine == null) throw new Exception("Magazine not found");

			var newStat = new ViewStats
			{
				Id = Guid.NewGuid(),
				MagazineId = magazineId,
				ViewCount = dto.ViewCount,
				FavoriteCount = dto.FavoriteCount,
				DownloadCount = dto.DownloadCount
			};

			await _writeRepository.AddAsync(newStat);
			await _writeRepository.SaveAsync();

			return newStat;
		}

		public async Task<ViewStats> UpdateAsync(ViewStats viewStats)
		{
			await _writeRepository.UpdateAsync(viewStats);
			await _writeRepository.SaveAsync();
			return viewStats;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var stat = await _readRepository.GetByIdAsync(id);
			if (stat == null) return false;

			await _writeRepository.RemoveAsync(stat);
			await _writeRepository.SaveAsync();
			return true;
		}
	}
}
