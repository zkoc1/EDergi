using DergiAPI.Domain.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IAdminService
	{
		Task<List<Admin>> GetAllAsync();
		Task<Admin> GetByIdAsync(Guid id);
		Task CreateAsync(Admin admin);
		Task UpdateAsync(Admin admin);
		Task DeleteAsync(Guid id);
	}
}
