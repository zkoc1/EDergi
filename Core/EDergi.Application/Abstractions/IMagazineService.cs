using EDergi.Application.DTOs;
using EDergi.Application.ViewComponentModel;
using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IMagazineService
	{
		Task<List<Magazine>> GetAllAsync();
		Task<List<MagazineCreateDto>> GetAllAsync2();
		Task<Magazine> GetByIdAsync(Guid id);
		Task<bool> CreateAsync(MagazineCreateDto dto);
		Task UpdateAsync(Magazine magazine);
		Task DeleteAsync(Guid id);
		Task<List<Magazine>> ViewComponentList();
		Task<List<MagazineCreateDto>> SearchAsync(string query);


	}
}
