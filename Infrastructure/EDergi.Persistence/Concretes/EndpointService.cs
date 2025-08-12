using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using EDergi.Persistence.Contexts;  // DbContext (varsayım)
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EndpointService : IEndpointService
{
	private readonly EDergiDbContext _context;
	private readonly RoleManager<AppRole> _roleManager;

	public EndpointService(EDergiDbContext context, RoleManager<AppRole> roleManager)
	{
		_context = context;
		_roleManager = roleManager;
	}

	public async Task<IEnumerable<EndpointDto>> GetAllEndpointsAsync()
	{
		var endpoints = await _context.Endpoints
			.Include(e => e.Roles)
			.Include(e => e.Menu)
			.ToListAsync();

		return endpoints.Select(e => new EndpointDto
		{
			Id = e.Id,
			ActionType = e.ActionType,
			HttpType = e.HttpType,
			Definition = e.Definition,
			Code = e.Code,
			MenuId = e.Menu.Id,
			RoleNames = e.Roles.Select(r => r.Name).ToList()
		});
	}

	public async Task<bool> RegisterEndpointAsync(EndpointDto dto)
	{
		// Menü kontrolü
		var menu = await _context.Menus.FindAsync(dto.MenuId);
		if (menu == null)
			return false;

		// Var olan endpoint kontrolü (Code eşsiz olmalı)
		var existing = await _context.Endpoints.FirstOrDefaultAsync(e => e.Code == dto.Code);
		if (existing != null)
			return false;

		var roles = new List<AppRole>();
		if (dto.RoleNames != null && dto.RoleNames.Any())
		{
			foreach (var roleName in dto.RoleNames)
			{
				var role = await _roleManager.FindByNameAsync(roleName);
				if (role != null)
					roles.Add(role);
			}
		}

		var endpoint = new Endpoint
		{
			Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
			ActionType = dto.ActionType,
			HttpType = dto.HttpType,
			Definition = dto.Definition,
			Code = dto.Code,
			Menu = menu,
			Roles = roles
		};

		await _context.Endpoints.AddAsync(endpoint);
		var result = await _context.SaveChangesAsync();

		return result > 0;
	}
}
