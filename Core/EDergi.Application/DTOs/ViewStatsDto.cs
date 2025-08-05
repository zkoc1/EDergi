using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class ViewStatsDto
	{
		public int ViewCount { get; set; }
		public int FavoriteCount { get; set; }
		public int DownloadCount { get; set; }
	}

}
