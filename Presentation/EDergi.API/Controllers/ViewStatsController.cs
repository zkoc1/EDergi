using EDergi.Application.Interfaces.Services;
using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EDergi.Application.DTOs; // DTO import edildi

namespace EDergi.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ViewStatsController : ControllerBase
	{
		private readonly IViewStatsService _viewStatsService;

		public ViewStatsController(IViewStatsService viewStatsService)
		{
			_viewStatsService = viewStatsService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var list = await _viewStatsService.GetAllAsync();
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var entity = await _viewStatsService.GetByIdAsync(id);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		[HttpGet("magazine/{magazineId}")]
		public async Task<IActionResult> GetByMagazineId(Guid magazineId)
		{
			var entity = await _viewStatsService.GetByMagazineIdAsync(magazineId);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		// 🆕 DTO kullanan Create metodu
		[HttpPost("{magazineId}")]
		public async Task<IActionResult> Create(Guid magazineId, [FromBody] ViewStatsDto viewStats)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var created = await _viewStatsService.CreateAsync(viewStats, magazineId);
			return Ok(created); // alternatifi: return CreatedAtAction(...)
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] ViewStats viewStats)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (id != viewStats.Id) return BadRequest("Id mismatch");

			var existing = await _viewStatsService.GetByIdAsync(id);
			if (existing == null) return NotFound();

			var updated = await _viewStatsService.UpdateAsync(viewStats);
			return Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleted = await _viewStatsService.DeleteAsync(id);
			if (!deleted) return NotFound();

			return NoContent();
		}
	}
}
