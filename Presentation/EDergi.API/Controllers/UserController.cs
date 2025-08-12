using EDergi.Application.DTOs;
using EDergi.Application.DTOs.EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EDergi.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = new AppUser
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

			await _userManager.AddToRoleAsync(user, model.RoleName ?? "User");

			return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
		}

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

		[HttpPut("update")]
		[Authorize]
		public async Task<IActionResult> Update([FromBody] UpdateUserDto model)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return NotFound("Kullanıcı bulunamadı.");

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.UpdatedAt = DateTime.UtcNow;

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok(new { Message = "Kullanıcı bilgileri güncellendi." });
		}

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
		[HttpPost("assign-roles")]
		[Authorize(Roles = "Admin")] // Sadece Admin rolüne sahip kullanıcılar bu endpoint'i kullanabilir
		public async Task<IActionResult> AssignRolesToUser([FromBody] AssignRolesDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = await _userManager.FindByIdAsync(model.UserId.ToString());
			if (user == null)
				return NotFound("Kullanıcı bulunamadı.");

			var currentRoles = await _userManager.GetRolesAsync(user);
			await _userManager.RemoveFromRolesAsync(user, currentRoles);

			var result = await _userManager.AddToRolesAsync(user, model.Roles);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok(new { Message = "Roller başarıyla atandı." });
		}

	}
}
