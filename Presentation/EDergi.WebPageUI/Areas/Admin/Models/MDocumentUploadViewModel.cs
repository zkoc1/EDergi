// Areas/Admin/Models/MDocumentUploadViewModel.cs
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace EDergi.Web.Areas.Admin.Models
{
	public class MDocumentUploadViewModel
	{
		public Guid MagazineId { get; set; }
		public List<IFormFile> Files { get; set; }
	}
}
