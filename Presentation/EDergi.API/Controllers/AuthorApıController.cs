using EDergi.Application.Abstractions.Services;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EDergi.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthorApıController : ControllerBase
	{
		private readonly IAuthorService _authorService;

		public AuthorApıController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var authors = await _authorService.GetAllAsync();
			return Ok(authors);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var author = await _authorService.GetByIdAsync(id);
			return author == null ? NotFound() : Ok(author);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AuthorCreateDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _authorService.CreateAsync(dto);
			return Ok();
		}


		[HttpPut]
		public async Task<IActionResult> Update([FromBody] Author author)
		{
			await _authorService.UpdateAsync(author);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _authorService.DeleteAsync(id);
			return Ok();
		}
	}
}
