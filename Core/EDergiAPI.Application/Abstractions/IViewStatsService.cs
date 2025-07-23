using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions.Services
{
	public interface IViewStatsService
	{
		Task<List<ViewStats>> GetAllAsync();
		Task<ViewStats> GetByIdAsync(Guid id);
		Task<ViewStats> CreateAsync(ViewStats viewStats);
		Task<ViewStats> UpdateAsync(ViewStats viewStats);
		Task<bool> DeleteAsync(Guid id);
	}
}
