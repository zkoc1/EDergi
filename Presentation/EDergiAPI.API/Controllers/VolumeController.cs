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
	public class VolumeController : ControllerBase
	{
		private readonly IVolumeService _volumeService;

		public VolumeController(IVolumeService volumeService)
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
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var created = await _volumeService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
