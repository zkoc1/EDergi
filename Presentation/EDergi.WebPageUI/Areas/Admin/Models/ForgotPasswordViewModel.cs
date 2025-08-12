using System.ComponentModel.DataAnnotations;

namespace WebPageUI.Areas.Admin.Models
{
	public class ForgotPasswordViewModel
	{
		[Required(ErrorMessage = "E-posta adresi gereklidir.")]
		[EmailAddress(ErrorMessage = "Geçersiz e-posta formatı.")]
		public string Email { get; set; }
	}
}
