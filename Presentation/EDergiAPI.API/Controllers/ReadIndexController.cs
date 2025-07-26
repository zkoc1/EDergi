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
	public class ReadIndexController : ControllerBase
	{
		private readonly IReadIndexService _readIndexService;

		public ReadIndexController(IReadIndexService readIndexService)
		{
			_readIndexService = readIndexService;
		}

		[HttpGet]
		public async Task<ActionResult<List<ReadIndex>>> GetAll()
		{
			var readIndices = await _readIndexService.GetAllAsync();
			return Ok(readIndices);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ReadIndex>> GetById(Guid id)
		{
			var readIndex = await _readIndexService.GetByIdAsync(id);
			if (readIndex == null)
				return NotFound();

			return Ok(readIndex);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ReadIndex readIndex)
		{
			await _readIndexService.CreateAsync(readIndex);
			return CreatedAtAction(nameof(GetById), new { id = readIndex.Id }, readIndex);
		}

		// Yeni: Örnek ReadIndex nesnesi oluşturan endpoint
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var sampleReadIndex = new ReadIndex
			{
				Id = Guid.NewGuid(),
				Name = "TR Dizin",
				ImageName = "trdizin.png",
				ImageUrl = "https://example.com/images/trdizin.png",
				CreatedDate = DateTime.UtcNow
			};

			await _readIndexService.CreateAsync(sampleReadIndex);
			return CreatedAtAction(nameof(GetById), new { id = sampleReadIndex.Id }, sampleReadIndex);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] ReadIndex readIndex)
		{
			if (id != readIndex.Id)
				return BadRequest("ID uyuşmuyor.");

			await _readIndexService.UpdateAsync(readIndex);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _readIndexService.DeleteAsync(id);
			return NoContent();
		}
	}
}
