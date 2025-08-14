using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EDergi.Web.Areas.Admin.Models
{
	public class VolumeViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Cilt başlığı zorunludur.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Yıl zorunludur.")]
		public int Year { get; set; }

		[Required(ErrorMessage = "Dergi ID'si zorunludur.")]
		public Guid MagazineId { get; set; }

	
	}
}
