using System.ComponentModel.DataAnnotations;

namespace WebPageUI.Areas.Admin.Models
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Ad gereklidir.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Soyad gereklidir.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "E-posta gereklidir.")]
		[EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Doğum tarihi gereklidir.")]
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }
	}
}
