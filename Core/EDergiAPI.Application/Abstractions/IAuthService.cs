using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DergiAPI.Application.DTOs;

namespace DergiAPI.Application.Abstractions
{
	public interface IAuthService
	{
		Task<string> RegisterAsync(RegisterDto model);
		Task<LoginResultDto> LoginAsync(LoginDto model);

		
	}
}

