// Areas/Admin/Controllers/MDocumentController.cs
using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Application.Interfaces.Services;
using EDergi.Domain.Entitites;
using EDergi.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Area("Admin")]
public class MDocumentController : Controller
{
	private readonly IDocumentService _documentService;
	private readonly IFileUploadService _fileUploadService;

	public MDocumentController(IDocumentService documentService, IFileUploadService fileUploadService)
	{
		_documentService = documentService;
		_fileUploadService = fileUploadService;
	}

	public async Task<IActionResult> Index(Guid magazineId)
	{
		var documents = await _documentService.GetByMagazineIdAsync(magazineId);
		return View(documents);
	}

	public IActionResult Create(Guid magazineId)
	{
		var model = new MDocumentCreateViewModel
		{
			MagazineId = magazineId
		};
		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Create(MDocumentCreateViewModel model, List<IFormFile> files)
	{
		if (ModelState.IsValid && files != null && files.Count > 0)
		{
			foreach (var file in files)
			{
				var (fileName, fileUrl) = await _fileUploadService.UploadFileAsync(file);

				var document = new MDocument
				{
					MagazineId = model.MagazineId,
					FileName = fileName,
					FilePath = fileUrl,
					CreatedDate = DateTime.UtcNow
				};

				await _documentService.CreateAsync(document);
			}

			TempData["Success"] = "Doküman(lar) başarıyla yüklendi.";
			return RedirectToAction("Index", new { magazineId = model.MagazineId });
		}

		return View(model);
	}

	public async Task<IActionResult> Edit(Guid id)
	{
		var document = await _documentService.GetByIdAsync(id);
		if (document == null)
			return NotFound();

		var model = new MDocumentEditViewModel
		{
			Id = document.Id,
			MagazineId = document.MagazineId,
			FileName = document.FileName,
			FilePath = document.FilePath
		};

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Edit(MDocumentEditViewModel model)
	{
		if (ModelState.IsValid)
		{
			var document = await _documentService.GetByIdAsync(model.Id);
			if (document == null)
				return NotFound();

			document.FileName = model.FileName;
			document.FilePath = model.FilePath;

			await _documentService.UpdateAsync(document);

			TempData["Success"] = "Doküman başarıyla güncellendi.";
			return RedirectToAction("Index", new { magazineId = document.MagazineId });
		}

		return View(model);
	}
}
