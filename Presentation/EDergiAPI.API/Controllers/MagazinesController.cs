using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MagazinesController : ControllerBase
	{
		private readonly IMagazineService _magazineService;

		public MagazinesController(IMagazineService magazineService)
		{
			_magazineService = magazineService;
		}

		// GET: api/Magazines
		[HttpGet]
		public async Task<ActionResult<List<Magazine>>> GetAll()
		{
			var magazines = await _magazineService.GetAllAsync();
			return Ok(magazines);
		}

		// GET: api/Magazines/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Magazine>> GetById(Guid id)
		{
			var magazine = await _magazineService.GetByIdAsync(id);
			if (magazine == null)
				return NotFound();

			return Ok(magazine);
		}

		// POST: api/Magazines
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Magazine magazine)
		{
			if (magazine == null)
				return BadRequest("Geçersiz veri.");

			await _magazineService.CreateAsync(magazine);
			return CreatedAtAction(nameof(GetById), new { id = magazine.Id }, magazine);
		}

		// POST: api/Magazines/create-sample
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var sampleMagazine = new Magazine
			{
				Title = "Bilimsel Araştırmalar Dergisi",
				Description = "Bilimsel araştırmaların yayımlandığı akademik dergi.",
				StartDate = new DateTime(2020, 1, 1),
				ISSN = "1234-5678",
				Period = "Yılda 2 kez",
				Purpose = "Akademik bilginin yaygınlaştırılması.",
				Scope = "Bilim, Teknoloji, Mühendislik",
				WritingRules = "APA 7 formatı kullanılmalı.",
				JournalRules = "Yazar rehberine uygun gönderim yapılmalı.",
				ViewStats = new ViewStats
				{
					ViewCount = 1000,
					FavoriteCount = 50,
					DownloadCount = 300
				},
				Publishers = new List<Publisher>(),
				Documents = new List<MDocument>(),
				Indexes = new List<ReadIndex>(),
				Archives = new List<MNumberOf>(),
				Articles = new List<Article>()
			};

			await _magazineService.CreateAsync(sampleMagazine);
			return CreatedAtAction(nameof(GetById), new { id = sampleMagazine.Id }, sampleMagazine);
		}

		// PUT: api/Magazines/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Magazine magazine)
		{
			if (magazine == null || id != magazine.Id)
				return BadRequest("ID uyuşmuyor veya veri eksik.");

			await _magazineService.UpdateAsync(magazine);
			return NoContent();
		}

		// DELETE: api/Magazines/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _magazineService.DeleteAsync(id);
			return NoContent();
		}
	}
}
