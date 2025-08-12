using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using EDergi.WebPageUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPageUI.Areas.Admin.Models;

namespace EDergi.WebPageUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SysAdmin")]
	public class UserManagementController : Controller
	{
		private readonly IUserService _userService;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly UserManager<AppUser> _userManager;

		public UserManagementController(
			IUserService userService,
			RoleManager<AppRole> roleManager,
			UserManager<AppUser> userManager)
		{
			_userService = userService;
			_roleManager = roleManager;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var users = await _userService.GetAllUsersAsync();
			var userRolesViewModel = users.Select(u => new UserRolesViewModel
			{
				UserId = u.Id,
				Email = u.Email,
				FirstName = u.FirstName,
				LastName = u.LastName,
				Roles = u.Roles
			}).ToList();

			return View(userRolesViewModel);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var user = new AppUser
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName
			};

			var result = await _userManager.CreateAsync(user, "TempPassword123!");

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "User");
				return RedirectToAction("Index");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
				return NotFound();

			var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

			var model = new EditUserViewModel
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Roles = user.Roles,
				AllRoles = allRoles
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditUserViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var dto = new AppUserDto
			{
				Id = model.Id,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName
			};

			var result = await _userService.UpdateUserAsync(model.Id, dto);
			if (!result)
			{
				ModelState.AddModelError(string.Empty, "Kullanıcı güncellenirken bir hata oluştu.");
				return View(model);
			}

			await _userService.AssignRolesToUserAsync(model.Id, model.SelectedRoles ?? new List<string>());
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _userService.DeleteUserAsync(id);
			if (!result)
			{
				ModelState.AddModelError(string.Empty, "Kullanıcı silinirken bir hata oluştu.");
			}

			return RedirectToAction("Index");
		}
	}
}
