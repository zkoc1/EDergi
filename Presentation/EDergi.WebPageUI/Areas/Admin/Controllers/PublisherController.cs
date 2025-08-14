using EDergi.Application.Interfaces.Services;
using EDergi.Domain.Entitites;
using EDergi.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PublisherController : Controller
	{
		private readonly IPublisherService _publisherService;

		public PublisherController(IPublisherService publisherService)
		{
			_publisherService = publisherService;
		}

		public async Task<IActionResult> Index()
		{
			var publishers = await _publisherService.GetAllAsync();
			var list = publishers.Select(p => new PublisherViewModel
			{
				Id = p.Id,
				Name = p.Name
			}).ToList();

			return View(list);
		}

		public IActionResult Create()
		{
			return View(new PublisherViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(PublisherViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var publisher = new Publisher
			{
				Id = Guid.NewGuid(),
				Name = model.Name
			};

			await _publisherService.CreateAsync(publisher);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(Guid id)
		{
			var publisher = await _publisherService.GetByIdAsync(id);
			if (publisher == null) return NotFound();

			var vm = new PublisherViewModel
			{
				Id = publisher.Id,
				Name = publisher.Name
			};

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(PublisherViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var publisher = await _publisherService.GetByIdAsync(model.Id);
			if (publisher == null)
			{
				TempData["Error"] = "Yayıncı bulunamadı.";
				return RedirectToAction(nameof(Index));
			}

			publisher.Name = model.Name;

			await _publisherService.UpdateAsync(publisher);
			TempData["Success"] = "Yayıncı başarıyla güncellendi.";
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			await _publisherService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
