using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IViewStatsService
	{
		Task<List<ViewStats>> GetAllAsync();
		Task<ViewStats> GetByIdAsync(Guid id);
		Task CreateAsync(ViewStats viewStats);
		Task UpdateAsync(ViewStats viewStats);
		Task DeleteAsync(Guid id);
	}
}
