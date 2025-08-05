using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IMagazineService
	{
		Task<List<Magazine>> GetAllAsync();
		Task<Magazine> GetByIdAsync(Guid id);
		Task<bool> CreateAsync(MagazineCreateDto dto);

		Task UpdateAsync(Magazine magazine);
		Task<bool> DeleteAsync(Guid id);
	

	}
}
