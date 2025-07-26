using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;

		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Admin>>> GetAll()
		{
			var admins = await _adminService.GetAllAsync();
			return Ok(admins);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Admin>> GetById(Guid id)
		{
			var admin = await _adminService.GetByIdAsync(id);
			if (admin == null)
				return NotFound();

			return Ok(admin);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Admin admin)
		{
			if (admin == null)
				return BadRequest();

			await _adminService.CreateAsync(admin);
			return CreatedAtAction(nameof(GetById), new { id = admin.Id }, admin);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Admin admin)
		{
			if (admin == null || id != admin.Id)
				return BadRequest();

			await _adminService.UpdateAsync(admin);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _adminService.DeleteAsync(id);
			return NoContent();
		}
	}
}
