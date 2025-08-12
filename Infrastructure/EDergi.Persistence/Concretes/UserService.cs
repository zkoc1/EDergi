using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.Persistence.Concretes
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;

		public UserService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<List<AppUserDto>> GetAllUsersAsync()
		{
			var users = await _userManager.Users.ToListAsync();
			var result = new List<AppUserDto>();

			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				result.Add(new AppUserDto
				{
					Id = user.Id,
					Email = user.Email,
					FirstName = user.FirstName,
					LastName = user.LastName,
					BirthDate = user.BirthDate,
					IsSysAdmin = user.IsSysAdmin,
					Roles = roles.ToList()
				});
			}

			return result;
		}

		public async Task<AppUserDto> GetUserByIdAsync(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null) return null;

			var roles = await _userManager.GetRolesAsync(user);

			return new AppUserDto
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				BirthDate = user.BirthDate,
				IsSysAdmin = user.IsSysAdmin,
				Roles = roles.ToList()
			};
		}

		public async Task<bool> UpdateUserAsync(Guid id, AppUserDto dto)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null) return false;

			user.FirstName = dto.FirstName;
			user.LastName = dto.LastName;
			user.BirthDate = dto.BirthDate;
			user.IsSysAdmin = dto.IsSysAdmin;

			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}

		public async Task<bool> DeleteUserAsync(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null) return false;

			var result = await _userManager.DeleteAsync(user);
			return result.Succeeded;
		}

		public async Task<bool> AssignRolesToUserAsync(Guid userId, List<string> roles)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			if (user == null) return false;

			var currentRoles = await _userManager.GetRolesAsync(user);
			await _userManager.RemoveFromRolesAsync(user, currentRoles);

			var result = await _userManager.AddToRolesAsync(user, roles);
			return result.Succeeded;
		}
	}
}
