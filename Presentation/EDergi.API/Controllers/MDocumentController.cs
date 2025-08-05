using EDergi.Application.Interfaces.Services;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MDocumentController : ControllerBase
	{
		private readonly IDocumentService _mDocumentService;

		public MDocumentController(IDocumentService mDocumentService)
		{
			_mDocumentService = mDocumentService;
		}

		[HttpGet]
		public async Task<ActionResult<List<MDocument>>> GetAll()
		{
			var documents = await _mDocumentService.GetAllAsync();
			return Ok(documents);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<MDocument>> GetById(Guid id)
		{
			var document = await _mDocumentService.GetByIdAsync(id);
			if (document == null)
				return NotFound();

			return Ok(document);
		}

		
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] MDocument document)
		{
			if (id != document.Id)
				return BadRequest("ID uyuşmuyor.");

			await _mDocumentService.UpdateAsync(document);
			return NoContent();
		}
		[HttpPost]
		public async Task<IActionResult> CreateFromEntity([FromBody] MDocumentCreateDto dto)
		{
			var document = new MDocument
			{
				Id = Guid.NewGuid(),
				MagazineId = dto.MagazineId,
				FileName = dto.FileName,
				FilePath = dto.FilePath,
				CreatedDate = dto.CreatedDate ?? DateTime.UtcNow
			};

			await _mDocumentService.CreateAsync(document);
			return CreatedAtAction(nameof(GetById), new { id = document.Id }, document);
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _mDocumentService.DeleteAsync(id);
			return NoContent();
		}
	}
}
