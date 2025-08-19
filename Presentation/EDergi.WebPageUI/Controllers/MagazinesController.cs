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
		//public async Task<IActionResult> GetMagazinesPartial()
		//{
		//	var dtoList = await _magazineService.GetAllAsync2();
		//	if (dtoList == null || !dtoList.Any())
		//	{
		//		// Eğer dergi listesi boşsa, boş bir liste gönder
		//		dtoList = new List<MagazineCreateDto>();
		//	}
		//	return PartialView("~/Views/Shared/.cshtml", dtoList);
		//}

		// Detay sayfası
		public async Task<IActionResult> Details(Guid id)
		{
			var dto = await _magazineService.GetByIdAsync(id);
			if (dto == null) return NotFound();

			return View(dto);
		}
		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] string q)
		
		
		{
			var results = await _magazineService.SearchAsync(q);
			return Ok(results); // JSON
		}

		[HttpGet]
		public async Task<IActionResult> Search_([FromQuery] string q, [FromQuery] string[] periods)
		{
			// Tüm listeyi çek
			var list = await _magazineService.GetAllAsync2();

			// Metin arama (opsiyonel)
			if (!string.IsNullOrWhiteSpace(q))
			{
				var qNorm = q.Trim();
				list = list.Where(m =>
						(!string.IsNullOrEmpty(m.Title) && m.Title.Contains(qNorm, StringComparison.OrdinalIgnoreCase)) ||
						(!string.IsNullOrEmpty(m.Description) && m.Description.Contains(qNorm, StringComparison.OrdinalIgnoreCase)) ||
						(!string.IsNullOrEmpty(m.ISSN) && m.ISSN.Contains(qNorm, StringComparison.OrdinalIgnoreCase))
					)
					.ToList();
			}

			// Periyot filtre (opsiyonel) — string karşılaştırma, trim + ignore case
			if (periods != null && periods.Length > 0)
			{
				var wanted = new HashSet<string>(
					periods.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p.Trim()),
					StringComparer.OrdinalIgnoreCase
				);

				list = list
					.Where(m => !string.IsNullOrWhiteSpace(m.Period) && wanted.Contains(m.Period.Trim()))
					.ToList();
			}

			return Json(list); // JSON
		}

	}
}
