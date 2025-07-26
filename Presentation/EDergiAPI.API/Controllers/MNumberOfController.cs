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
	public class MNumberOfController : ControllerBase
	{
		private readonly IMNumberOfService _mNumberOfService;

		public MNumberOfController(IMNumberOfService mNumberOfService)
		{
			_mNumberOfService = mNumberOfService;
		}

		[HttpGet]
		public async Task<ActionResult<List<MNumberOf>>> GetAll()
		{
			var list = await _mNumberOfService.GetAllAsync();
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<MNumberOf>> GetById(Guid id)
		{
			var item = await _mNumberOfService.GetByIdAsync(id);
			if (item == null)
				return NotFound();

			return Ok(item);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] MNumberOf model)
		{
			await _mNumberOfService.CreateAsync(model);
			return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
		}

		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			// Örnek Volume nesnesi (eğer Volume sınıfı karmaşıksa, basit örnek)
			var sampleVolume = new Volume
			{
			
				Id = Guid.NewGuid(),
				CreatedDate = DateTime.UtcNow
			};

			var sample = new MNumberOf
			{
				Id = Guid.NewGuid(),
				Year = 2025,
				NumberOf = 5,
				Volumes = sampleVolume,
				CreatedDate = DateTime.UtcNow
			};

			await _mNumberOfService.CreateAsync(sample);
			return CreatedAtAction(nameof(GetById), new { id = sample.Id }, sample);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] MNumberOf model)
		{
			if (id != model.Id)
				return BadRequest("ID uyuşmuyor.");

			await _mNumberOfService.UpdateAsync(model);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _mNumberOfService.DeleteAsync(id);
			return NoContent();
		}
	}
}
