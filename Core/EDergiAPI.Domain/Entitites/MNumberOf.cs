

using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DergiAPI.Domain.Entitites
{
	public class MNumberOf : BaseEntity
	{
	
		public int Year { get; set; }
		public int  NumberOf { get; set; }
		public virtual Volume Volumes { get; set; }
	}
}
