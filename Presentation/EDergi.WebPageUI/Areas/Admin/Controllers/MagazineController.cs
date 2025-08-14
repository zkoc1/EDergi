using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Application.Interfaces.Services;
using EDergi.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Area("Admin")]
public class MagazineController : Controller
{
	private readonly IMagazineService _magazineService;
	private readonly IPublisherService _publisherService;
	private readonly IFileUploadService _fileUploadService;

	public MagazineController(IMagazineService magazineService, IPublisherService publisherService, IFileUploadService fileUploadService)
	{
		_magazineService = magazineService;
		_publisherService = publisherService;
		_fileUploadService = fileUploadService;

	}

	public async Task<IActionResult> Index()
	{
		var magazines = await _magazineService.GetAllAsync();
		var list = magazines.Select(m => new MagazineViewModel
		{
			Id = m.Id,
			Title = m.Title,
			Description = m.Description,
			StartDate = m.StartDate,
			ISSN = m.ISSN,
			Period = m.Period,
			Purpose = m.Purpose,
			Scope = m.Scope,
			WritingRules = m.WritingRules,
			JournalRules = m.JournalRules,
			PublisherId = m.PublisherId,
			ViewStats = new ViewStatsDto { ViewCount = 0, FavoriteCount = 0, DownloadCount = 0 },
		
		}).ToList();

		return View(list);
	}

	public async Task<IActionResult> Create()
	{
		var publishers = await _publisherService.GetAllAsync();
		ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
		return View(new MagazineViewModel());
	}

	[HttpPost]
	public async Task<IActionResult> Create(MagazineViewModel model, IFormFile ImageFile)
	{
		
		if (!ModelState.IsValid)
		{
			var publishers = await _publisherService.GetAllAsync();
			ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
			return View(model);
		}

		// Dosya yükleme işlemleri

		var (fileName, fileUrl) = await _fileUploadService.UploadFileAsync(ImageFile);
		 var ImageUrl = fileUrl;
		 var ImageName = fileName;
		var dto = new MagazineCreateDto
		{
			Title = model.Title,
			Description = model.Description,
			StartDate = model.StartDate,
			ISSN = model.ISSN,
			Period = model.Period,
			Purpose = model.Purpose,
			Scope = model.Scope,
			WritingRules = model.WritingRules,
			JournalRules = model.JournalRules,
			ImageUrl = ImageUrl,
			ImageName = ImageName,
			PublisherId = model.PublisherId,
		};

		await _magazineService.CreateAsync(dto);
		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Edit(Guid id)
	{
		var magazine = await _magazineService.GetByIdAsync(id);
		if (magazine == null) return NotFound();

		var publishers = await _publisherService.GetAllAsync();
		ViewBag.Publishers = new SelectList(publishers, "Id", "Name", magazine.PublisherId);

		var vm = new MagazineViewModel
		{
			Id = magazine.Id,
			Title = magazine.Title,
			Description = magazine.Description,
			StartDate = magazine.StartDate,
			ISSN = magazine.ISSN,
			Period = magazine.Period,
			Purpose = magazine.Purpose,
			Scope = magazine.Scope,
			WritingRules = magazine.WritingRules,
			JournalRules = magazine.JournalRules,
			PublisherId = magazine.PublisherId,
			ViewStats = new ViewStatsDto { ViewCount = 0, FavoriteCount = 0, DownloadCount = 0 },
			
		};

		return View(vm);
	}

	[HttpPost]
	public async Task<IActionResult> Edit(MagazineViewModel model)
	{
		if (!ModelState.IsValid)
		{
			var publishers = await _publisherService.GetAllAsync();
			ViewBag.Publishers = new SelectList(publishers, "Id", "Name", model.PublisherId);
			return View(model);
		}


		
		var magazine = await _magazineService.GetByIdAsync(model.Id);
		if (magazine == null)
		{
			TempData["Error"] = "Dergi bulunamadı.";
			return RedirectToAction(nameof(Index));
		}

		magazine.Title = model.Title;
		magazine.Description = model.Description;
		magazine.StartDate = model.StartDate;
		magazine.ISSN = model.ISSN;
		magazine.Period = model.Period;
		magazine.Purpose = model.Purpose;
		magazine.Scope = model.Scope;
		magazine.WritingRules = model.WritingRules;
		magazine.JournalRules = model.JournalRules;
		magazine.PublisherId = model.PublisherId;

		try
		{
			await _magazineService.UpdateAsync(magazine);
			TempData["Success"] = "Dergi başarıyla güncellendi.";
		}
		catch (Exception ex)
		{
			TempData["Error"] = "Dergi güncellenirken bir hata oluştu: " + ex.Message;
		}

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _magazineService.DeleteAsync(id);
		return RedirectToAction(nameof(Index));
	}
}
