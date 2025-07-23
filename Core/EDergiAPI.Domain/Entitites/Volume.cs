using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DergiAPI.Domain.Entitites
{
	public class Volume : BaseEntity
	{
	
		public string Title { get; set; } //cilt 1, cilt 2 olarak tanımlanabilir
        public Guid ArticleId { get; set; }
		public ICollection<Issue> Issues { get; set; }
	}
}
