using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

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

		// Kullanıcı kayıt
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

		// Kullanıcı giriş
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

		
	}
}
