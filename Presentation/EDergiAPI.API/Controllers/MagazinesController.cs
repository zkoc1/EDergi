using DergiAPI.Application.Abstractions;
using DergiAPI.Application.Interfaces.Services;
using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DergiAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MagazineController : ControllerBase
	{
		private readonly IMagazineService _magazineService;

		public MagazineController(IMagazineService magazineService)
		{
			_magazineService = magazineService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var magazines = await _magazineService.GetAllAsync();
			return Ok(magazines);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var magazine = await _magazineService.GetByIdAsync(id);
			if (magazine == null) return NotFound();
			return Ok(magazine);
		}

		[HttpPost]
		public async Task<IActionResult> CreateMagazine([FromBody] MagazineCreateDto dto)
		{
			var result = await _magazineService.CreateAsync(dto);
			if (result) return Ok("Magazine başarıyla oluşturuldu.");
			return BadRequest("Magazine oluşturulamadı.");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleted = await _magazineService.DeleteAsync(id);
			if (!deleted) return NotFound();
			return NoContent();
		}
	}
}
