using EDergi.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Interfaces.Services
{
	public interface IPublisherService
	{
		Task<List<Publisher>> GetAllAsync();
		Task<Publisher> GetByIdAsync(Guid id);
		Task<Publisher> CreateAsync(Publisher publisher);
		Task<Publisher> UpdateAsync(Publisher publisher);
		Task<bool> DeleteAsync(Guid id);
	}
}
