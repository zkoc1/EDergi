using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ArticleIssuesController : ControllerBase
	{
		private readonly IArticleIssueService _articleIssueService;

		public ArticleIssuesController(IArticleIssueService articleIssueService)
		{
			_articleIssueService = articleIssueService;
		}

		// GET: api/ArticleIssues
		[HttpGet]
		public async Task<ActionResult<List<ArticleIssue>>> GetAll()
		{
			var list = await _articleIssueService.GetAllAsync();
			return Ok(list);
		}

		// GET: api/ArticleIssues/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<ArticleIssue>> GetById(Guid id)
		{
			var articleIssue = await _articleIssueService.GetByIdAsync(id);
			if (articleIssue == null)
				return NotFound();

			return Ok(articleIssue);
		}

		// POST: api/ArticleIssues
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ArticleIssue articleIssue)
		{
			if (articleIssue == null)
				return BadRequest("Geçersiz veri.");

			await _articleIssueService.CreateAsync(articleIssue);
			return CreatedAtAction(nameof(GetById), new { id = articleIssue.Id }, articleIssue);
		}

		// Örnek veri eklemek için: api/ArticleIssues/create-sample
		
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var newArticleIssue = new ArticleIssue
			{
				Id = Guid.NewGuid(),
				ArticleId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Gerçek Article ID ile değiştir
				IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"),   // Gerçek Issue ID ile değiştir
				CreatedDate = DateTime.UtcNow
			};

			await _articleIssueService.CreateAsync(newArticleIssue);
			return CreatedAtAction(nameof(GetById), new { id = newArticleIssue.Id }, newArticleIssue);
		}

		// PUT: api/ArticleIssues/{id}
		
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] ArticleIssue articleIssue)
		{
			if (articleIssue == null || id != articleIssue.Id)
				return BadRequest("ID eşleşmiyor veya geçersiz veri.");

			await _articleIssueService.UpdateAsync(articleIssue);
			return NoContent();
		}

		// DELETE: api/ArticleIssues/{id}
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _articleIssueService.DeleteAsync(id);
			return NoContent();
		}
	}
}
