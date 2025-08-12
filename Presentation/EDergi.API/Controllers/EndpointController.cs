using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EDergi.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "Admin")]
	public class EndpointController : ControllerBase
	{
		private readonly IEndpointService _endpointService;

		public EndpointController(IEndpointService endpointService)
		{
			_endpointService = endpointService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var endpoints = await _endpointService.GetAllEndpointsAsync();
			return Ok(endpoints);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] EndpointDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var success = await _endpointService.RegisterEndpointAsync(dto);
			if (!success)
				return BadRequest("Endpoint kaydı başarısız veya zaten mevcut.");

			return Ok("Endpoint başarıyla kaydedildi.");
		}
	}
}
