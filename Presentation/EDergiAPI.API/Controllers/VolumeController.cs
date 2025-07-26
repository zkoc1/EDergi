using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VolumesController : ControllerBase
	{
		private readonly IVolumeService _volumeService;

		public VolumesController(IVolumeService volumeService)
		{
			_volumeService = volumeService;
		}

		// GET: api/Volumes
		[HttpGet]
		public async Task<ActionResult<List<Volume>>> GetAll()
		{
			var volumes = await _volumeService.GetAllAsync();
			return Ok(volumes);
		}

		// GET: api/Volumes/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Volume>> GetById(Guid id)
		{
			var volume = await _volumeService.GetByIdAsync(id);
			if (volume == null)
				return NotFound();

			return Ok(volume);
		}

		// POST: api/Volumes
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Volume volume)
		{
			if (volume == null)
				return BadRequest("Veri eksik.");

			await _volumeService.CreateAsync(volume);
			return CreatedAtAction(nameof(GetById), new { id = volume.Id }, volume);
		}

		// POST: api/Volumes/create-sample
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var newVolume = new Volume
			{
				Title = "Cilt 1 - Bilgi Teknolojileri",
				ArticleId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Gerçek ArticleId ile değiştir
				Issues = new List<Issue>() // Boş başlatılabilir
			};

			await _volumeService.CreateAsync(newVolume);
			return CreatedAtAction(nameof(GetById), new { id = newVolume.Id }, newVolume);
		}

		// PUT: api/Volumes/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Volume volume)
		{
			if (volume == null || id != volume.Id)
				return BadRequest("ID uyuşmuyor ya da veri eksik.");

			await _volumeService.UpdateAsync(volume);
			return NoContent();
		}

		// DELETE: api/Volumes/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _volumeService.DeleteAsync(id);
			return NoContent();
		}
	}
}
