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
	public class ViewStatsController : ControllerBase
	{
		private readonly IViewStatsService _viewStatsService;

		public ViewStatsController(IViewStatsService viewStatsService)
		{
			_viewStatsService = viewStatsService;
		}

		[HttpGet]
		public async Task<ActionResult<List<ViewStats>>> GetAll()
		{
			var stats = await _viewStatsService.GetAllAsync();
			return Ok(stats);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ViewStats>> GetById(Guid id)
		{
			var stat = await _viewStatsService.GetByIdAsync(id);
			if (stat == null)
				return NotFound();

			return Ok(stat);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ViewStats viewStats)
		{
			await _viewStatsService.CreateAsync(viewStats);
			return CreatedAtAction(nameof(GetById), new { id = viewStats.Id }, viewStats);
		}

		// Yeni: Örnek ViewStats oluşturup kaydeden endpoint
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var sampleViewStats = new ViewStats
			{
				Id = Guid.NewGuid(),
				ViewCount = 1000,
				FavoriteCount = 150,
				DownloadCount = 500,
				CreatedDate = DateTime.UtcNow
			};

			await _viewStatsService.CreateAsync(sampleViewStats);
			return CreatedAtAction(nameof(GetById), new { id = sampleViewStats.Id }, sampleViewStats);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] ViewStats viewStats)
		{
			if (id != viewStats.Id)
				return BadRequest("ID uyuşmuyor.");

			await _viewStatsService.UpdateAsync(viewStats);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _viewStatsService.DeleteAsync(id);
			return NoContent();
		}
	}
}
