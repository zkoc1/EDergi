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
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IEmailService _emailService;

		public AccountController(
			IAuthService authService,
			UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			IEmailService emailService)
		{
			_authService = authService;
			_userManager = userManager;
			_signInManager = signInManager;
			_emailService = emailService;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			if (!ModelState.IsValid)
				return View(model);

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");
				return View(model);
			}

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);

			if (result.Succeeded)
			{
				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					return Redirect(returnUrl);

				if (!user.IsChangedPassword)
				{
					return RedirectToAction("SetNewPassword", "Account", new { area = "Admin" });
				}

				return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
			}

			if (result.IsLockedOut)
			{
				ModelState.AddModelError(string.Empty, "Hesabınız kilitlenmiştir. Lütfen daha sonra tekrar deneyin.");
				return View(model);
			}

			ModelState.AddModelError(string.Empty, "Geçersiz giriş bilgileri.");
			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account", new { area = "Admin" });
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				ModelState.AddModelError("", "Bu e-posta ile kayıtlı kullanıcı bulunamadı.");
				return View(model);
			}

			var tempPassword = new Random().Next(100000, 999999).ToString();
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var resetResult = await _userManager.ResetPasswordAsync(user, token, tempPassword);

			if (!resetResult.Succeeded)
			{
				ModelState.AddModelError("", "Şifre sıfırlanırken hata oluştu.");
				return View(model);
			}

			user.IsChangedPassword = false;
			await _userManager.UpdateAsync(user);

			await _emailService.SendTemporaryPasswordAsync(user.Email, tempPassword, user.UserName);

			TempData["SuccessMessage"] = "Geçici şifreniz e-posta adresinize gönderildi.";
			return RedirectToAction("Login");
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult SetNewPassword()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SetNewPassword(SetNewPasswordViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var user = await _userManager.FindByEmailAsync(User.Identity.Name);
			if (user == null)
			{
				ModelState.AddModelError("", "Kullanıcı bulunamadı.");
				return View(model);
			}

			var isValidPassword = await _userManager.CheckPasswordAsync(user, model.TemporaryPassword);
			if (!isValidPassword)
			{
				ModelState.AddModelError("", "Geçici şifre yanlış.");
				return View(model);
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
					ModelState.AddModelError("", error.Description);
				return View(model);
			}

			user.IsChangedPassword = true;
			await _userManager.UpdateAsync(user);

			TempData["SuccessMessage"] = "Şifreniz başarıyla güncellendi. Yeni şifrenizle giriş yapabilirsiniz.";
			return RedirectToAction("Login");
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var tempPassword = new Random().Next(100000, 999999).ToString();

			var user = new AppUser
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				BirthDate = model.BirthDate,
				IsChangedPassword = false
			};

			var result = await _userManager.CreateAsync(user, tempPassword);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
					ModelState.AddModelError("", error.Description);
				return View(model);
			}

			// Kullanıcıya "User" rolünü ata
			var roleResult = await _userManager.AddToRoleAsync(user, "User");
			if (!roleResult.Succeeded)
			{
				foreach (var error in roleResult.Errors)
					ModelState.AddModelError("", error.Description);
				return View(model);
			}

			await _emailService.SendTemporaryPasswordAsync(user.Email, tempPassword, user.UserName);

			TempData["SuccessMessage"] = "Kayıt işlemi başarılı. Geçici şifreniz e-posta adresinize gönderildi.";
			return RedirectToAction("Login");
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
