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
	public class VolumeApıController : ControllerBase
	{
		private readonly IVolumeService _volumeService;

		public VolumeApıController(IVolumeService volumeService)
		{
			_volumeService = volumeService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var list = await _volumeService.GetAllAsync();
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var volume = await _volumeService.GetByIdAsync(id);
			if (volume == null) return NotFound();
			return Ok(volume);
		}

		[HttpGet("magazine/{magazineId}")]
		public async Task<IActionResult> GetByMagazineId(Guid magazineId)
		{
			var list = await _volumeService.GetByMagazineIdAsync(magazineId);
			return Ok(list);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] VolumeCreateDto dto)
		{
			var result = await _volumeService.CreateAsync(dto);
			if (result!=null) return Ok("Volume başarıyla oluşturuldu.");
			return BadRequest("Volume oluşturulamadı.");
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Volume volume)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (id != volume.Id) return BadRequest("Id mismatch");

			var existing = await _volumeService.GetByIdAsync(id);
			if (existing == null) return NotFound();

			var updated = await _volumeService.UpdateAsync(volume);
			return Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleted = await _volumeService.DeleteAsync(id);
			if (!deleted) return NotFound();

			return NoContent();
		}
	}
}
