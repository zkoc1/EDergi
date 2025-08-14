using EDergi.Application.DTOs;
using EDergi.Application.Interfaces.Services;
using EDergi.Domain.Entitites;
using EDergi.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Area("Admin")]
public class VolumeController : Controller
{
	private readonly IVolumeService _volumeService;

	public VolumeController(IVolumeService volumeService)
	{
		_volumeService = volumeService;
	}

	// Cilt Listesi
	public async Task<IActionResult> Index(Guid magazineId)
	{
		var volumes = await _volumeService.GetByMagazineIdAsync(magazineId);

		var viewModel = volumes.Select(v => new VolumeViewModel
		{
			Id = v.Id,
			Title = v.Title,
			Year = v.Year,
			MagazineId = v.MagazineId
		}).ToList();


		return View(viewModel);
	}

	[HttpGet]
	public IActionResult Create(Guid magazineId)
	{
		// MagazineId kontrolü ekleyelim
		if (magazineId == Guid.Empty)
		{
			TempData["Error"] = "Dergi bilgisi bulunamadı!";
			return RedirectToAction("Index", "Magazine");
		}

		var model = new VolumeViewModel
		{
			MagazineId = magazineId
		};

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Create(VolumeViewModel model)
	{
		// MagazineId kontrolü ekleyelim
		if (model.MagazineId == Guid.Empty)
		{
			ModelState.AddModelError("", "Dergi bilgisi bulunamadı!");
			return View(model);
		}

		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var dto = new VolumeCreateDto
		{
			Title = model.Title,
			Year = model.Year,
			MagazineId = model.MagazineId,
			Issues = new List<IssueCreateDto>()
		};

		await _volumeService.CreateAsync(dto);
		TempData["Success"] = "Cilt başarıyla oluşturuldu.";
		return RedirectToAction(nameof(Index), new { magazineId = model.MagazineId });
	}

	// Cilt Düzenleme Formu
	[HttpGet]
	public async Task<IActionResult> Edit(Guid id)
	{
		var volume = await _volumeService.GetByIdAsync(id);
		if (volume == null) return NotFound();

		var model = new VolumeViewModel
		{
			Id = volume.Id,
			Title = volume.Title,
			Year = volume.Year,
			MagazineId = volume.MagazineId
		};

		return View(model);
	}

	// Cilt Düzenleme İşlemi
	[HttpPost]
	public async Task<IActionResult> Edit(VolumeViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model); // Hatalarla birlikte formu tekrar göster
		}

		var volume = await _volumeService.GetByIdAsync(model.Id);
		if (volume == null) return NotFound();

		volume.Title = model.Title;
		volume.Year = model.Year;

		await _volumeService.UpdateAsync(volume);
		TempData["Success"] = "Cilt başarıyla güncellendi."; // Başarı mesajı
		return RedirectToAction(nameof(Index), new { magazineId = model.MagazineId });
	}

	// Cilt Silme İşlemi
	public async Task<IActionResult> Delete(Guid id)
	{
		var deleted = await _volumeService.DeleteAsync(id);
		if (!deleted)
		{
			TempData["Error"] = "Cilt silinirken bir hata oluştu."; // Hata mesajı
		}
		else
		{
			TempData["Success"] = "Cilt başarıyla silindi."; // Başarı mesajı
		}
		return RedirectToAction(nameof(Index)); // Listeleme sayfasına yönlendir
	}
}
