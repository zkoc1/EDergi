using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IMenuService
	{
		Task<IEnumerable<MenuDto>> GetAllMenusAsync();
		Task<bool> AddMenuAsync(MenuDto dto);
	}

}
