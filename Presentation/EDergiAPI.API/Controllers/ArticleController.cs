using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DergiAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticleController : ControllerBase
	{
		private readonly IArticleService _articleService;

		public ArticleController(IArticleService articleService)
		{
			_articleService = articleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var articles = await _articleService.GetAllAsync();
			return Ok(articles);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var article = await _articleService.GetByIdAsync(id);
			if (article == null)
				return NotFound();
			return Ok(article);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ArticleCreateDto dto)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors)
											  .Select(e => e.ErrorMessage);
				return BadRequest(string.Join("; ", errors));
			}

			await _articleService.CreateAsync(dto);
			return Ok();
		}

		[HttpPut("approve/{id}")]
		public async Task<IActionResult> Approve(Guid id)
		{
			var article = await _articleService.GetByIdAsync(id);
			if (article == null)
				return NotFound();

			article.IsApproved = true;
			await _articleService.UpdateAsync(article);

			return Ok();
		}
		[HttpPut("reject/{id}")]
		public async Task<IActionResult> Reject(Guid id)
		{
			var article = await _articleService.GetByIdAsync(id);
			if (article == null)
				return NotFound();

			await _articleService.RejectAsync(id);
			return Ok("Makale reddedildi.");
		}
		
		[HttpGet("pending")]
		public async Task<IActionResult> GetPending()
		{
			var pendingArticles = await _articleService.GetPendingAsync();
			return Ok(pendingArticles);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _articleService.DeleteAsync(id);
			return Ok();
		}
	}
}