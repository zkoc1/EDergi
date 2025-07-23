
using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DergiAPI.Domain.Entitites
{
	public class Issue : BaseEntity
	{
		
		public int IssueNumber { get; set; }
        public Guid VolumeId { get; set; }
        public Guid ArticleId { get; set; }
		public ICollection<Article> Articles { get; set; }
	}
}
