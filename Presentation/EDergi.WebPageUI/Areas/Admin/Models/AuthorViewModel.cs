using System;
using System.ComponentModel.DataAnnotations;

namespace EDergi.Web.Areas.Admin.Models
{
	public class AuthorViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }

		public string Affiliation { get; set; }
	}
}

