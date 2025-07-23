
using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DergiAPI.Domain.Entitites
{
	public class ReadIndex : BaseEntity
	{
		
		public string Name { get; set; } // Örn: TR Dizin, Scopus vs.
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
	}
}
