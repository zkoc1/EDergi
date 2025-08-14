using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace EDergi.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ReadIndexController : Controller
	{
		private readonly IWebHostEnvironment _env;

		public ReadIndexController(IWebHostEnvironment env)
		{
			_env = env;
		}

		public IActionResult Index(Guid magazineId)
		{
			// Burada DB'den ilgili Index kayıtları çekilir
			// Şimdilik örnek:
			ViewBag.MagazineId = magazineId;
			var list = new List<ReadIndexDto>
			{
				
			};
			return View(list);
		}

		[HttpGet]
		public IActionResult Create(Guid magazineId)
		{
			var dto = new ReadIndexCreateDto
			{
				MagazineId = magazineId
			};
			return View(dto);
		}

		[HttpPost]
		public IActionResult Create(ReadIndexCreateDto model)
		{
			if (model.ImageFiles != null && model.ImageFiles.Any())
			{
				var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "readindex");

				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);

				foreach (var file in model.ImageFiles)
				{
					var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
					var filePath = Path.Combine(uploadPath, fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

					// DB'ye ekleme işlemi burada yapılır (ImageUrl, ImageName vs.)
				}
			}

			TempData["Success"] = "İndeksler başarıyla eklendi.";
			return RedirectToAction("Index", new { magazineId = model.MagazineId });
		}
	}
}
