
using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DergiAPI.Domain.Entitites
{
	public class Author : BaseEntity
	{
	
		public string Name { get; set; }
		public string Affiliation { get; set; }
	}
}
