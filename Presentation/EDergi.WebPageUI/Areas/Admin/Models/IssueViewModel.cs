using System;
using System.ComponentModel.DataAnnotations;

namespace EDergi.Web.Areas.Admin.Models
{
	public class IssueViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Sayı numarası zorunludur.")]
		public int IssueNumber { get; set; }

		[Required(ErrorMessage = "Yayın tarihi zorunludur.")]
		public DateTime PublishDate { get; set; }

		[Required(ErrorMessage = "Cilt ID'si zorunludur.")]
		public Guid VolumeId { get; set; }
	}
}
