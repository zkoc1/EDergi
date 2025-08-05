using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using WebPageUI.Areas.Admin.Models;
using System.Threading.Tasks;

namespace EDergi.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AccountController : Controller
	{
		private readonly IAuthService _authService;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountController(IAuthService authService, UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_authService = authService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya parola.");
				return View(model);
			}

			// user.UserName ile giriş yapıyoruz çünkü PasswordSignInAsync username bekler
			var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

			if (result.Succeeded)
			{
				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					return Redirect(returnUrl);

				return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
			}

			if (result.IsLockedOut)
			{
				ModelState.AddModelError(string.Empty, "Hesabınız kilitlenmiştir. Lütfen daha sonra tekrar deneyin.");
				return View(model);
			}

			ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya parola.");
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account", new { area = "Admin" });
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]  // İstersen rol bazlı kısıtlama ekleyebilirsin
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
		{
			if (!ModelState.IsValid)
				return View(dto);

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return RedirectToAction("Login");

			var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

			if (result.Succeeded)
			{
				TempData["PasswordChangeSuccess"] = true;
				return View();
			}

			foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

			return View(dto);
		}
	}
}
