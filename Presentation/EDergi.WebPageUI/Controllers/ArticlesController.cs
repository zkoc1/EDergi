using EDergi.Application.Abstractions.Services;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EDergi.Web.Controllers
{
	public class ArticlesController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly IAuthorService _authorService;

		public ArticlesController(IArticleService articleService, IAuthorService authorService)
		{
			_articleService = articleService;
			_authorService = authorService;
		}

		[HttpGet]
		public async Task<IActionResult> Create(Guid magazineId)
		{
			var authors = await _authorService.GetAllAsync();
			ViewBag.Authors = authors;
			ViewBag.MagazineId = magazineId;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ArticleCreateDto dto)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var issueId = await _articleService.GetIssueIdByMagazineIdAsync(dto.MagazineId);
					await _articleService.CreateAsync(dto, issueId);
					return RedirectToAction("Index", "Magazines");
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			var authors = await _authorService.GetAllAsync();
			ViewBag.Authors = authors;
			ViewBag.MagazineId = dto.MagazineId;
			return View(dto);
		}


	}
}
