using EDergi.Application.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace EDergi.Web.Areas.Admin.Models
{
	public class MagazineViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Başlık zorunludur.")]
		[StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Açıklama zorunludur.")]
		[StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "ISSN zorunludur.")]
		[StringLength(9, ErrorMessage = "ISSN en fazla 9 karakter olabilir.")]
		public string ISSN { get; set; }

		[StringLength(50, ErrorMessage = "Dönem en fazla 50 karakter olabilir.")]
		public string Period { get; set; }

		[StringLength(500, ErrorMessage = "Amaç en fazla 500 karakter olabilir.")]
		public string Purpose { get; set; }

		[StringLength(500, ErrorMessage = "Kapsam en fazla 500 karakter olabilir.")]
		public string Scope { get; set; }

		[StringLength(1000, ErrorMessage = "Yazım kuralları en fazla 1000 karakter olabilir.")]
		public string WritingRules { get; set; }

		[StringLength(1000, ErrorMessage = "Dergi kuralları en fazla 1000 karakter olabilir.")]
		public string JournalRules { get; set; }

		[Required(ErrorMessage = "Yayıncı zorunludur.")]
		public Guid PublisherId { get; set; }

		public ViewStatsDto ViewStats { get; set; }

	}
}
