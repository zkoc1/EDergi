// API/Controllers/AuthController.cs
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

		/// <summary>
		/// Kullanıcı kaydı yapar.
		/// </summary>
		/// <param name="model">Kayıt DTO'su (email, password, firstName, lastName)</param>
		/// <returns>Başarılıysa mesaj döner, değilse hata mesajı</returns>
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.RegisterAsync(model);

			if (result == "Kayıt başarılı!")
				return Ok(new { message = result });

			return BadRequest(new { error = result });
		}

		/// <summary>
		/// Giriş yapar ve JWT token döner.
		/// </summary>
		/// <param name="model">Giriş DTO'su (email, password)</param>
		/// <returns>Başarılıysa token, değilse hata mesajı</returns>
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var token = await _authService.LoginAsync(model);

			if (token == "Kullanıcı bulunamadı." || token == "Şifre yanlış.")
				return Unauthorized(new { error = token });

			return Ok(new { token });
		}
	}
}
