using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergiAPI.Application.DTOs
{
	public class ArticleCreateDto
	{
		public string Title { get; set; }
		public string Description { get; set; } // Article'daki Description ile eşleşiyor
		public string Keywords { get; set; }
		public string PdfUrl { get; set; } // Article'dan eklendi
		public string SupportingInstitution { get; set; } // Article'dan eklendi
		public string ProjectNumber { get; set; } // Article'dan eklendi
		public string Reference { get; set; } // Article'dan eklendi
		public string ArticleLink { get; set; } // Article'dan eklendi
		public Guid IssueId { get; set; }
		public bool IsApproved { get; set; }
													 // ArticleAuthor ilişkisi için
		public List<Guid> AuthorIds { get; set; }
		// ArticleAuthors yerine doğrudan author ID'leri

	}
}
