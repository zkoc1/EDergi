using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EDergi.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "Admin")]
	public class MenuController : ControllerBase
	{
		private readonly IMenuService _menuService;

		public MenuController(IMenuService menuService)
		{
			_menuService = menuService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var menus = await _menuService.GetAllMenusAsync();
			return Ok(menus);
		}

		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] MenuDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var success = await _menuService.AddMenuAsync(dto);
			if (!success)
				return BadRequest("Menü eklenemedi.");

			return Ok("Menü başarıyla eklendi.");
		}
	}
}
