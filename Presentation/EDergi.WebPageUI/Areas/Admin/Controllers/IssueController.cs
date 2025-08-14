
using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
namespace EDergi.WebPageUI.Areas.Admin.Controllers {
	[Area("Admin")]
	public class IssueController : Controller
	{
		private readonly IIssueService _issueService;

		public IssueController(IIssueService issueService)
		{
			_issueService = issueService;
		}

		// Sayı Listesi
		public async Task<IActionResult> Index(Guid volumeId)
		{
			var issues = await _issueService.GetByVolumeIdAsync(volumeId);

			var viewModel = issues.Select(i => new IssueViewModel
			{
				Id = i.Id,
				IssueNumber = i.IssueNumber,
				PublishDate = i.PublishDate,
				VolumeId = i.VolumeId
			}).ToList();

			ViewBag.VolumeId = volumeId;
			return View(viewModel);
		}

		// Yeni Sayı Ekleme Formu
		public IActionResult Create(Guid volumeId)
		{
			ViewBag.VolumeId = volumeId;
			return View();
		}

		// Yeni Sayı Ekleme İşlemi
		[HttpPost]
		public async Task<IActionResult> Create(IssueViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var dto = new IssueCreateDto
			{
				IssueNumber = model.IssueNumber,
				PublishDate = model.PublishDate,
				VolumeId = model.VolumeId
			};

			await _issueService.CreateAsync(dto);
			return RedirectToAction(nameof(Index), new { volumeId = model.VolumeId });
		}

		// Sayı Düzenleme Formu
		public async Task<IActionResult> Edit(Guid id)
		{
			var issue = await _issueService.GetByIdAsync(id);
			if (issue == null) return NotFound();

			var model = new IssueViewModel
			{
				Id = issue.Id,
				IssueNumber = issue.IssueNumber,
				PublishDate = issue.PublishDate,
				VolumeId = issue.VolumeId
			};

			return View(model);
		}

		// Sayı Düzenleme İşlemi
		[HttpPost]
		public async Task<IActionResult> Edit(IssueViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var issue = await _issueService.GetByIdAsync(model.Id);
			if (issue == null) return NotFound();

			issue.IssueNumber = model.IssueNumber;
			issue.PublishDate = model.PublishDate;

			await _issueService.UpdateAsync(issue);
			return RedirectToAction(nameof(Index), new { volumeId = model.VolumeId });
		}

		// Sayı Silme İşlemi
		public async Task<IActionResult> Delete(Guid id)
		{
			await _issueService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}