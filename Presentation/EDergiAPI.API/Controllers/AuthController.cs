using DergiAPI.Application.Abstractions;
using DergiAPI.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DergiAPI.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto model)
		{
			var result = await _authService.RegisterAsync(model);
			if (result == "Kayıt başarılı!")
				return Ok(new { message = result });

			return BadRequest(new { error = result });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto model)
		{
			var token = await _authService.LoginAsync(model);
			if (token == "Kullanıcı bulunamadı." || token == "Şifre yanlış.")
				return Unauthorized(new { error = token });

			return Ok(new { token });
		}
	}
}
