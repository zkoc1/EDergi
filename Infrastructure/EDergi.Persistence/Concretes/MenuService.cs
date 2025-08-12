using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using EDergi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MenuService : IMenuService
{
	private readonly EDergiDbContext _context;

	public MenuService(EDergiDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<MenuDto>> GetAllMenusAsync()
	{
		var menus = await _context.Menus.ToListAsync();

		return menus.Select(m => new MenuDto
		{
			Id = m.Id,
			Name = m.Name,
			Icon = m.Icon,
			Path = m.Path
		});
	}

	public async Task<bool> AddMenuAsync(MenuDto dto)
	{
		if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
			return false;

		var menu = new Menu
		{
			Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
			Name = dto.Name,
			Icon = dto.Icon,
			Path = dto.Path
		};

		await _context.Menus.AddAsync(menu);
		var result = await _context.SaveChangesAsync();

		return result > 0;
	}
}
