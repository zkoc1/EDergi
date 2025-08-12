using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IEndpointService
	{
		Task<IEnumerable<EndpointDto>> GetAllEndpointsAsync();
		Task<bool> RegisterEndpointAsync(EndpointDto dto);
	}

}
