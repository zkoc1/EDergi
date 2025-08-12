using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IRoleService
	{
		Task<IEnumerable<AppRoleDto>> GetAllRolesAsync();
		Task<AppRoleDetailDto> GetRoleByIdAsync(Guid roleId);
		Task<bool> AddRoleAsync(AddRoleDto model);
		Task<bool> UpdateRoleAsync(UpdateRoleDto model);
		Task<bool> DeleteRoleAsync(Guid roleId);
		Task<int> GetUserCountInRoleAsync(Guid roleId);
		Task<bool> AssignRoleToUserAsync(Guid userId, string roleName);
		Task<List<AppRoleDetailDto>> GetAllRolesWithUserCountAsync();
	}
}
