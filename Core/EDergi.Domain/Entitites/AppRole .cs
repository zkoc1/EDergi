using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EDergi.Domain.Entitites;

namespace EDergi.Domain.Entitites
{
	public class AppRole : IdentityRole<Guid>
	{
		public ICollection<Endpoint> Endpoints { get; set; } = new HashSet<Endpoint>();
		public ICollection<AppUserRole> UserRoles { get; set; }	= new HashSet<AppUserRole>();
	}
}
