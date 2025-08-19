using Microsoft.AspNetCore.Mvc;
using EDergi.Application.Abstractions.Services;
using System.Threading.Tasks;

namespace EDergi.Web.Controllers
{
	public class ResearchersController : Controller
	{
		private readonly IAuthorService _authorService;

		public ResearchersController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		public async Task<IActionResult> Index()
		{
			var authors = await _authorService.GetAllAsync();
			return View(authors); // List<Author> gönderiyoruz
		}

	}
}
