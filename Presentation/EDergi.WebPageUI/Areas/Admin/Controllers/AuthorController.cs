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
	public class AuthorController : Controller
	{
		private readonly IAuthorService _authorService;

		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		public async Task<IActionResult> Index()
		{
			var authors = await _authorService.GetAllAsync();
			var list = authors.Select(a => new AuthorViewModel
			{
				Id = a.Id,
				Name = a.Name,
				Email = a.Email,
				Affiliation = a.Affiliation
			}).ToList();

			return View(list);
		}

		public IActionResult Create()
		{
			return View(new AuthorViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(AuthorViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var dto = new AuthorCreateDto
			{
				Name = model.Name,
				Email = model.Email,
				Affiliation = model.Affiliation
			};

			await _authorService.CreateAsync(dto);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Edit(Guid id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();

            var vm = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                Affiliation = author.Affiliation
            };

            return View(vm);
        }

    [HttpPost]
public async Task<IActionResult> Edit(AuthorViewModel model)
{
    if (!ModelState.IsValid)
    {
        // ModelState hatalarını logla veya kullanıcıya göster
        return View(model);
    }

    var author = await _authorService.GetByIdAsync(model.Id);
    if (author == null)
    {
        TempData["Error"] = "Yazar bulunamadı.";
        return RedirectToAction(nameof(Index));
    }

    author.Name = model.Name;
    author.Email = model.Email;
    author.Affiliation = model.Affiliation;

    try
    {
        await _authorService.UpdateAsync(author);
        TempData["Success"] = "Yazar başarıyla güncellendi.";
    }
    catch (Exception ex)
    {
        TempData["Error"] = "Yazar güncellenirken bir hata oluştu: " + ex.Message;
    }

    return RedirectToAction(nameof(Index));
}
		public async Task<IActionResult> Delete(Guid id)
		{
			await _authorService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

	}
}
