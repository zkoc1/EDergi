using DergiAPI.Domain.Entitites;
using DergiAPI.Application.Repostories;
using DergiAPI.Application.Abstractions;
using DergiAPI.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Services
{
	public class PublisherService : IPublisherService
	{
		private readonly IReadRepository<Publisher> _readRepository;
		private readonly IWriteRepository<Publisher> _writeRepository;

		public PublisherService(IReadRepository<Publisher> readRepository, IWriteRepository<Publisher> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<Publisher>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<Publisher> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(p => p.Id == id);
		}

		public async Task<Publisher> CreateAsync(Publisher publisher)
		{
			await _writeRepository.AddAsync(publisher);
			return publisher;
		}

		public async Task<Publisher> UpdateAsync(Publisher publisher)
		{
			await _writeRepository.UpdateAsync(publisher);
			return publisher;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var publisher = await _readRepository.GetSingleAsync(p => p.Id == id);
			if (publisher == null) return false;

			await _writeRepository.RemoveAsync(publisher);
			return true;
		}
	}
}
