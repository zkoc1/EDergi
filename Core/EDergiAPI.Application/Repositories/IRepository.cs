using DergiAPI.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DergiAPI.Application.Repostories
{
	public interface IRepository <T> where T : class
	{
		DbSet<T> Table { get; }

		Task AddAsync(MNumberOf archive);
		Task DeleteAsync(long id);
		Task<List<MNumberOf>> GetAllAsync();
		Task<MNumberOf> GetByIdAsync(long id);
		Task UpdateAsync(MNumberOf archive);
	}
}
