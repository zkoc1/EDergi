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
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService _authorService;

		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		// GET: api/Author
		[HttpGet]
		public async Task<ActionResult<List<Author>>> GetAll()
		{
			var authors = await _authorService.GetAllAsync();
			return Ok(authors);
		}

		// GET: api/Author/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Author>> GetById(Guid id)
		{
			var author = await _authorService.GetByIdAsync(id);
			if (author == null)
				return NotFound();

			return Ok(author);
		}

		// POST: api/Author
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Author author)
		{
			if (author == null)
				return BadRequest("Geçersiz veri.");

			await _authorService.CreateAsync(author);
			return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
		}

		// ÖRNEK: Elle bir yazar oluştur
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var newAuthor = new Author
			{
				Name = "Prof. Dr. Zeynep Kaya",
				Affiliation = "İstanbul Teknik Üniversitesi"
			};

			await _authorService.CreateAsync(newAuthor);
			return CreatedAtAction(nameof(GetById), new { id = newAuthor.Id }, newAuthor);
		}

		// PUT: api/Author/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Author author)
		{
			if (author == null || id != author.Id)
				return BadRequest("ID eşleşmiyor veya veri eksik.");

			await _authorService.UpdateAsync(author);
			return NoContent();
		}

		// DELETE: api/Author/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _authorService.DeleteAsync(id);
			return NoContent();
		}
	}
}
