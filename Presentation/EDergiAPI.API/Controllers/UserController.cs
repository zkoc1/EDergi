using DergiAPI.Application.DTOs;
using DergiAPI.Domain.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DergiAPI.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// 🔧 Kullanıcı kaydı
		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterDto model)
		{
			var user = new User
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				CreatedAt = DateTime.UtcNow
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			// Kullanıcıya varsayılan rol atama
			await _userManager.AddToRoleAsync(user, "User");

			return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
		}

		// 🔧 Kullanıcı girişi
		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
				return Unauthorized("Kullanıcı bulunamadı.");

			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!result.Succeeded)
				return Unauthorized("Şifre yanlış.");

			return Ok(new { Message = "Giriş başarılı." });
		}

		// 🔧 Kullanıcı bilgilerini güncelleme
		[HttpPut("update")]
		[Authorize]
		public async Task<IActionResult> Update([FromBody] UpdateUserDto model)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return NotFound("Kullanıcı bulunamadı.");

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok(new { Message = "Kullanıcı bilgileri güncellendi." });
		}

		// 🔧 Kullanıcı şifresini güncelleme
		[HttpPut("change-password")]
		[Authorize]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return NotFound("Kullanıcı bulunamadı.");

			var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok(new { Message = "Şifre başarıyla güncellendi." });
		}
	}
}
