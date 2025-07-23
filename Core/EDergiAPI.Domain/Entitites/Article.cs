
using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DergiAPI.Domain.Entitites
{
	public class Article : BaseEntity
	{
	
		public string Title { get; set; }
		public int Year { get; set; }
		public int VolumeNumber { get; set; }

		public ICollection<ArticleIssue> Issues { get; set; }

		public string Description { get; set; }
		public string Keywords { get; set; }
		public string SupportingInstitution { get; set; }
		public string ProjectNumber { get; set; }
		public string Reference { get; set; }
		public string Details { get; set; }

		public ICollection<Author> Authors { get; set; }
	}
}
