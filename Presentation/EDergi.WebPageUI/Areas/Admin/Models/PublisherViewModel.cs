using System;
using System.ComponentModel.DataAnnotations;

namespace EDergi.Web.Areas.Admin.Models
{
	public class PublisherViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Yayıncı adı zorunludur.")]
		public string Name { get; set; }
	}
}
