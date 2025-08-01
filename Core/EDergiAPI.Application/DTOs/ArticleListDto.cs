using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergiAPI.Application.DTOs
{
	
		public class ArticleListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string PdfUrl { get; set; }
    public string SupportingInstitution { get; set; }
    public string ProjectNumber { get; set; }
    public string Reference { get; set; }
    public string ArticleLink { get; set; }
    public Guid IssueId { get; set; }
    public List<Guid> AuthorIds { get; set; }
    public bool IsApproved { get; set; }
	}

	
}
