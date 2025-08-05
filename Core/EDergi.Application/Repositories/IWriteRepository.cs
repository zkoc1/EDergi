using EDergi.Domain.Entitites;
using EDergi.Domain.Entitites.Commmon;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Repostories
{
	public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
	{
		Task<bool> AddAsync(T model);
		Task<bool> AddRangeAsync(List<T> models);

		Task<bool> RemoveAsync(T model);
		Task<bool> RemoveAsync(Guid id);

		void Update(T model);

		Task SaveAsync();
		Task UpdateAsync(Article article);
		Task UpdateAsync(Author author);
		Task UpdateAsync(MDocument document);
		Task UpdateAsync(Issue issue);
		Task UpdateAsync(Magazine magazine);
		Task UpdateAsync(Publisher publisher);
		Task UpdateAsync(ReadIndex readIndex);
		Task UpdateAsync(ViewStats viewStats);
		Task UpdateAsync(Volume volume);
		void Remove(Magazine magazine);
	}
}
