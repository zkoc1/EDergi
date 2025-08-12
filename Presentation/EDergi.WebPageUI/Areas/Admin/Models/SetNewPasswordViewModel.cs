using System.ComponentModel.DataAnnotations;

namespace WebPageUI.Areas.Admin.Models
{
	public class SetNewPasswordViewModel
	{
		[Required(ErrorMessage = "Geçici şifre gereklidir.")]
		public string TemporaryPassword { get; set; }

		[Required(ErrorMessage = "Yeni şifre gereklidir.")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Yeni şifre tekrarı gereklidir.")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
		public string ConfirmNewPassword { get; set; }
	}
}
