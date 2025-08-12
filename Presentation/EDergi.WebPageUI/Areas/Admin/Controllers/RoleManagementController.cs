using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SysAdmin")]
	public class RoleManagementController : Controller
	{
		private readonly IRoleService _roleService;

		public RoleManagementController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var roles = await _roleService.GetAllRolesWithUserCountAsync();
			return View(roles);
		}


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(AddRoleDto model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _roleService.AddRoleAsync(model);
			if (!result)
			{
				ModelState.AddModelError(string.Empty, "Rol oluşturulurken bir hata oluştu.");
				return View(model);
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var role = await _roleService.GetRoleByIdAsync(id);
			if (role == null)
				return NotFound();

			var model = new UpdateRoleDto
			{
				Id = role.Id,
				Name = role.Name
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateRoleDto model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _roleService.UpdateRoleAsync(model);
			if (!result)
			{
				ModelState.AddModelError(string.Empty, "Rol güncellenirken bir hata oluştu.");
				return View(model);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _roleService.DeleteRoleAsync(id);
			if (!result)
			{
				ModelState.AddModelError(string.Empty, "Rol silinirken bir hata oluştu.");
			}

			return RedirectToAction("Index");
		}
	}
}
