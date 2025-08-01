using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Application.Abstractions.Services
{
	public interface IAuthorService
	{
		Task<List<Author>> GetAllAsync();
		Task<Author> GetByIdAsync(Guid id);
		Task CreateAsync(AuthorCreateDto dto);
		Task UpdateAsync(Author author);
		Task DeleteAsync(Guid id);
	}
}
