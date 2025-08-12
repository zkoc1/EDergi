using System.ComponentModel.DataAnnotations;

namespace WebPageUI.Areas.Admin.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = " E-posta gereklidir.")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Şifre gereklidir.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Beni Hatırla")]
		public bool RememberMe { get; set; }

		public string? ReturnUrl { get; set; } // bu da lazım
	}
}
