
using EDergi.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EDergi.Domain.Entitites
{
	public class Publisher : BaseEntity
	{
		
		public string Name { get; set; }
		public ICollection<Magazine> Magazines { get; set; }
	}
}
