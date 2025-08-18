using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EDergi.Web.Controllers
{
	public class MagazinesController : Controller
	{
		private readonly IMagazineService _magazineService;

		public MagazinesController(IMagazineService magazineService)
		{
			_magazineService = magazineService;
		}

		// Listeleme (Partial'a veri gönderilecek)
		// Listeleme (Partial)

		// Ana sayfa için Index metodu
		public async Task<IActionResult> Index()
		{
			var dtoList = await _magazineService.GetAllAsync2();
			return View(dtoList);
		}

		// Partial View için veri gönderme
		public async Task<IActionResult> GetMagazinesPartial()
		{
			var dtoList = await _magazineService.GetAllAsync2();
			if (dtoList == null || !dtoList.Any())
			{
				// Eğer dergi listesi boşsa, boş bir liste gönder
				dtoList = new List<MagazineCreateDto>();
			}
			return PartialView("~/Views/Shared/_MagazinesPartial.cshtml", dtoList);
		}

		// Detay sayfası
		public async Task<IActionResult> Details(Guid id)
		{
			var dto = await _magazineService.GetByIdAsync(id);
			if (dto == null) return NotFound();

			return View(dto);
		}
	}
}
