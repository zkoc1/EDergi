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
	public class ReadIndexController : ControllerBase
	{
		private readonly IReadIndexService _readIndexService;

		public ReadIndexController(IReadIndexService readIndexService)
		{
			_readIndexService = readIndexService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var list = await _readIndexService.GetAllAsync();
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var entity = await _readIndexService.GetByIdAsync(id);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		[HttpGet("magazine/{magazineId}")]
		public async Task<IActionResult> GetByMagazineId(Guid magazineId)
		{
			var list = await _readIndexService.GetByMagazineIdAsync(magazineId);
			return Ok(list);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ReadIndexDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var readIndex = new ReadIndex
			{
				Name = dto.Name,
				ImageName = dto.ImageName,
				ImageUrl = dto.ImageUrl,
				// MagazineId controller üzerinden verilmediyse burada verilmesi gerekir
			};

			var created = await _readIndexService.CreateAsync(readIndex);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] ReadIndex readIndex)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (id != readIndex.Id) return BadRequest("Id mismatch");

			var existing = await _readIndexService.GetByIdAsync(id);
			if (existing == null) return NotFound();

			var updated = await _readIndexService.UpdateAsync(readIndex);
			return Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleted = await _readIndexService.DeleteAsync(id);
			if (!deleted) return NotFound();

			return NoContent();
		}
	}
}
