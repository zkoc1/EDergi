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
		public string Title { get; set; } 
		public string Description { get; set; } 
		public DateTime StartDate { get; set; }
		public string ISSN { get; set; }
		public string Period { get; set; }
        public string Purpose { get; set; }
        public string Scope { get; set; }
        public string WritingRules { get; set; }
		public string JournalRules { get; set; }
		public ViewStats ViewStats { get; set; }
		public Publisher  Publisher { get; set; }
		public Guid PublisherId { get; set; }
		public ICollection<MDocument> Documents { get; set; }
		public ICollection<ReadIndex> Indexes { get; set; }//Dizinler
		public ICollection<Volume> Volumes { get; set; }
	}
}
