using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions.Services
{
	public interface IVolumeService
	{
		Task<List<Volume>> GetAllAsync();
		Task<Volume> GetByIdAsync(Guid id);
		Task<Volume> CreateAsync(Volume volume);
		Task<Volume> UpdateAsync(Volume volume);
		Task<bool> DeleteAsync(Guid id);
	}
}
