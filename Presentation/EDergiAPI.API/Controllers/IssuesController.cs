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
	public class IssuesController : ControllerBase
	{
		private readonly IIssueService _issueService;

		public IssuesController(IIssueService issueService)
		{
			_issueService = issueService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Issue>>> GetAll()
		{
			var issues = await _issueService.GetAllAsync();
			return Ok(issues);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Issue>> GetById(Guid id)
		{
			var issue = await _issueService.GetByIdAsync(id);
			if (issue == null)
				return NotFound();

			return Ok(issue);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Issue issue)
		{
			await _issueService.CreateAsync(issue);
			return CreatedAtAction(nameof(GetById), new { id = issue.Id }, issue);
		}

		// Örnek (sample) Issue oluşturup ekleyen endpoint
		[HttpPost("create-sample")]
		public async Task<IActionResult> CreateSample()
		{
			var sampleIssue = new Issue
			{
				Id = Guid.NewGuid(),
				IssueNumber = 1,
				VolumeId = Guid.NewGuid(), 
				ArticleIssues = new List<ArticleIssue>(), 
				CreatedDate = DateTime.UtcNow
			};

			await _issueService.CreateAsync(sampleIssue);
			return CreatedAtAction(nameof(GetById), new { id = sampleIssue.Id }, sampleIssue);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Issue issue)
		{
			if (id != issue.Id)
				return BadRequest("ID uyuşmuyor.");

			await _issueService.UpdateAsync(issue);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _issueService.DeleteAsync(id);
			return NoContent();
		}
	}
}
