using EDergi.Application.Abstractions;
using EDergi.Application.Abstractions.Services;
using EDergi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

public class VolumesController : Controller
{
	private readonly IVolumeService _volumeService;
	private readonly IIssueService _issueService;
	private readonly IArticleService _articleService;

	public VolumesController(
		IVolumeService volumeService,
		IIssueService issueService,
		IArticleService articleService)
	{
		_volumeService = volumeService;
		_issueService = issueService;
		_articleService = articleService;
	}

	public async Task<IActionResult> Details(Guid id)
	{
		var volume = await _volumeService.GetByIdAsync(id);
		if (volume == null)
			return NotFound();

		// Volume → Issues → Articles ilişkisini doldur
		var issues = await _issueService.GetIssuesByVolumeIdAsync(id);

		foreach (var issue in issues)
		{
			var articles = await _articleService.GetByIssueIdAsync(issue.Id);
			issue.Articles = articles;
		}

		volume.Issues = issues;
		return View(volume);
	}
}
