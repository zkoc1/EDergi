using EDergi.Application.Abstractions;
using EDergi.Application.Abstractions.Services;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using EDergi.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MagazineController : Controller
	{
		private readonly IMagazineService _magazineService;

		public MagazineController(IMagazineService magazineService)
		{
			_magazineService = magazineService;
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
				ImageUrl = m.ImageUrl,
				ImageName = m.ImageName,
				PublisherId = m.PublisherId
			}).ToList();

			return View(list);
		}

		public IActionResult Create()
		{
			return View(new MagazineViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(MagazineViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

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
				ImageUrl = model.ImageUrl,
				ImageName = model.ImageName,
				PublisherId = model.PublisherId
			};

			await _magazineService.CreateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(Guid id)
		{
			var magazine = await _magazineService.GetByIdAsync(id);
			if (magazine == null) return NotFound();

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
				ImageUrl = magazine.ImageUrl,
				ImageName = magazine.ImageName,
				PublisherId = magazine.PublisherId
			};

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(MagazineViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

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
			magazine.ImageUrl = model.ImageUrl;
			magazine.ImageName = model.ImageName;
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
}
