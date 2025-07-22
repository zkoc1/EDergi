using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Domain.Entitites;
using System.Reflection;

namespace ETicaretAPI.Persistance.Contexts
{
	public class ETicaretAPIDbContext : DbContext
	{
		
		public ETicaretAPIDbContext(DbContextOptions<ETicaretAPIDbContext> options) : base(options)
		{
		}

		
		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }

		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
	}
}
