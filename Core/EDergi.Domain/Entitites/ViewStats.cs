
using EDergi.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EDergi.Domain.Entitites
{
	public class ViewStats : BaseEntity
	{
	
		public int ViewCount { get; set; }
		public int FavoriteCount { get; set; }
		public int DownloadCount { get; set; }
		public Guid MagazineId { get; set; }
		public virtual Magazine Magazine { get; set; }
	}
}
