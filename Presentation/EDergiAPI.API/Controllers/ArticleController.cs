using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ArticleController : ControllerBase
	{
		private readonly IArticleService _articleService;

		public ArticleController(IArticleService articleService)
		{
			_articleService = articleService;
		}

		// GET: api/Article
		[HttpGet]
		public async Task<ActionResult<List<Article>>> GetAll()
		{
			var articles = await _articleService.GetAllAsync();
			return Ok(articles);
		}

		// GET: api/Article/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Article>> GetById(Guid id)
		{
			var article = await _articleService.GetByIdAsync(id);
			if (article == null)
				return NotFound();

			return Ok(article);
		}

		// POST: api/Article
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Article article)
		{
			if (article == null)
				return BadRequest();

			await _articleService.CreateAsync(article);
			return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
		}

		// ÖRNEK: Manuel Article nesnesi oluşturup kaydeden endpoint
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSampleArticle()
		{
			var sampleArticle = new Article
			{
				Title = "Yapay Zekâ ile Akademik Yayınlar",
				Year = 2025,
				Description = "Bu makale, yapay zekânın akademik yayınlardaki etkisini inceler.",
				Keywords = "Yapay Zeka, Akademik, Makale",
				SupportingInstitution = "OpenAI Research",
				ProjectNumber = "AI-2025-001",
				Reference = "https://openai.com/articles/ai-publications",
				ArticleLink = "https://dergipark.org.tr/ai-article",

				// İlişkili koleksiyonlar boş başlatılabilir
				Authors = new List<Author>(),
				Volumes = new List<Volume>(),
				ArticleIssues = new List<ArticleIssue>()
			};

			await _articleService.CreateAsync(sampleArticle);
			return CreatedAtAction(nameof(GetById), new { id = sampleArticle.Id }, sampleArticle);
		}

		// PUT: api/Article/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Article article)
		{
			if (article == null || id != article.Id)
				return BadRequest();

			await _articleService.UpdateAsync(article);
			return NoContent();
		}

		// DELETE: api/Article/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _articleService.DeleteAsync(id);
			return NoContent();
		}
	}
}
