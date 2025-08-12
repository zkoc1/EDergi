using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class AssignRolesDto
	{
		public Guid UserId { get; set; }
		public List<string> Roles { get; set; }
	}

}
