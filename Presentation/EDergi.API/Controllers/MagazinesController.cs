using EDergi.Application.Abstractions;
using EDergi.Application.Interfaces.Services;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EDergi.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MagazinesController : ControllerBase
	{
		private readonly IMagazineService _magazineService;
		


		public MagazinesController(IMagazineService magazineService)
		{
			_magazineService = magazineService;
			

		}
		[HttpPost]
		public async Task<IActionResult> CreateMagazine([FromBody] MagazineCreateDto dto)
		{

			var result = await _magazineService.CreateAsync(dto);
			if (result) return Ok("Magazine başarıyla oluşturuldu.");
			return BadRequest("Magazine oluşturulamadı.");
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



		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _magazineService.DeleteAsync(id);
			return Ok();
		}
	}
}
