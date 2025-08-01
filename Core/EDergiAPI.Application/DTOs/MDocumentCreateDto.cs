using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergiAPI.Application.DTOs
{
	public class MDocumentCreateDto
	{
		public Guid MagazineId { get; set; }

		public string FileName { get; set; }

		public string FilePath { get; set; }

		public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
		
	}


}
