using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergiAPI.Application.DTOs
{
	public class IssueCreateDto
	{
		public int IssueNumber { get; set; }
		public DateTime PublishDate { get; set; }

		// Article'lar bu Issue'ya ait
		public List<ArticleCreateDto> Articles { get; set; }
	}

}
