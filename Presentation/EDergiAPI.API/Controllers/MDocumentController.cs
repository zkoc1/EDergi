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
	public class MDocumentController : ControllerBase
	{
		private readonly IMDocumentService _mDocumentService;

		public MDocumentController(IMDocumentService mDocumentService)
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

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] MDocument document)
		{
			await _mDocumentService.CreateAsync(document);
			return CreatedAtAction(nameof(GetById), new { id = document.Id }, document);
		}

		// Doğru şekilde örnek nesne oluşturan endpoint
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var sampleDocument = new MDocument
			{
				
				Id = Guid.NewGuid(),
				CreatedDate = DateTime.UtcNow,

				FileName = "example.pdf",
				FilePath = "/files/documents/example.pdf"
			};

			await _mDocumentService.CreateAsync(sampleDocument);
			return CreatedAtAction(nameof(GetById), new { id = sampleDocument.Id }, sampleDocument);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] MDocument document)
		{
			if (id != document.Id)
				return BadRequest("ID uyuşmuyor.");

			await _mDocumentService.UpdateAsync(document);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _mDocumentService.DeleteAsync(id);
			return NoContent();
		}
	}
}
