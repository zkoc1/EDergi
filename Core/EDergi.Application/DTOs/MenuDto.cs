﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class MenuDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Icon { get; set; }
		public string? Path { get; set; }
	}
}
