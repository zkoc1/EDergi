using EDergi.Application.Abstractions.Services;
using EDergi.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ArticleController : Controller
	{
		private readonly IArticleService _articleService;

		public ArticleController(IArticleService articleService)
		{
			_articleService = articleService;
		}

		// Belirli bir Issue'ya ait makaleleri listeleme
		public async Task<IActionResult> Index(Guid issueId)
		{
			var articles = await _articleService.GetByIssueIdAsync(issueId);

			var viewModel = articles.Select(a => new ArticleViewModel
			{
				Id = a.Id,
				Title = a.Title,
				Keywords = a.Keywords,
				IsApproved = a.IsApproved,
				IssueId = a.IssueId
			}).ToList();

			ViewBag.IssueId = issueId;
			return View(viewModel);
		}

		// Makale Onaylama
		[HttpPost]
		public async Task<IActionResult> Approve(Guid id)
		{
			var article = await _articleService.GetByIdAsync(id);
			if (article != null)
			{
				article.IsApproved = true;
				await _articleService.UpdateAsync(article);
			}
			return RedirectToAction(nameof(Index), new { issueId = article?.IssueId });
		}

		// Makale Reddetme
		[HttpPost]
		public async Task<IActionResult> Reject(Guid id)
		{
			var article = await _articleService.GetByIdAsync(id);
			if (article != null)
			{
				await _articleService.DeleteAsync(id);
			}
			return RedirectToAction(nameof(Index), new { issueId = article?.IssueId });
		}
	}
}
