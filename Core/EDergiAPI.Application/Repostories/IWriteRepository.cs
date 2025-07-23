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
		Task AddAsync(ArticleIssue articleIssue);
		Task<bool> AddAsync(Author author);
		Task AddAsync(Issue issue);
		void Remove(Issue issue);
		Task<bool> RemoveAsync(T model);	
		Task<bool> RemoveAsync(Guid id);
		Task<bool> RemoveAsync(MNumberOf author);
		Task SaveAsync();
		void Update(ArticleIssue articleIssue);
		void Update(Issue issue);
		void Update(Magazine magazine);
		void Update(Publisher publisher);
		void Update(PurposeScope purposeScope);
		void Update(Rules rules);
		void Update(ViewStats viewStats);
		void Update(Volume volume);
		void Update(WritingRules writingRules);
		Task<bool> UpdateAsync(T model);
	}
}
