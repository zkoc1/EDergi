using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class IssueCreateDto
	{
		public Guid Id { get; set; }
		public int IssueNumber { get; set; }
		public DateTime PublishDate { get; set; }

		public Guid VolumeId { get; set; }
		// Article'lar bu Issue'ya ait
		public List<ArticleCreateDto> Articles { get; set; }
	}

}
