using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RoleService : IRoleService
{
	private readonly RoleManager<AppRole> _roleManager;
	private readonly UserManager<AppUser> _userManager;

	public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
	{
		_roleManager = roleManager;
		_userManager = userManager;
	}

	public async Task<IEnumerable<AppRoleDto>> GetAllRolesAsync()
	{
		var roles = await _roleManager.Roles.ToListAsync();
		return roles.Select(r => new AppRoleDto
		{
			Id = r.Id,
			Name = r.Name
		});
	}

	public async Task<AppRoleDetailDto> GetRoleByIdAsync(Guid roleId)
	{
		var role = await _roleManager.FindByIdAsync(roleId.ToString());
		if (role == null) return null;

		var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
		return new AppRoleDetailDto
		{
			Id = role.Id,
			Name = role.Name,
			UserCount = usersInRole.Count
		};
	}

	public async Task<bool> AddRoleAsync(AddRoleDto model)
	{
		if (string.IsNullOrWhiteSpace(model.Name))
			return false;

		if (await _roleManager.RoleExistsAsync(model.Name))
			return false;

		var role = new AppRole { Name = model.Name };
		var result = await _roleManager.CreateAsync(role);
		return result.Succeeded;
	}

	public async Task<bool> UpdateRoleAsync(UpdateRoleDto model)
	{
		var role = await _roleManager.FindByIdAsync(model.Id.ToString());
		if (role == null) return false;

		role.Name = model.Name;
		var result = await _roleManager.UpdateAsync(role);
		return result.Succeeded;
	}

	public async Task<bool> DeleteRoleAsync(Guid roleId)
	{
		var role = await _roleManager.FindByIdAsync(roleId.ToString());
		if (role == null) return false;

		var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
		if (usersInRole.Any()) return false;

		var result = await _roleManager.DeleteAsync(role);
		return result.Succeeded;
	}

	public async Task<int> GetUserCountInRoleAsync(Guid roleId)
	{
		var role = await _roleManager.FindByIdAsync(roleId.ToString());
		if (role == null) return 0;

		var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
		return usersInRole.Count;
	}

	public async Task<bool> AssignRoleToUserAsync(Guid userId, string roleName)
	{
		var user = await _userManager.FindByIdAsync(userId.ToString());
		if (user == null) return false;

		if (!await _roleManager.RoleExistsAsync(roleName))
			return false;

		var result = await _userManager.AddToRoleAsync(user, roleName);
		return result.Succeeded;
	}
	public async Task<List<AppRoleDetailDto>> GetAllRolesWithUserCountAsync()
	{
		var roles = await _roleManager.Roles.ToListAsync();
		var roleDetails = new List<AppRoleDetailDto>();

		foreach (var role in roles)
		{
			var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
			var roleDetail = new AppRoleDetailDto
			{
				Id = role.Id,
				Name = role.Name,
				UserCount = usersInRole.Count
			};
			roleDetails.Add(roleDetail);
		}

		return roleDetails;
	}

	
}
