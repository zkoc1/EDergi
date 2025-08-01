using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions
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
