using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Interfaces.Services
{
	public interface IViewStatsService
	{
		Task<IEnumerable<ViewStats>> GetAllAsync();
		Task<ViewStats> GetByIdAsync(Guid id);
		Task<ViewStats> GetByMagazineIdAsync(Guid magazineId);
		Task<ViewStats> CreateAsync(ViewStatsDto dto, Guid magazineId);
		Task<ViewStats> UpdateAsync(ViewStats viewStats);
		Task<bool> DeleteAsync(Guid id);
	}
}
