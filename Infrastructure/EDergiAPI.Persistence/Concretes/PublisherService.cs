using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes.Services
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

		public async Task<List<Publisher>> GetAllAsync()
		{
			return await _readRepository.GetAllAsync();
		}

		public async Task<Publisher> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task<Publisher> CreateAsync(Publisher publisher)
		{
			await _writeRepository.Addasync(publisher);
			await _writeRepository.SaveAsync();
			return publisher;
		}

		public async Task<Publisher> UpdateAsync(Publisher publisher)
		{
			_writeRepository.Update(publisher);
			await _writeRepository.SaveAsync();
			return publisher;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var publisher = await _readRepository.GetByIdAsync(id);
			if (publisher == null) return false;

			await _writeRepository.RemoveAsync(publisher);
			await _writeRepository.SaveAsync();
			return true;
		}
	}
}
