using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.Abstractions;
using DergiAPI.Application.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class AdminService : IAdminService
	{
		private readonly IReadRepository<Admin> _readRepository;
		private readonly IWriteRepository<Admin> _writeRepository;

		public AdminService(IReadRepository<Admin> readRepository, IWriteRepository<Admin> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<Admin>> GetAllAsync()
		{
			var admins = _readRepository.GetAll().ToList();
			return Task.FromResult(admins);
		}

		public async Task<Admin> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetByIdAsync(id);
		}

		public async Task CreateAsync(Admin admin)
		{
			await _writeRepository.AddAsync(admin);
		}

		public async Task UpdateAsync(Admin admin)
		{
			await _writeRepository.UpdateAsync(admin);
		}

		public async Task DeleteAsync(Guid id)
		{
			await _writeRepository.RemoveAsync(id);
		}
	}
}
