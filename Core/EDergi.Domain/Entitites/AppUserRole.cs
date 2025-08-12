﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Domain.Entitites
{
	public class AppUserRole : IdentityUserRole<Guid>
	{
		public AppUser User { get; set; }
		public AppRole Role { get; set; }
	}
}
