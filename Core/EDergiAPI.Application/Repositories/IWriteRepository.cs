using DergiAPI.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DergiAPI.Application.Repostories
{
	public interface IWriteRepository<T> : IRepository<T> where T : class
	{
	 Task <bool> Addasync(T model);
		Task<bool> Addasync(List<T> model);
		Task AddAsync(Article article);
		Task<bool> AddAsync(Author author);
		Task AddAsync(Issue issue);
		Task AddAsync(MDocument document);
		Task AddAsync(Magazine magazine);
		Task AddAsync(Publisher publisher);
		Task AddAsync(ReadIndex readIndex);
		Task AddAsync(ViewStats viewStats);
		Task AddAsync(Volume volume);
		Task AddAsync(ArticleIssue articleIssue);
		void Remove(Issue issue);
		Task<bool> RemoveAsync(T model);	
		Task<bool> RemoveAsync(Guid id);
		Task<bool> RemoveAsync(MNumberOf author);
		Task SaveAsync();
	
		void Update(Issue issue);
		void Update(Magazine magazine);
		void Update(Publisher publisher);
		void Update(ViewStats viewStats);
		void Update(Volume volume);
		Task<bool> UpdateAsync(T model);
	}
}
