using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IPublisherService
	{
		Task<List<Publisher>> GetAllAsync();
		Task<Publisher> GetByIdAsync(Guid id);
		Task CreateAsync(Publisher publisher);
		Task UpdateAsync(Publisher publisher);
		Task DeleteAsync(Guid id);
	}
}
