using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Interfaces.Services
{
	public interface IVolumeService
	{
		Task<List<Volume>> GetAllAsync();
		Task<Volume> GetByIdAsync(Guid id);
		Task<List<Volume>> GetByMagazineIdAsync(Guid magazineId);
		Task<Volume> CreateAsync(VolumeCreateDto dto);

		Task<Volume> UpdateAsync(Volume volume);
		Task<bool> DeleteAsync(Guid id);
	}
}
