using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions.Services
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
