using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Interfaces.Services
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
