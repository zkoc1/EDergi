using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DergiAPI.Domain.Entitites
{
	public class Magazine : BaseEntity
	{
		public string Name { get; set; } 
		public string Description { get; set; } 
		public DateTime StartDate { get; set; }
		public string ISSN { get; set; }
		public string Period { get; set; } 

		public PurposeScope PurposeScope { get; set; }
		public Rules Rules { get; set; }
		public WritingRules WritingRules { get; set; }
		public ViewStats ViewStats { get; set; }

		public ICollection<Publisher> Publishers { get; set; }
		public ICollection<MDocument> Documents { get; set; }
		public ICollection<ReadIndex> Indexes { get; set; }//Dizinler
		public ICollection<MNumberOf> Archives { get; set; }
		public ICollection<Article> Articles { get; set; }
	}
}
