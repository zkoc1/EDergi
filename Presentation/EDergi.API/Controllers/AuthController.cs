using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EDergi.API.Controllers
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
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.RegisterAsync(model);

			return result == "Kayıt başarılı!"
				? Ok(new { message = result })
				: BadRequest(new { error = result });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.LoginAsync(model);

			if (!string.IsNullOrEmpty(result.Error))
				return Unauthorized(new { error = result.Error });

			var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
			var tokenObj = handler.ReadJwtToken(result.Token);
			var roleClaim = tokenObj.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
			var role = roleClaim?.Value ?? "User";

			return Ok(new { token = result.Token, role });
		}

		// ✅ Set Password (ilk defa giriş yapan kullanıcı için)
		[HttpPost("set-password")]
		public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.SetPasswordAsync(model);
			if (!result)
				return BadRequest(new { error = "Şifre güncellenemedi." });

			return Ok(new { message = "Şifre başarıyla güncellendi." });
		}

		// ✅ Forgot Password (şifre sıfırlama maili için)
		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.ForgotPasswordAsync(model);
			if (!result)
				return NotFound(new { error = "Kullanıcı bulunamadı." });

			return Ok(new { message = "Geçici şifre e-posta adresinize gönderildi." });
		}

		// ✅ Reset Password (geçici şifreyi girip yeni şifreyi ayarlamak için)
		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.ResetPasswordAsync(model);
			if (!result)
				return BadRequest(new { error = "Şifre sıfırlama başarısız." });

			return Ok(new { message = "Şifreniz başarıyla sıfırlandı." });
		}
	}
}
