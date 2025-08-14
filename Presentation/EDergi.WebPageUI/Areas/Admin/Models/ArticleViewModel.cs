using System;
using System.ComponentModel.DataAnnotations;

namespace EDergi.Web.Areas.Admin.Models
{
	public class ArticleViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Başlık zorunludur.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Anahtar kelimeler zorunludur.")]
		public string Keywords { get; set; }

		public bool IsApproved { get; set; }

		[Required(ErrorMessage = "Sayı ID'si zorunludur.")]
		public Guid IssueId { get; set; }
	}
}
