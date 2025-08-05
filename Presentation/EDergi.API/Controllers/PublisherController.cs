using EDergi.Application.Interfaces.Services;
using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EDergi.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublisherController : ControllerBase
	{
		private readonly IPublisherService _publisherService;

		public PublisherController(IPublisherService publisherService)
		{
			_publisherService = publisherService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var publishers = await _publisherService.GetAllAsync();
			return Ok(publishers);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var publisher = await _publisherService.GetByIdAsync(id);
			if (publisher == null) return NotFound();
			return Ok(publisher);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Publisher publisher)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var created = await _publisherService.CreateAsync(publisher);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Publisher publisher)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (id != publisher.Id) return BadRequest("Id mismatch");

			var existing = await _publisherService.GetByIdAsync(id);
			if (existing == null) return NotFound();

			var updated = await _publisherService.UpdateAsync(publisher);
			return Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleted = await _publisherService.DeleteAsync(id);
			if (!deleted) return NotFound();

			return NoContent();
		}
	}
}
