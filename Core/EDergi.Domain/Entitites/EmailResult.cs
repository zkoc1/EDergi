using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Domain.Entitites
{
	public class EmailResult
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public static EmailResult Ok(string message = "") => new() { Success = true, Message = message };
		public static EmailResult Fail(string message) => new() { Success = false, Message = message };
	}
}
