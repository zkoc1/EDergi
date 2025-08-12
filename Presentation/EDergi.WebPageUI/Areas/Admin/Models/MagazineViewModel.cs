using System;
using System.ComponentModel.DataAnnotations;

namespace EDergi.Web.Areas.Admin.Models
{
	public class MagazineViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Başlık zorunludur.")]
		public string Title { get; set; }

		public string Description { get; set; }

		[Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "ISSN zorunludur.")]
		public string ISSN { get; set; }

		public string Period { get; set; }
		public string Purpose { get; set; }
		public string Scope { get; set; }
		public string WritingRules { get; set; }
		public string JournalRules { get; set; }
		public string ImageUrl { get; set; }
		public string ImageName { get; set; }
		public Guid PublisherId { get; set; }
	}
}
