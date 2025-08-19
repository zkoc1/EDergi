using EDergi.Application.Abstractions;
using EDergi.Persistence.Concretes;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPageUI.Models;

namespace WebPageUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IMagazineService _magazineService;

		public HomeController(ILogger<HomeController> logger, IMagazineService magazineService)
		{
			_logger = logger;
			_magazineService = magazineService;
		}
		
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		
	}
}
