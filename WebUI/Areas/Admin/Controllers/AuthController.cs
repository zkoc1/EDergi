using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EDergiAPI.Domain.Entitites;

namespace WebUI.Areas.Controllers
{
	[Area("Login")]
	public class AccountController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					var roles = await _userManager.GetRolesAsync(user);

					if (roles.Contains("Admin"))
						return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

					if (roles.Contains("AdminSys"))
						return RedirectToAction("Index", "SysPanel", new { area = "Sys" });

					return RedirectToAction("Index", "Home", new { area = "" });
				}
			}

			ViewBag.Error = "Hatalı e-posta veya şifre.";
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}
	}
}
