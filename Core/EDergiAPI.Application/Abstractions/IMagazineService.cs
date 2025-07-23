using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions.Services
{
	public interface IMagazineService
	{
		Task<List<Magazine>> GetAllAsync();
		Task<Magazine> GetByIdAsync(Guid id);
		Task<Magazine> CreateAsync(Magazine magazine);
		Task<Magazine> UpdateAsync(Magazine magazine);
		Task<bool> DeleteAsync(Guid id);
	}
}
