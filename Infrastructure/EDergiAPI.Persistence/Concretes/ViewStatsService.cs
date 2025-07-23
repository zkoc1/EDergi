using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
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

		public Task<List<ViewStats>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<ViewStats> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(v => v.Id == id);
		}

		public async Task CreateAsync(ViewStats viewStats)
		{
			await _writeRepository.AddAsync(viewStats);
		}

		public async Task UpdateAsync(ViewStats viewStats)
		{
			await _writeRepository.UpdateAsync(viewStats);
		}

		public async Task DeleteAsync(Guid id)
		{
			var viewStats = await _readRepository.GetSingleAsync(v => v.Id == id);
			if (viewStats != null)
			{
				await _writeRepository.RemoveAsync(viewStats);
			}
		}
	}
}
