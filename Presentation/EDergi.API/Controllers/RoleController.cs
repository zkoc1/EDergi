using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Application.DTOs.EDergi.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EDergi.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "Admin,SysAdmin")]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var roles = await _roleService.GetAllRolesAsync();
			return Ok(new
			{
				success = true,
				data = roles
			});
		}

		[HttpPost("assign")]
		public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleDto request)
		{
			if (request == null || request.UserId == Guid.Empty || string.IsNullOrWhiteSpace(request.RoleName))
				return BadRequest(new { success = false, message = "Geçersiz kullanıcı veya rol adı." });

			var result = await _roleService.AssignRoleToUserAsync(request.UserId, request.RoleName);

			if (!result)
				return BadRequest(new { success = false, message = "Rol ataması başarısız." });

			return Ok(new { success = true, message = "Rol başarıyla atandı." });
		}
	}
}
