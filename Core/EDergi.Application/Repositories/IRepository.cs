using EDergi.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.Repostories
{
	public interface IRepository <T> where T : class
	{
		DbSet<T> Table { get; }

		Task<bool> AddAsync(T model);
	}
}
