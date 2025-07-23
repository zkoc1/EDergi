
using DergiAPI.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DergiAPI.Domain.Entitites
{
	public class MDocument : BaseEntity
	{
		
		public string FileName { get; set; }
		public string FilePath { get; set; }
	}
}
