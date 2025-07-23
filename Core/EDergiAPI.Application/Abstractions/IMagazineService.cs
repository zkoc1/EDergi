using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IMagazineService
	{
		Task<List<Magazine>> GetAllAsync();
		Task<Magazine> GetByIdAsync(Guid id);
		Task CreateAsync(Magazine magazine);
		Task UpdateAsync(Magazine magazine);
		Task DeleteAsync(Guid id);
	}
}
