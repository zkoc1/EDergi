
using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DergiAPI.Domain.Entitites
{
	public class ArticleIssue : BaseEntity
	{
	
		public Guid ArticleId { get; set; }
		public Guid IssueId { get; set; }
	}
}
 
