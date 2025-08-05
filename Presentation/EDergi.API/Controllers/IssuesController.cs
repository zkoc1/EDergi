// IssueController.cs
using EDergi.Application.Abstractions;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EDergi.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class IssueController : ControllerBase
	{
		private readonly IIssueService _issueService;

		public IssueController(IIssueService issueService)
		{
			_issueService = issueService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var issues = await _issueService.GetAllAsync();
			return Ok(issues);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var issue = await _issueService.GetByIdAsync(id);
			return issue != null ? Ok(issue) : NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] IssueCreateDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _issueService.CreateAsync(dto);
			return Ok();
		}



		[HttpPut]
		public async Task<IActionResult> Update([FromBody] Issue issue)
		{
			await _issueService.UpdateAsync(issue);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _issueService.DeleteAsync(id);
			return Ok();
		}
	}
}
