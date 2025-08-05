using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class AuthorCreateDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Affiliation { get; set; } // Kurum
		public List<Guid>? ArticleIds { get; set; } = new List<Guid>();
	}
}
