using System;
namespace EDergi.Web.Areas.Admin.Models
{
	public class MDocumentEditViewModel
	{
		public Guid Id { get; set; }
		public Guid MagazineId { get; set; }
		public string FileName { get; set; }
		public string FilePath { get; set; }
	}
}