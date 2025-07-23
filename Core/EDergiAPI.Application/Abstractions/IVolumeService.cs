using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IVolumeService
	{
		Task<List<Volume>> GetAllAsync();
		Task<Volume> GetByIdAsync(Guid id);
		Task CreateAsync(Volume volume);
		Task UpdateAsync(Volume volume);
		Task DeleteAsync(Guid id);
	}
}
