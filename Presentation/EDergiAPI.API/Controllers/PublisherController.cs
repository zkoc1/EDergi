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
	public class PublisherController : ControllerBase
	{
		private readonly IPublisherService _publisherService;

		public PublisherController(IPublisherService publisherService)
		{
			_publisherService = publisherService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Publisher>>> GetAll()
		{
			var publishers = await _publisherService.GetAllAsync();
			return Ok(publishers);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Publisher>> GetById(Guid id)
		{
			var publisher = await _publisherService.GetByIdAsync(id);
			if (publisher == null)
				return NotFound();

			return Ok(publisher);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Publisher publisher)
		{
			await _publisherService.CreateAsync(publisher);
			return CreatedAtAction(nameof(GetById), new { id = publisher.Id }, publisher);
		}

		// Yeni: Örnek Publisher oluşturup kaydeden endpoint
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var samplePublisher = new Publisher
			{
				Id = Guid.NewGuid(),
				Name = "Örnek Yayıncı",
				
				
			};

			await _publisherService.CreateAsync(samplePublisher);
			return CreatedAtAction(nameof(GetById), new { id = samplePublisher.Id }, samplePublisher);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Publisher publisher)
		{
			if (id != publisher.Id)
				return BadRequest("ID uyuşmuyor.");

			await _publisherService.UpdateAsync(publisher);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _publisherService.DeleteAsync(id);
			return NoContent();
		}
	}
}
