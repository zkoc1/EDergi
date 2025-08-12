using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IUserService
	{
		Task<List<AppUserDto>> GetAllUsersAsync();
		Task<AppUserDto> GetUserByIdAsync(Guid id);
		Task<bool> UpdateUserAsync(Guid id, AppUserDto dto);
		Task<bool> DeleteUserAsync(Guid id);
		Task<bool> AssignRolesToUserAsync(Guid userId, List<string> roles); // Kullanıcıya roller atama
	}

}
