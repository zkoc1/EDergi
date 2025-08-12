using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Domain.Entitites
{
	public class EmailSettings
	{
		public string Smtp { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string From { get; set; }
	}

}
